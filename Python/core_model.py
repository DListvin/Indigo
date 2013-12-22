__author__ = 'Zurk'
from SAD_core import *


class ModelState:
    Uninitialised = 0
    Initialised = 1
    Running = 2
    Paused = 3
    Stopping = 4
    Error = -1


class Core_model:
    simulatingWorld = None             #Shows, what world is simulating in the model
    passedModelIterations = 0       #Info about how many iterations of main loop have passed
    modelIterationTick = 500          #Info about time interval between loop iterations in mc
    state = ModelState.Uninitialised   #Model state from ModelState enum
    storedActions = None               #Actions from agent to perform after thinking loop

    def __init__(self):
        self.Init()

    def Init(self):
        simulatingWorld = World()

    def Start(self):
        pass

    def Pause(self):
        pass

    def Resume(self):
        pass

    def Stop(self):
        pass

    def MainLoop(self):
        while True:
            if self.state == ModelState.Stopping:
                break
            if self.state == ModelState.Paused:
                pass
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
    # Create one Stupid  Man TOO LONG. Must be in other place
        a = Agent()
        a.TypeName = 'Man';
        p = Characteristic('Stupidity')
        p.Value = 100
        p2 = Characteristic('Location')
        p2.Value = [0]
        Go = ActionMove()
        Go.Duration = 2;
        Go.Name = 'Go';
        c = Comparison()
        c.CompType = 0;
        Go.Condition = c;
        p3 = Subjectivity('You shall pass!', Go)
        a.Properties = [p, p2, p3]
        self.agents = [self.agents, a]

        state = ModelState.Initialised

    # Here is the one iteration of main loop
    def MainLoopIteration(self):
        pass

    # Clears agents FieldOfView and than fills it with agents and action within range of view
    def UpdateAgentFeelings(self):
        pass

    #Deleting agent from the world completely. Nobody should use any other ways of deleting some agent, except this method
    def DeleteAgent(self):
        pass

    #Adding agent to the world. Nobody should use any other ways of adding some agent, except this method
    def AddAgent(self):
        pass