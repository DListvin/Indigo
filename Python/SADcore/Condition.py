__author__ = 'Zurk'
from model import Model


class Condition:
    Conditions = [] #List of conditions
    Args = []
    GetAgentById = Model.simulatingWorld.GetAgentById # This is function from world to have access to all agents

    def Calculate(self):
        res = True
        for i in self.Conditions:
            if i.Calculate() != True:
                res = False
        return res


class HaveProperty(Condition):
    def Calculate(self):
        pass


class Comparison(Condition):
    CompType = []

    def Calculate(self):
        #swich CompType
        return self.GetAgentById(self.Args[0]).GetCharacteristicByName(self.Args[2]).Value <= \
               self.GetAgentById(self.Args[1]).GetCharacteristicByName(self.Args[2]).Value