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