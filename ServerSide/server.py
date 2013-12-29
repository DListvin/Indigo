import gevent
import random
import json
import time

from map_engine import Map
from geventwebsocket import WebSocketServer, WebSocketApplication, Resource


class PlotApplication(WebSocketApplication):
    def on_open(self):
        random.seed()
        newseed = random.randrange(1000000)
        newMap = Map(newseed)
        self.ws.send(newMap.ToJson())

    def on_message(self, message):        
        random.seed()
        newseed = random.randrange(1000000)
        newMap = Map(newseed)
        self.ws.send(newMap.ToJson())

    def on_close(self, reason):
        print "Connection Closed!!!", reason


def static_wsgi_app(environ, start_response):
    start_response("200 OK", [("Content-Type", "text/html")])
    return open("HTML/index.html").readlines()


resource = Resource({
    '/': static_wsgi_app,
    '/data': PlotApplication()
})  

if __name__ == "__main__":
    server = WebSocketServer(('0.0.0.0', 2345), resource, debug=True)
    server.serve_forever()