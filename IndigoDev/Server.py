import gevent
import random
import json

from GameSide.Model import *
from geventwebsocket import WebSocketServer, WebSocketApplication, Resource

activeQueues = []
model = None

class PlotApplication(WebSocketApplication):
    """
    Class for WebSocket Communication
    """
    def on_open(self):
        global model
        random.seed()
        newSeed = random.randrange(1000000)
        model = Model(newSeed, activeQueues)
        model.start()
        for q in activeQueues:
            q.put(ModelState.Running)
        model.simulatingWorld.map.sortAgents(model.simulatingWorld.agents)
        self.ws.send(model.simulatingWorld.map.ToJson())

    def on_message(self, message):    
        model.simulatingWorld.map.sortAgents(model.simulatingWorld.agents)
        self.ws.send(model.simulatingWorld.map.ToJson())

    def on_close(self, reason):
        print("Connection Closed!!!", reason)

resource = Resource({
    '/data': PlotApplication
})  

if __name__ == '__main__':
    server = WebSocketServer(('0.0.0.0', 2345), resource, debug=True)
    server.serve_forever()