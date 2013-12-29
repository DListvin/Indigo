__author__ = 'Zurk'
from SADcore.Property import *
from SADcore.Action import Argument

class Agent:
    Type = [] #Name of type (Man, dog, rain, axe...)
    ID = [] #Unique id, primary key
    Properties = [] #List of properties
    myWorld = [] # world Interface to agent

    def GetCharacteristicByName(self, name):
        for p in self.Properties:
            if isinstance(p, Characteristic):
                if p.Name == name:
                    return p
                    break

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
            if p == 'Move':
                action = self.myWorld.getAction(p)
                action.Arguments.set('self', self)
                action.Arguments.set('directionX', 10)
                action.Arguments.set('directionY', 10)
                return action

    def initSubjectivity(self):
        self.posibleActions = []
        for prop in self.Properties:
            self.posibleActions += prop.getAllObjectivity()