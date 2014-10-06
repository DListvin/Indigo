import gevent
import random
import json

from GameSide.Model import *
from geventwebsocket import WebSocketServer, WebSocketApplication, Resource

class PlotApplication(WebSocketApplication):
    """
    Class for WebSocket Communication
    """
    #def __init__(self):
    #    WebSocketApplication.__init__(self)
    #    self.connections = 0

    @staticmethod
    def ModelUpdate():
        global sockets
        model.simulatingWorld.map.sortAgents(model.simulatingWorld.agents)

        for socket in sockets:
            socket.send(model.simulatingWorld.map.ToJson())

    def on_open(self):
        global connectionsCount
        global model
        global activeQueues
        global sockets
        connectionsCount += 1
        sockets.append(self.ws)

        print "\n" +'[on_open]: con_count - ]'+ str(connectionsCount) + "\n"

        for q in activeQueues:
            q.send(ModelState.Running)

    def on_message(self, message):
        pass
        #global model    
        #model.simulatingWorld.map.sortAgents(model.simulatingWorld.agents)
        #try:
        #    self.ws.send(model.simulatingWorld.map.ToJson())
        #except:
        #    pass

    def on_close(self, reason):
        global connectionsCount
        global activeQueues
        global sokets
        connectionsCount -= 1
        #sockets.remove(self.ws)

        print "\n" +'[on_close] con_count - '+ str(connectionsCount) + "\n"

        if connectionsCount == 0:
            for q in activeQueues:
                q.send(ModelState.Paused)
        print("Connection Closed!!!", reason)

if __name__ == '__main__':

    global connectionsCount
    global sockets 
    global model
    global activeQueues

    connectionsCount = 0
    sockets = []
    activeQueues = []
    resource = Resource({
    '/data': PlotApplication
})  

    random.seed()
    newSeed = random.randrange(1000000)
    model = Model(newSeed, activeQueues, PlotApplication.ModelUpdate)
    model.start()

    server = WebSocketServer(('0.0.0.0', 2345), resource, debug=True)
    server.serve_forever()