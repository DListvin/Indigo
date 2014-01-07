//CONSTANTS
var StageXSize = $(window).width();
var StageYSize = $(window).height();
var HexXStep = 225;     //225 for scale 1
var HexYStep = 73;      //73 for scale 1
var HexRowShift = 150;  //150 for scale 1 
var HexScale = 0.07;

var chunkXShift = 4573; // 4573 for scale 1
var chunkYShift = 1170; // 1170 for scale 1
var chunkzxShift = 1278; //1278 for scale 1
var chunkzyShift = -76; //-76 for scale 1

var ChunkSize = 16;
var ChunkLenght = ChunkSize * 2 - 1;
var chunkTilesCount = (ChunkSize * ChunkSize) * 2 + (ChunkSize - 2) * (ChunkSize - 1) - 1;
var StageXShift = HexXStep * ChunkSize;

//CONSTANTS

$(window).resize(resize)
$("body").css("overflow", "hidden");

var stage = new PIXI.Stage(0x66FF99, true);
var renderer = PIXI.autoDetectRenderer(StageXSize, StageYSize);
document.body.appendChild(renderer.view);
requestAnimationFrame(animate);

var tileTexture = PIXI.Texture.fromImage("http://zurkserv.myftp.org:1234/Static/Images/isometr.png");
var tileWhiteTexture = PIXI.Texture.fromImage("http://zurkserv.myftp.org:1234/Static/Images/isometr_white.png");

var socket = new WebSocket("ws://zurkserv.myftp.org:1234/data");
socket.onmessage = function(event)
{		
	//alert(event.data);
	var jsonData = JSON.parse(event.data);
	//alert(jsonData.chunks[0].tiles[0].t);
	if(stage.children.length == 0)
	{
		for(var chunk_num in jsonData.chunks)
		{
			var chunk = jsonData.chunks[chunk_num];

			var zxShift = chunkzxShift * -chunk.z;
			var xShift = chunk.x * chunkXShift + zxShift;

			var ySgn = chunk.z > chunk.y ? 1 : -1;
			var zyShift = chunk.z == 0 ? 0 : chunkzyShift * Math.abs(chunk.z);
			var yShift = ((Math.abs(chunk.y) + Math.abs(chunk.z)) * chunkYShift + zyShift) * ySgn;

			for(var i = 0, tile_num = 0; i < ChunkLenght; i++)
			{
				for(var j = 0; j < (i < ChunkSize ? ChunkSize + i : ChunkLenght - 1 - (i - ChunkSize)); j++, tile_num++)
				{
					var tile = jsonData.chunks[chunk_num].tiles[tile_num];

					var new_tile = tile.t == 0 ? new PIXI.Sprite(tileTexture) : new PIXI.Sprite(tileWhiteTexture);
					var shift = i < ChunkSize ? i : ChunkLenght - i - 0.5 + 0.5 * (i - ChunkSize);
					new_tile.position.x = 400 + (j * HexXStep - shift * HexRowShift + xShift + StageXShift) * HexScale;
					new_tile.position.y = 200 + (i * HexYStep + yShift) * HexScale;

					new_tile.anchor.x = 0;
					new_tile.anchor.y = 0;
					new_tile.scale.x = HexScale;
					new_tile.scale.y = HexScale;
					
					new_tile.hitArea = new PIXI.Polygon([
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
						35, 256 - 70,
					]);

					new_tile.setInteractive(true);
					new_tile.mouseover = new_tile.touchstart = function (interactionData)
					{
						this.setTexture(tileWhiteTexture);
					}
					new_tile.mouseout = new_tile.touchend = function (interactionData)
					{
						this.setTexture(tileTexture);
					}
					stage.addChild(new_tile);
				}
			}
		}
		setInterval(function(){socket.send("refresh")}, 1000);
	}
	else
	{
		for(var chunk_num in jsonData.chunks)
		{
			for(var tile_num in jsonData.chunks[chunk_num].tiles)
			{
				var tile = jsonData.chunks[chunk_num].tiles[tile_num];
				//alert(chunkTilesCount);
				//alert(stage.children[chunk_num * chunkTilesCount + tile_num]);
				//alert(stage.children[chunk_num * chunkTilesCount + tile_num]);
				stage.children[Number(chunk_num * chunkTilesCount) + Number(tile_num)].setTexture(tile.t == 0 ? tileTexture : tileWhiteTexture);
			}
		}

	}
}


function animate() 
{
	renderer.render(stage);
	requestAnimationFrame(animate);
}

function resize()
{
	renderer.resize($(window).width(), $(window).height())
}
