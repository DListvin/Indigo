import gevent
import random
import json

from GameSide.Model import *
from ToJson import *
from geventwebsocket import WebSocketServer, WebSocketApplication, Resource

class Connection:
    def __init__(self, argSocket):
        self.ws = argSocket
        self.rootHex = {'x' : 0, 'y' : 0, 'z' : 0}
        self.height = 100
        self.width = 100

class PlotApplication(WebSocketApplication):
    """
    Class for WebSocket Communication
    Includes functions overrided from base class(obvious ones)
    """

    @staticmethod
    def ModelUpdate():
        """
        Function that model calls to report it's state after last tick 
        """
        global ConnectionsList
        global Model
        
        Model.simulatingWorld.map.sortAgents(Model.simulatingWorld.agents) #TODO: check: may be this is bad logic

        for connection in ConnectionsList:
            if not connection.ws.closed:
                coords = connection.rootHex
                height = connection.height
                width = connection.width
                connection.ws.send(MapToJson(Model.simulatingWorld.map, coords['x'], coords['y'], coords['z'], height, width))

    def on_open(self):
        global Model
        global ActivePipes
        global ConnectionsList

        ConnectionsList.append(Connection(self.ws))

        for p in ActivePipes:
            p.send(ModelState.Running)
        print str(server.clients) + "\n" 

    def on_message(self, message):
        global ConnectionsList

        print message

        currentConnection = [connection for connection in ConnectionsList if connection.ws == self.ws][0]
        jsonData = json.loads(message)
        
        currentConnection.rootHex['x'] = jsonData['coords'][0]
        currentConnection.rootHex['y'] = jsonData['coords'][1]
        currentConnection.rootHex['z'] = jsonData['coords'][2]
        currentConnection.height = jsonData['height']
        currentConnection.width = jsonData['width']

    def on_close(self, reason):
        global ActivePipes
        global ConnectionsList
        ConnectionsList = [socket for socket in ConnectionsList if socket.ws != self.ws]

        if len(ConnectionsList) == 0:
            for p in ActivePipes:
                p.send(ModelState.Paused)

        print("Connection Closed!!!", reason)


if __name__ == '__main__':
    global ConnectionsList          #List of connected sockets

    global Model            #Model of this current server
    global ActivePipes      #Pipe with commands to model and from model

    ConnectionsList = []
    ActivePipes = []
    resource = Resource({'/data': PlotApplication})  

    random.seed()
    newSeed = random.randrange(1000000)
    Model = Model(newSeed, ActivePipes, PlotApplication.ModelUpdate)
    Model.start() #Not actually starts the model, only it's thread

    server = WebSocketServer(('0.0.0.0', 2345), resource, debug=True)
    server.serve_forever()