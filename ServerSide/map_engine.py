import random
import json
import time

class Map:

	def __init__(self, seed):
		random.seed(seed)
		self.mapData = []
		#for i in xrange(2):
			#newSeed = seed * seed / 42 + seed / (i + 1)
			#newSeed = i
			#self.mapData.append(Chunk(newSeed, 0, 0, 0))
			#random.randrange(1000000)
		self.mapData.append(Chunk(random.randrange(1000000), 0, 0, 0))
		self.mapData.append(Chunk(random.randrange(1000000), 0, 1, -1))
		self.mapData.append(Chunk(random.randrange(1000000), 1, 0, -1))
		self.mapData.append(Chunk(random.randrange(1000000), 1, -1, 0))
		self.mapData.append(Chunk(random.randrange(1000000), 0, -1, 1))
		self.mapData.append(Chunk(random.randrange(1000000), -1, 0, 1))
		self.mapData.append(Chunk(random.randrange(1000000), -1, 1, 0))
		self.mapData.append(Chunk(random.randrange(1000000), 1, 1, -2))
		self.mapData.append(Chunk(random.randrange(1000000), -1, -1, 2))
		#self.mapData.append(Chunk(random.randrange(1000000), 0, -2, 2))

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

	def __init__(self, seed, x, y, z):
		self.chunkData = []
		self.x = x
		self.y = y
		self.z = z
		for i in xrange(ChunkLenght):
			for j in xrange(ChunkSize + i if i < ChunkSize else ChunkLenght - 1 - (i - ChunkSize)):	
				newTile = (seed * (i + 1) / (j + 1) + seed % (j + i + 1))  % 2
				#newTile = seed % 2
				self.chunkData.append(Tile(newTile))

	def ToJson(self):
		jsonString = '{"x":' + str(self.x) + ',"y":' + str(self.y) + ',"z":' + str(self.z)
		jsonString += ',"tiles":['
		firstTime = True
		for tile in self.chunkData:
			if(not firstTime):
				jsonString += ","
			firstTime = False
			jsonString += tile.ToJson()
		jsonString += "]}"

		return jsonString


class Tile:

	def __init__(self, argTileType):
		self.tileType = argTileType

	def ToJson(self):
		return '{"t":' + str(self.tileType) + '}'