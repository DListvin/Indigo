import sys, os
SADPath = os.path.abspath('./GameSide/')
if not SADPath in sys.path:
    sys.path.append(SADPath)
import gevent
import random
import json

from  Model import *
from geventwebsocket import WebSocketServer, WebSocketApplication, Resource

activeQueues = []
model = None

class PlotApplication(WebSocketApplication):
    def on_open(self):
        global model
        random.seed()
        newSeed = random.randrange(1000000)
        model = Model(newSeed, activeQueues)
        model.start()
        for q in activeQueues:
            q.put(ModelState.Running)
        model.simulatingWorld.map.SortAgents(model.simulatingWorld.agents)
        self.ws.send(model.simulatingWorld.map.ToJson())

    def on_message(self, message):    
        model.simulatingWorld.map.SortAgents(model.simulatingWorld.agents)
        self.ws.send(model.simulatingWorld.map.ToJson())

    def on_close(self, reason):
        print("Connection Closed!!!", reason)

resource = Resource({
    '/data': PlotApplication
})  

server = WebSocketServer(('0.0.0.0', 2345), resource, debug=True)
server.serve_forever()