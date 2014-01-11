__author__ = 'Zurk'
import sys, os
SADPath = os.path.abspath('./SADCore/')
if not SADPath in sys.path:
    sys.path.append(SADPath)

from threading import Thread
from Queue import Queue
from Agent import *
from Property import *
from WorldTemplates import WorldTemplates
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
                sleep(max(0,timeToSleep))


class World:
    """
    Just World
    @type templates:WorldTemplates
    """
    agents = []
    #List of all agents in the world
    worldCommands = []
    #List of Delegates, to add or Delete agents at right place in MainLoop
    #curActions = []
    #List of current actions in world at this iteration. Every iteration Refreshed
    templates = []
    #world templates of all. Special class, that can create agents, actions, properties, that exists in world

    def __init__(self):
        self.Init()

    def Init(self):
        """
        Here we basically create world
        @return: None
        """
        self.templates = WorldTemplates()
        self.templates.parseWorldFromXML('WorldModel1')
        a = self.templates.createAgent('MovingMan')
        #TODO: here must be not self, but WorldToAgent(self)
        a.myWorld = self
        self.agents.append(a)

    def MainLoopIteration(self):
        """
        Here is the one iteration of main loop
        @return: None
        """
        #Get All Periodicity specified actions(without thinking)
        curActions = []
        for agent in self.agents:
            curActions.append(agent.GetActionsByType(Periodicity))
        #Execute it
        for action in curActions:
            if action:
                action.Perform()
        #Get All feeling specified actions
        curActions = []
        for agent in self.agents:
            curActions.append(agent.GetActionsByType(Feeling))
        #Execute it
        for action in curActions:
            if action:
                action.Perform()
        #Get All thinking specified actions
        curActions = []
        for agent in self.agents:
            if isinstance(agent, Indigo):
                curActions.append(agent.Think())
        #Non-conflict execution
        for action in curActions:
            if action:
                action.Perform()
    #
    def getAction(self, name):
        """
        Function for agent, that ask to get action with name
        @param name: action name
        @type:str
        @return: action
        @rtype:Action
        """
        return self.templates.createAction(name)

    def GetAgentById(self,id):
        return self.agents[id]

    def DeleteAgent(self):
        """
        Deleting agent from the world completely. Nobody should use any other ways of deleting some agent, except this method
        @return:None
        """
        pass

    def AddAgent(self):
        """
        Adding agent to the world. Nobody should use any other ways of adding some agent, except this method
        @return:None
        """
        pass