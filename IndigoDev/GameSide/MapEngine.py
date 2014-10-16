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

        for i in xrange(rawNumber):
            collNumber += ChunkSize + i if i < ChunkSize else ChunkLength - 1 - (i - ChunkSize)

        return collNumber

    def __init__(self, seed):
        random.seed(seed)
        self.mapData = {}
        for i in xrange(10):
            for j in xrange(10):
                x = j - (i - (i & 1)) / 2
                y = -x - i
                newTile = 1
                newAgentsList = []
                newCoords = str(x) + " " + str(y)
                self.mapData[newCoords] = Tile(newTile, newAgentsList)

    def sortAgents(self, agentsList):
        for tile in self.mapData.values():
            tile.agentsList = []
        for agent in agentsList:
            x = agent.GetPropertyByName("LocationX").Value
            y = agent.GetPropertyByName("LocationY").Value
            newCoords = str(x) + " " + str(y)
            if newCoords in self.mapData:
                self.mapData[newCoords].agentsList.append(agent)


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


class Tile:

    def __init__(self, argTileType, argAgentsList):
        self.tileType = argTileType
        self.agentsList = argAgentsList