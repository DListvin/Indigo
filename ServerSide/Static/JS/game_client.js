//CONSTANTS

var StageXSize = $(window).width();
var StageYSize = $(window).height();
var HexXStep = 225;     //222 for scale 1
var HexYStep = 73;      //69 for scale 1
var HexRowShift = 147;  //145 for scale 1
var HexScale = 0.2;

var ChunkSize = 16;
var ChunkLenght = ChunkSize * 2 - 1;

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
socket.onmessage = function(event){		
	var jsonData = JSON.parse(event.data);

	if(stage.children.length == 0)
	{
		for(var chunk_num in jsonData.chunks)
		{
			var tile_num = 0;
			for(var i = 0; i < ChunkLenght; i++)
			{
				for(var j = 0; j < (i < ChunkSize ? ChunkSize + i : ChunkLenght - 1 - (i - ChunkSize)); j++)
				{
					var tile = jsonData.chunks[chunk_num].tiles[tile_num];

					var new_tile = tile.t == 0 ? new PIXI.Sprite(tileTexture) : new PIXI.Sprite(tileWhiteTexture);
					var shift = i < ChunkSize ? i : ChunkLenght - i - 0.5 + 0.5 * (i - ChunkSize);
					new_tile.position.x = 450 + j * HexXStep * HexScale - shift * HexRowShift * HexScale;
					new_tile.position.y = i * HexYStep * HexScale;

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
					tile_num++;
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
				stage.children[tile_num].setTexture(tile.t == 0 ? tileTexture : tileWhiteTexture);
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
