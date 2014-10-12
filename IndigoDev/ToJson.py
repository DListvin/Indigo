from GameSide.MapEngine import ChunkAxialCoords

def MapToJson(map, x, y, z, height, width):
    """
    Converting map to Json according to client request
    @type x, y, z, height, width: int
    @param x, y, z: coords of the left-top hex on the client screen
    @param height, width: lengths of the client screen(already in chunks)
    """

    jsonString = '{"map":['
    firstTime = True

    #converting coords to offset for building a rectangle array to return
    r = z
    q = x + (z - (z & 1)) / 2

    for i in xrange(height):
        for j in xrange(width):
            if not firstTime:
                jsonString += ","
            firstTime = False
            #go to right place in array
            rToAdd = r + i
            qToAdd = q + j
            #now converting coords back to access mapData
            xToAdd = qToAdd - (rToAdd - (rToAdd & 1)) / 2
            yToAdd = -x - rToAdd
            newCoords = ChunkAxialCoords(xToAdd, yToAdd)

            if newCoords in map.mapData:
                jsonString += TileToJson(map.mapData[newCoords])
            else:
                jsonString += '{"t":' + str(0) + ',"a":[]}'
    jsonString += "]}"

    return jsonString

def ChunkToJson(chunk):
    jsonString = '{"x":' + str(chunk.x) + ',"y":' + str(chunk.y) + ',"z":' + str(chunk.z)
    jsonString += ',"tiles":['
    firstTime = True
    for tile in chunk.chunkData:
        if(not firstTime):
            jsonString += ","
        firstTime = False
        jsonString += TileToJson(tile)
    jsonString += "]}"

    return jsonString

def TileToJson(tile):
    jsonString = '{"t":' + str(tile.tileType) + ',"a":['

    firstTime = True
    for agent in tile.agentsList:
        if(not firstTime):
            jsonString += ","
        firstTime = False
        jsonString += AgentToJson(agent)
    jsonString += "]}"
    return jsonString

def AgentToJson(agent):
    if agent.Type == "MovingMan":
        return '{"t":' + str(1) + '}'
    return '{"t":' + str(agent.Type) + '}'

