__author__ = 'Zurk'
import sys, os
SADPath = os.path.abspath('./SADCore/')
if not SADPath in sys.path:
    sys.path.append(SADPath)
from Property import *


class Agent:
    """
    Base Class in SADcore.
    Everything is agents agents are everything.
    """
    def __init__(self):
        self.Type = []        #Name of type (Man, dog, rain, axe...)
        self.ID = []          #Unique id, primary key
        self.Properties = []  #List of properties
        self.myWorld = []     # world Interface to agent

    def GetPropertyByName(self, name):
        """
        Get property by name of agent if exist
        @param name: property name
        @type name:str
        @return: found property or None if agent has not such property
        @rtype:Property
        """
        for prop in self.Properties:
            ch = prop.getByName(name)
            if not (ch is None):
                return ch
                #may be here we must raise exception
        return None

    def GetActionsByType(self, type):
        """
        Get actions By Type of Property like Feeling or Periodicity
        @param type: type of Property
        @return: all found actions
        """
        result = []
        for p in self.Properties:
            if isinstance(p, type):
                result.append(p)
        return result


class Indigo(Agent):
    """
    Agent with brain. Can rename later
    """
    def __init__(self):
        Agent.__init__(self)
        self.init = False

    def Think(self):
        if not self.init:
            self.initSubjectivity()
            self.init = True
            self.steps = 0
        for p in self.posibleActions:
            if p.actionName == 'Move':
                action = self.myWorld.getAction(p.actionName)
                action.arguments.set('self', self)
                if self.steps < 5:
                    action.arguments.set('directionX', 1)
                    action.arguments.set('directionY', 0)
                elif self.steps < 10:
                    action.arguments.set('directionX', 0)
                    action.arguments.set('directionY', 1)
                elif self.steps < 15:
                    action.arguments.set('directionX', -1)
                    action.arguments.set('directionY', 0)
                elif self.steps < 20:
                    action.arguments.set('directionX', 0)
                    action.arguments.set('directionY', -1)
                else:
                    self.steps = 0
                    action.arguments.set('directionX', 0)
                    action.arguments.set('directionY', 0)
                self.steps += 1
                for p in self.Properties:
                    temp = p.getByName('LocationX')
                    if not (temp is None):
                        print(temp)
                    temp = p.getByName('LocationY')
                    if not (temp is None):
                        print(temp)
                return action

    def initSubjectivity(self):
        self.posibleActions = []
        for prop in self.Properties:
            self.posibleActions += prop.getAllByType(Objectivity)