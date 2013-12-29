__author__ = 'Zurk'
from SADcore.Property import *
from SADcore.Action import Argument

class Agent:
    Type = [] #Name of type (Man, dog, rain, axe...)
    ID = [] #Unique id, primary key
    Properties = [] #List of properties
    myWorld = [] # world Interface to agent

    def GetPropertyByName(self, name):
        for prop in self.Properties:
                ch = prop.getByName(name)
                if not (ch is None):
                    return ch
        #may be here we must raise exception
        return None

    def GetActionsByType(self, type):
        result = []
        for p in self.Properties:
            if isinstance(p, type):
                result.append(p)
        return result

#Agent with brain. Can rename later
class Indigo(Agent):
    init = False
    def Think(self):
        if not self.init:
            self.initSubjectivity()
            self.init = True
        for p in self.posibleActions:
            if p.actionName == 'Move':
                action = self.myWorld.getAction(p.actionName)
                action.arguments.set('self', self)
                action.arguments.set('directionX', 10)
                action.arguments.set('directionY', 10)
                return action

    def initSubjectivity(self):
        self.posibleActions = []
        for prop in self.Properties:
            self.posibleActions += prop.getAllByType(Objectivity)