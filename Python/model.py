__author__ = 'Zurk'
from threading import Thread
import queue


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
    agents = None         #List of all agents in the world
    actions = None        #List of all actions, that must be performed (refreshing each loop iteration)
    worldCommands = None  #List of Delegates, to add or Delete agents at right place in MainLoop

    def __init__(self):
        self.Init()

    #Here we basically create world
    def Init(self):
    # Create one Man TOO LONG. Must be in other place
        a = Agent()
        a.TypeName = 'Man'
        p = Characteristic('Stupidity')
        p.Value = 100
        p2 = Characteristic('Location')
        p2.Value = [0]
        Go = ActionMove()
        Go.Duration = 2
        Go.Name = 'Go'
        c = Comparison()
        c.CompType = 0
        Go.Condition = c
        p3 = Subjectivity('You shall pass!', Go)
        a.Properties = [p, p2, p3]
        self.agents = [self.agents, a]

    # Here is the one iteration of main loop
    def MainLoopIteration(self):
        #Get All Periodicity and Reactivity specified actions(without thinking)
        #Execute it
        #Get All feeling specified actions
        #Execute it
        #Get All thinking specified actions
        #Non-conflict execution
        pass

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