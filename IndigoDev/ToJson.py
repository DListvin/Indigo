def MapToJson(map):
    jsonString = '{"chunks":['
    firstTime = True

    for chunk in map.mapData:
        if(not firstTime):
            jsonString += ","
        firstTime = False
        jsonString += ChunkToJson(chunk)

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

