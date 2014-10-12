import gevent
import random
import json

from GameSide.Model import *
from ToJson import *
from geventwebsocket import WebSocketServer, WebSocketApplication, Resource

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
        global sockets
        
        model.simulatingWorld.map.sortAgents(model.simulatingWorld.agents)

        for socket in sockets:
            if not socket.closed:
                socket.send(MapToJson(model.simulatingWorld.map, 0, 0, 0, 10, 10))

    def on_open(self):
        global connectionsCount
        global model
        global activePipes
        global sockets

        connectionsCount += 1
        sockets.append(self.ws)

        for p in activePipes:
            p.send(ModelState.Running)
        print str(server.clients) + "\n" 

    def on_message(self, message):
        pass

    def on_close(self, reason):
        global connectionsCount
        global activePipes
        global sockets

        connectionsCount -= 1
        sockets.remove(self.ws)

        if connectionsCount == 0:
            for p in activePipes:
                p.send(ModelState.Paused)

        print("Connection Closed!!!", reason)


if __name__ == '__main__':

    global connectionsCount #Current number of connected sockets
    global sockets          #List of connected sockets

    global model            #Model of this current server
    global activePipes      #Pipe with commands to model and from model

    connectionsCount = 0
    sockets = []
    activePipes = []
    resource = Resource({'/data': PlotApplication})  

    random.seed()
    newSeed = random.randrange(1000000)
    model = Model(newSeed, activePipes, PlotApplication.ModelUpdate)
    model.start() #Not actually starts the model, only it's thread

    server = WebSocketServer(('0.0.0.0', 2345), resource, debug=True)
    server.serve_forever()