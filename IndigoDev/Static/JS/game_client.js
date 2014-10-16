// <editor-fold desc="CONSTANTS">
//Server info
var serverName = "zurkserv.myftp.org";
var serverPort = 1234;

//Chunk parametres
var ChunkSize = 16;
var ChunkLenght = ChunkSize * 2 - 1;
var chunkTilesCount = (ChunkSize * ChunkSize) * 2 + (ChunkSize - 2) * (ChunkSize - 1) - 1;

//For chunk drawing
var StageXSize = $(window).width();
var StageYSize = $(window).height();
var HexXStep = 225;     //225 for scale 1 x step from one hex to another
var HexYStep = 73;      //73 for scale 1 y step from one hex row to another
var HexRowShift = 150;  //150 for scale 1 shift for hex rows in chunk
var HexScale =1;    //map scale
var HexScreenWidth = Math.floor(10 / HexScale); //Screen width in hexes
var HexScreenHeight = Math.floor(17 / HexScale); //Screen height in hexes
var StageXShift = HexXStep * ChunkSize;

//For map drawing
//This for chunk size = 16, may be should be some formulas
var chunkXShift = 4573; // 4573 for scale 1 x shift from one chunk to another
var chunkYShift = 1170; // 1170 for scale 1 y shift from one chunk to another
var chunkzxShift = 1278; //1278 for scale 1 shift to compensate isometric x shift
var chunkzyShift = -76; //-76 for scale 1 shift to compensate isometric y shift

//For map drawing
var tileTexture = PIXI.Texture.fromImage("http://" + serverName + ":" + serverPort + "/Static/Images/isometr.png");
var tileWhiteTexture = PIXI.Texture.fromImage("http://" + serverName + ":" + serverPort + "/Static/Images/isometr_white.png");
var agentTexture = PIXI.Texture.fromImage("http://" + serverName + ":" + serverPort + "/Static/Images/agent.png");

//</editor-fold>

// <editor-fold desc="GLOBAL FLAGS AND VARIABLES">

//Renderer engine init
var stage = new PIXI.Stage(0x66FF99, true);
var renderer = PIXI.autoDetectRenderer(StageXSize, StageYSize);
var mapContainer = new PIXI.DisplayObjectContainer();
var agentsContainer = new PIXI.DisplayObjectContainer();
mapContainer.visible = true;
agentsContainer.visible = true;
stage.addChild(mapContainer);
stage.addChild(agentsContainer);
document.body.appendChild(renderer.view);
requestAnimationFrame(animate);
    
//for map dragging
var mouseDownFlag = false;
var xMouseDown = 0;
var yMouseDown = 0;

//for chunk refreshing
var pixelCoords = [0, 0];
var centralChunkCoords = [0, 0, 0];

// </editor-fold>

//<editor-fold desc="Jquery">
$(window).resize(resize);
$("body").css("overflow", "hidden");

function resize()
{
	renderer.resize($(window).width(), $(window).height());
}
// </editor-fold>


var socket = new WebSocket("ws://" + serverName + ":" + serverPort + "/data");

window.onbeforeunload = function()
{
	socket.onclose = function () {}; // disable onclose handler first
	socket.close();
};

socket.onopen = function()
{
	var clientInfo = {
		'method' : 'refresh',
		'coords' : centralChunkCoords,
		'height' : HexScreenHeight,
		'width' : HexScreenWidth
	};
	socket.send(JSON.stringify(clientInfo));
};

