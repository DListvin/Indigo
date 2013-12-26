__author__ = 'Zurk'

class Action:
    Name = []
    Duration = [] #How many turns it take
    Condition = [] # action applicability condition
    Actions = [] #List of actions
    Arguments = [] #List of arguments

    def Do(self):
        pass


class CharacteristicChange(Action):
    def __init__(self, characteristic, delta):
        self.characteristic = characteristic
        self.delta = delta
    def Do(self):
        self.characteristic += self.delta # this methods(like += ) must be override in Characteristic


class CharacteristicSet(Action):
    def __init__(self, characteristic, value):
        self.characteristic = characteristic
        self.value = value
    def Do(self):
        self.characteristic = self.value # this methods(like += ) must be override in Characteristic


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


class Think(Action):
    def __init__(self, agent, brainType):
        self.agent = agent
        self.brainType = brainType

