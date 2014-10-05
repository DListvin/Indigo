import random
import json
from SADCore.Agent import *

ChunkSize = 16
ChunkLength = ChunkSize * 2 - 1

class Map:

    @staticmethod
    def coordsChunkToGrid(argX, argY, argZ, argChunkPos):
        rad = (abs(argX) + abs(argY) + abs(argZ))/2
        return [0, 0, 0]

    @staticmethod    
    def coordsGridToChunk(argX, argY, argZ):
        return [0, 0, 0, 0]

    @staticmethod
    def coordsGridLocalToNumber(argX, argY, argZ):
        rawNumber = argZ + ChunkSize - 1
        if argZ > 0:
            collNumber = argX + ChunkSize - 1
        else:
            collNumber = argY + ChunkSize - 1

        for i in range(rawNumber):
            collNumber += ChunkSize + i if i < ChunkSize else ChunkLength - 1 - (i - ChunkSize)

        return collNumber

    def __init__(self, seed):
        random.seed(seed)
        self.mapData = []
        #for i in xrange(2):
            #newSeed = seed * seed / 42 + seed / (i + 1)
            #newSeed = i
            #self.mapData.append(Chunk(newSeed, 0, 0, 0))
            #random.randrange(1000000)
        self.mapData.append(Chunk(random.randrange(1000000), 0, 0, 0))
        #self.mapData.append(Chunk(random.randrange(1000000), 0, 1, -1))
        #self.mapData.append(Chunk(random.randrange(1000000), 1, 0, -1))
        #self.mapData.append(Chunk(random.randrange(1000000), 1, -1, 0))
        #self.mapData.append(Chunk(random.randrange(1000000), 0, -1, 1))
        #self.mapData.append(Chunk(random.randrange(1000000), -1, 0, 1))
        #self.mapData.append(Chunk(random.randrange(1000000), -1, 1, 0))
        #self.mapData.append(Chunk(random.randrange(1000000), 1, 1, -2))
        #self.mapData.append(Chunk(random.randrange(1000000), -1, -1, 2))
        #self.mapData.append(Chunk(random.randrange(1000000), 0, -2, 2))

    def sortAgents(self, agentsList):
        for chunk in self.mapData:
            for tile in chunk.chunkData:
                tile.agentsList = []
        for agent in agentsList:
            x = agent.GetPropertyByName("LocationX").Value
            y = agent.GetPropertyByName("LocationY").Value
            z = agent.GetPropertyByName("LocationZ").Value
            locCoord = Map.coordsGridLocalToNumber(x,y,z)
            self.mapData[0].chunkData[locCoord].agentsList.append(agent)

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


class Chunk:
    def __init__(self, seed, x, y, z):
        self.chunkData = []
        self.x = x
        self.y = y
        self.z = z
        for i in xrange(ChunkLength):
            for j in xrange(ChunkSize + i if i < ChunkSize else ChunkLength - 1 - (i - ChunkSize)):
                newTile = (seed * (i + 1) / (j + 1) + seed % (j + i + 1)) % 2
                newAgentsList = []
                self.chunkData.append(Tile(newTile, newAgentsList))

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

    def __init__(self, argTileType, argAgentsList):
        self.tileType = argTileType
        self.agentsList = argAgentsList

    def ToJson(self):
        jsonString = '{"t":' + str(self.tileType) + ',"a":['

        firstTime = True
        for agent in self.agentsList:
            if(not firstTime):
                jsonString += ","
            firstTime = False
            jsonString += agent.ToJson()
        jsonString += "]}"
        return jsonString