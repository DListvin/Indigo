__author__ = 'Zurk'
from WorldTemplates import WorldTemplates
from MapEngine import *
from SADCore.Agent import *
from SADCore.Indigo import *
from SADCore.Property import *



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
    modifiers = []
    # list of functions for world contents(list of Agents) modification.
    def __init__(self, seed):
        """
        Here we basically create world
        @return: None
        """
        self.map = Map(seed)
        self.templates = WorldTemplates()
        self.templates.parseWorldFromXML('WorldModelHex')
        a = self.templates.CreateAgent('MovingMan')
        #TODO: here must be not self, but WorldToAgent(self)... or may be not
        a.myWorld = self
        self.agents.append(a)

    def MainLoopIteration(self):
        """
        Here is the one iteration of main loop
        @return: None
        """

        curActions = []
        #Get All Periodicity specified actions(without thinking)
        for agent in self.agents:
            curActions.extend(agent.GetActionsByType(Periodicity))

        #Get All feeling specified actions
        for agent in self.agents:
            curActions.extend(agent.GetActionsByType(Feeling))

        #Get All thinking specified actions
        for agent in self.agents:
            if isinstance(agent, Indigo):
                curActions.append(agent.Think())

        #Execute all actions in list. Order is important!
        for action in curActions:
            if action:
                action.Perform()

        for mod in self.modifiers:
            mod()
        self.modifiers = []

    def getAction(self, name):
        """
        Function for agent, that ask to get action with name
        @param name: action name
        @type:str
        @return: action
        @rtype:Action
        """
        return self.templates.CreateAction(name)

    def GetAgentById(self, id):
        return self.agents[id]

    def DeleteAgent(self, agent):
        """
        Deleting agent from the world completely. Nobody should use any other ways of deleting some agent, except this method
        @return:None
        """
        self.modifiers.append(lambda: self.agents.remove(agent))

    def AddAgent(self):
        """
        Adding agent to the world. Nobody should use any other ways of adding some agent, except this method
        @return:None
        """
        pass

    def __str__(self):
        if not self.agents:
            return 'World: Nobody here...'
        else:
            return str(self.agents[0])
