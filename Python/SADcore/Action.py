__author__ = 'Zurk'

class Action:
    Name = []
    Duration = [] #How many turns it take
    Condition = [] # action applicability condition
    Actions = [] #List of actions
    arguments = [] #List of arguments

    #TODO: code Perform function
    def Perform(self):
        pass

class Arguments(list):
    def set(self, name, value):
        for arg in self:
            if arg.name == name:
                #TODO: type checking
                arg.value = value
                break
        else:
            raise Exception('Arguments.set: No such argument name')
class Argument:
    def __init__(self, name, value, type):
        self.name = name
        self.value = value
        self.type = type

#TODO: must be recode all to Arguments no characteristic or delta
class CharacteristicChange(Action):
    def __init__(self):
        self.characteristic = []
        self.delta = []
    def Perform(self):
        self.characteristic += self.delta # this methods(like += ) must be override in Characteristic

class CharacteristicSet(Action):
    def __init__(self, characteristic, value):
        self.characteristic = characteristic
        self.value = value
    def Perform(self):
        self.characteristic = self.value


class AddAgent(Action):
    def __init__(self, agent):
        self.agent = agent


class DeleteAgent(Action):
    def __init__(self, agent):
        self.agent = agent


class AddToMemory(Action):
    def __init__(self, agent, flashback):
        self.agent = agent
        self.flashback = flashback