socket.onmessage = function(event)
{		
	var jsonData = JSON.parse(event.data);
	if(mapContainer.children.length === 0)
	{		
		for (var i = 0, tileNum = 0; i < HexScreenHeight; i++)
		{
			for(var j = 0; j < HexScreenWidth; j++, tileNum++)
			{                                 
				// <editor-fold desc="Tile creation">
				var tile = jsonData.map[tileNum];     
				var newTile = tile.t === 0 ? new PIXI.Sprite(tileTexture) : new PIXI.Sprite(tileWhiteTexture);
				var shift = i % 2 ? -(i - 1)/4 + 1.5: 1 - (i / 4);
				newTile.position.x = /*400 + */(j * HexXStep + shift * HexRowShift) * HexScale;
				newTile.position.y = /*200 + */(i * HexYStep) * HexScale;

				newTile.anchor.x = 0;
				newTile.anchor.y = 0;
				newTile.scale.x = HexScale;
				newTile.scale.y = HexScale;

				newTile.hitArea = new PIXI.Polygon([
					149, 256 - 91,
					162, 256 - 91,
					249, 256 - 71,
					255, 256 - 68,
					233, 256 - 24,
					229, 256 - 21,
					213, 256 - 19,
					117, 256 - 0,
					105, 256 - 0,
					10, 256 - 22,
					31, 256 - 68,
					35, 256 - 70
				]);

				newTile.setInteractive(true);
				newTile.mouseover = newTile.touchstart = function (interactionData)
				{
					this.setTexture(tileWhiteTexture);
				};
				newTile.mouseout = newTile.touchend = function (interactionData)
				{
					this.setTexture(tileTexture);
				};
				mapContainer.addChild(newTile);

				for(var agent in tile.a)
				{
					var new_agent = new PIXI.Sprite(agentTexture);

					new_agent.position.x = newTile.position.x
					new_agent.position.y = newTile.position.y

					new_agent.anchor.x = 0;
					new_agent.anchor.y = 0;
					new_agent.scale.x = HexScale;
					new_agent.scale.y = HexScale;

					agentsContainer.addChild(new_agent);
				}
				// </editor-fold>
			}
			
		}
	}
	else
	{	
		while(agentsContainer.children.length > 0)
		{ 
  			var child = agentsContainer.getChildAt(0);
 			agentsContainer.removeChild(child);
		}
		
		for(var tileNum in jsonData.map)
		{
			var tile = jsonData.map[tileNum];
			var child = mapContainer.children[tileNum];
			child.setTexture(tile.t === 0 ? tileTexture : tileWhiteTexture);

			for(var agent in tile.a)
			{
				var new_agent = new PIXI.Sprite(agentTexture);

				new_agent.position.x = child.position.x;
				new_agent.position.y = child.position.y;

				new_agent.anchor.x = 0;
				new_agent.anchor.y = 0;
				new_agent.scale.x = HexScale;
				new_agent.scale.y = HexScale;

				agentsContainer.addChild(new_agent);
			}
		}

	}
};
// <editor-fold desc="Map dragging events">
$(document).mousedown(function(interactionData)
{
	xMouseDown = interactionData.pageX;
	yMouseDown = interactionData.pageY;
	mouseDownFlag = true;
});
$(document).mouseup(function(interactionData)
{
	mouseDownFlag = false;
});
$(document).mousemove(function(interactionData)
{
	if(mouseDownFlag)
	{
		for(var child_num in stage.children)
		{
			var child = stage.children[child_num];

			var deltaX = xMouseDown - interactionData.pageX;
			var deltaY = yMouseDown - interactionData.pageY;

			child.position.x -= deltaX;		
			child.position.y -= deltaY;
		}
		xMouseDown = interactionData.pageX;	
		yMouseDown = interactionData.pageY;
			
		pixelCoords[0] -= deltaX;
		pixelCoords[1] -= deltaY;

		var pixelToHexQ = Math.floor(pixelCoords[0] / (HexXStep * HexScale)); 
		var pixelToHexR = Math.floor(pixelCoords[1] / (HexYStep * HexScale));

		centralChunkCoords[0] = pixelToHexQ - (pixelToHexR - (pixelToHexR & 1)) / 2;
		centralChunkCoords[1] = pixelToHexR;
		centralChunkCoords[2] = - centralChunkCoords[0] - centralChunkCoords[1];
	}
});
setInterval(
	function()
	{
		var clientInfo = {
			'method' : 'refresh',
			'coords' : centralChunkCoords,
			'height' : HexScreenHeight,
			'width' : HexScreenWidth
		};
		socket.send(JSON.stringify(clientInfo));
	},
	1000
);
// </editor-fold>

function animate() 
{
	renderer.render(stage);
	requestAnimationFrame(animate);
}
