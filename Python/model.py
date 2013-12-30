__author__ = 'Zurk'
from threading import Thread
import queue, sys
from SADcore.Agent import *
from SADcore.Property import *
from ParserXML import ParseModelXML
from time import sleep, time


class ModelState:
    Uninitialised = 0
    Initialised = 1
    Running = 2
    Paused = 3
    Stopping = 4
    Error = -1


class Model(Thread):
    simulatingWorld = None             #Shows, what world is simulating in the model
    passedModelIterations = 0          #Info about how many iterations of main loop have passed
    modelIterationTick = 1             #Info about time interval between loop iterations in c
    state = ModelState.Uninitialised   #Model state from ModelState enum
    storedActions = None               #Actions from agent to perform after thinking loop
    active_queues = []

    def __init__(self, active_queues):
        Thread.__init__(self)
        self.Init(active_queues)

    def Init(self, active_queues):
        self.simulatingWorld = World()
        self.state = ModelState.Initialised
        self.mailbox = queue.Queue()
        active_queues.append(self.mailbox)
        self.active_queues = active_queues

    def run(self):
        self.MainLoop()

    def stop(self):
        self.state = ModelState.Stopping
        self.active_queues.remove(self.mailbox)
        self.mailbox.put("shutdown")
        self.join()

    def MainLoop(self):
        while True:
            try:
                self.state = self.mailbox.get_nowait()
            except:
                pass
            if self.state == ModelState.Stopping:
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
    agents = []         #List of all agents in the world
    worldCommands = []  #List of Delegates, to add or Delete agents at right place in MainLoop
    curActions = []     #List of current actions in world at this iteration. Every iteration Refreshed
    templates = []      #world templates of all. Special class, that can create agents, actions, properties, that exists in world

    def __init__(self):
        self.Init()

    #Here we basically create world
    def Init(self):
    # Create one Man TOO LONG. Must be in other place in XML
        self.templates = ParseModelXML()
        self.templates.parse('WorldModel1')
        a = self.templates.createAgent('MovingMan')
        #TODO: here must be not self, but WordToAgent(self)
        a.myWorld = self
        self.agents.append(a)

    # Here is the one iteration of main loop
    def MainLoopIteration(self):
        curActions = []
        #Get All Periodicity specified actions(without thinking)
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
    # Function for agent, that ask to get action with name
    def getAction(self, name):
        return self.templates.createAction(name)

    def GetAgentById(self,id):
        return self.agents[id]

    #Deleting agent from the world completely. Nobody should use any other ways of deleting some agent, except this method
    def DeleteAgent(self):
        pass

    #Adding agent to the world. Nobody should use any other ways of adding some agent, except this method
    def AddAgent(self):
        pass