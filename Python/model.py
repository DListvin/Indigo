__author__ = 'Zurk'
from threading import Thread
import queue
from SADcore.Agent import *
from SADcore.Property import *
from SADcore.Condition import *
from SADcore.Action import *

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
    modelIterationTick = 500           #Info about time interval between loop iterations in mc
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
                self.simulatingWorld.MainLoopIteration()
                self.passedModelIterations += 1


class World:
    agents = []         #List of all agents in the world
    worldCommands = []  #List of Delegates, to add or Delete agents at right place in MainLoop

    def __init__(self):
        self.Init()

    #Here we basically create world
    def Init(self):
    # Create one Man TOO LONG. Must be in other place in XML
        a = Indigo()
        a.TypeName = 'Man'
        p = Characteristic('Folly')
        p.Value = 100
        p2 = Characteristic('Location')
        p2.Value = [0]
        Go = Action()
        Go.Name = 'Go'
        Go.Duration = 1
        Go.Actions = [ CharacteristicChange('Location', 1), CharacteristicChange('Location', -1)]
        c = NoComparison()
        c.CompType = 0
        Go.Condition = c
        p3 = Subjectivity('You shall pass!', 'Go')
        a.Properties = [p, p2, p3]
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
            action.Perform()

    def GetAgentById(self,id):
        return self.agents[id]

    # Clears agents FieldOfView and than fills it with agents and action within range of view
    def UpdateAgentFeelings(self):
        pass

    #Deleting agent from the world completely. Nobody should use any other ways of deleting some agent, except this method
    def DeleteAgent(self):
        pass

    #Adding agent to the world. Nobody should use any other ways of adding some agent, except this method
    def AddAgent(self):
        pass