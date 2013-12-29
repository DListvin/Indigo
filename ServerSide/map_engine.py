import random
import json
import time

class Map:
	mapData = []

	def __init__(self, seed):
		random.seed(seed)	
		self.mapData.append(Chunk(seed))

	def ToJson(self):
		jsonString = '{"chunks":['
		firstTime = True

		for chunk in self.mapData:
			if(not firstTime):
				jsonString += ","
			firstTime = False
			jsonString += chunk.ToJson()

		jsonString += "]}"

		return jsonString

ChunkSize = 16
ChunkLenght = ChunkSize * 2 - 1

class Chunk:
	chunkData = []

	def __init__(self, seed):
		self.chunkData = []
		for i in xrange(ChunkLenght):
			for j in xrange(ChunkSize + i if i < ChunkSize else ChunkLenght - 1 - (i - ChunkSize)):	
				newTile = (seed * (i + 1) / (j + 1) + seed % (j + i + 1))  % 2
				#print random.random()
				#newTile = random.random() % 2 
				self.chunkData.append(Tile(newTile))

	def ToJson(self):
		jsonString = '{"tiles":['
		firstTime = True
		for tile in self.chunkData:
			if(not firstTime):
				jsonString += ","
			firstTime = False
			jsonString += tile.ToJson()
		jsonString += "]}"

		return jsonString


class Tile:
	tileType = 0

	def __init__(self, argTileType):
		self.tileType = argTileType

	def ToJson(self):
		return '{"t":' + str(self.tileType) + '}'