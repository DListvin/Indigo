__author__ = 'Zurk'
import sys, os
SADPath = os.path.abspath('./SADCore/')
if not SADPath in sys.path:
    sys.path.append(SADPath)

from threading import Thread
from Queue import Queue
from World import *


from time import sleep, time


class ModelState:
    Uninitialised = 0
    Initialised = 1
    Running = 2
    Paused = 3
    Stopping = 4
    Error = -1


class Model(Thread):
    def __init__(self, active_queues):
        """
        init function for Model links queue
        @type active_queues: list
        @param active_queues: list of active queues from someone interface
        """
        Thread.__init__(self)
        self.simulatingWorld = World()
        #Shows, what world is simulating in the model
        self.passedModelIterations = 0
        #Info about how many iterations of main loop have passed
        self.modelIterationTick = 1
        #Info about time interval between loop iterations in c
        self.state = ModelState.Initialised
        #Model state from ModelState enum
        self.mailbox = Queue()
        #active queue to listen model commands
        active_queues.append(self.mailbox)
        self.active_queues = active_queues

    def run(self):
        """
        Thread function overriding
        @return: None
        """
        self.MainLoop()

    def stop(self):
        """
        Stop model function
        finish work, shutdown thread and join
        @return: None
        """
        self.active_queues.remove(self.mailbox)
        self.mailbox.put("shutdown")

    def MainLoop(self):
        """
        Main Model Loop
        @todo: rewrite with switch dictionary and run necessary according to it
        @return: None
        """
        while True:
            try:
                self.state = self.mailbox.get_nowait()
            except:
                pass
            if self.state == ModelState.Stopping:
                self.stop()
                break
            if self.state == ModelState.Paused:
                self.state = self.mailbox.get()
            if self.state == ModelState.Running:
                timeStart = time()
                self.simulatingWorld.MainLoopIteration()
                self.passedModelIterations += 1
                timeToSleep =self.modelIterationTick - (time() - timeStart)
                if(timeToSleep < 0.0001):
                    sys.stderr.write('MainLoop: To many calculations! timeToSleep = ' + str(timeToSleep))
                    #May be it must write to another place
                sleep(max(0, timeToSleep))