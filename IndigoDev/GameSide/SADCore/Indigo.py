from Agent import Agent
from Property import Objectivity


class Indigo(Agent):
    """
    Agent with brain. Can rename later
    """
    def __init__(self):
        Agent.__init__(self)
        self.go = False

    def Think(self):
        if not self.go:
            self.initSubjectivity()
            self.go = True
            self.steps = 0

        if self.steps < 5:
            action = self.myWorld.getAction('MoveUpRight')
            action.arguments.set('self', self)
        elif self.steps < 10:
            action = self.myWorld.getAction('MoveRight')
            action.arguments.set('self', self)
        elif self.steps < 15:
            action = self.myWorld.getAction('MoveDownLeft')
            action.arguments.set('self', self)
        elif self.steps < 20:
            action = self.myWorld.getAction('MoveLeft')
            action.arguments.set('self', self)
        else:
            self.steps = 0
            action = None
        self.steps += 1
#TODO: Zurk should probably delete it
#        for p in self.Properties:
#            temp = p.getByName('LocationX')
#            if not (temp is None):
#                print(temp)
#            temp = p.getByName('LocationY')
#            if not (temp is None):
#                print(temp)
#            temp = p.getByName('LocationZ')
#            if not (temp is None):
#                print(temp)
            if not (temp is None):
                print(temp)
            temp = p.getByName('Satiety')
            if not (temp is None):
                print(temp)
            temp = p.getByName('Health')
        return action

    def initSubjectivity(self):
        self.posibleActions = []
        for prop in self.Properties:
            self.posibleActions += prop.getAllByType(Objectivity)

    def ToJson(self):
        if self.Type == "MovingMan":
            return '{"t":' + str(1) + '}'
        return '{"t":' + str(self.Type) + '}'