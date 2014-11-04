from threading import Thread
from multiprocessing import Pipe
from time import sleep, time
from World import *
import sys


class ModelState:
    Uninitialised = 0
    Initialised = 1
    Running = 2
    Paused = 3
    Stopping = 4
    Error = -1

class ModelMsg:
    Refresh = 0


class Model(Thread):
    def __init__(self, seed, activeQueues, modelRefresh):
        """
        init function for Model links queue
        @type activeQueues: list
        @param activeQueues: list of active queues from someone interface
        """
        Thread.__init__(self)
        self.simulatingWorld = World(seed)
        #Shows, what world is simulating in the model
        self.passedModelIterations = 0
        #Info about how many iterations of main loop have passed
        self.modelIterationTick = 1
        #Info about time interval between loop iterations in c
        self.state = ModelState.Initialised
        #Model state from ModelState enum
        mailbox = Pipe()
        self.mailbox = mailbox[0]
        #active queue to listen model commands
        activeQueues.append(mailbox[1])
        self.activeQueues = activeQueues
        # function that runs for model refresh on client side for each model tick
        self.modelRefresh = modelRefresh

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
        self.activeQueues.remove(self.mailbox)
        self.mailbox.send("shutdown")

    def MainLoop(self):
        """
        Main Model Loop
        @todo: rewrite with switch dictionary and run necessary according to it
        @return: None
        """
        while True:
            if self.mailbox.poll():
                self.state = self.mailbox.recv()
            if self.state == ModelState.Stopping:
                self.stop()
                break
            if self.state == ModelState.Paused:
                self.state = self.mailbox.recv()
            if self.state == ModelState.Running:
                timeStart = time()
                self.simulatingWorld.MainLoopIteration()
                self.modelRefresh()
                self.passedModelIterations += 1
                timeToSleep =self.modelIterationTick - (time() - timeStart)
                if(timeToSleep < 0.0001):
                    sys.stderr.write('MainLoop: To many calculations! timeToSleep = ' + str(timeToSleep))
                    #May be it must write to another place
                sleep(max(0, timeToSleep))