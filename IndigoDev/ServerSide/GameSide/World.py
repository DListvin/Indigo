__author__ = 'Zurk'
import sys, os
SADPath = os.path.abspath('./GameSide/SADCore/')
if not SADPath in sys.path:
    sys.path.append(SADPath)

from WorldTemplates import WorldTemplates
from Agent import *
from Property import *
from MapEngine import *


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
        self.map = Map(1)
        self.templates = WorldTemplates()
        self.templates.parseWorldFromXML('./GameSide/WorldModelHex')
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

    def GetAgentById(self, id):
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