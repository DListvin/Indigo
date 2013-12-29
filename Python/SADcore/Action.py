__author__ = 'Zurk'

class Action:
    Name = []
    Duration = [] #How many turns it take
    Condition = [] # action applicability condition
    Actions = [] #List of actions
    Arguments = [] #List of arguments

    def Perform(self):
        pass


class Argument:
    def __init__(self, name, value, type):
        self.name = name
        self.value = value
        self.type = type


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