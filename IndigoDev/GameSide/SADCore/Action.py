__author__ = 'Zurk'
from Condition import Condition
from Property import Characteristic

class Arguments(dict):
    """
    Special dict for arguments
    Has specialized functions to access argument or its value
    Or have not...
    """


class Argument:
    """
    class of action and condition arguments
    now it is useless
    """
    def __init__(self, name, value, type):
        self.name = name
        self.value = value
        self.type = type


class Action:
    """
    class of Action
    """
    def __init__(self, name=None):
        self.name = name
        #Action name
        self.duration = 1
        #How many turns it take
        self.condition = Condition()
        #action applicability condition
        self.actions = []
        #List of actions
        self.arguments = Arguments()
        #Arguments list of arguments

    def __call__(self):
        self.Perform()

    def Perform(self):
        """
        Main Action function
        @return:Nome
        """
        if self.condition:
            if not self.condition.Calculate():
                return
        for action in self.actions:
                #pass arguments to next action
                action.arguments = self.arguments
                if action.condition:
                    action.condition.arguments = self.arguments
                action()


#TODO: must be recode all to Arguments no characteristic or delta
class CharacteristicChange(Action):
    """
    elementary action CharacteristicChange
    """
    def __init__(self):
        Action.__init__(self)
        self.ChName = None
        self.Modifier = None

    def Perform(self):
        # it must be checked. that it all is links and += will works

        argParse = self.ChName.split(".", 1)
        ch = self.arguments[argParse[0]][argParse[1]]
        if type(self.Modifier) is int:
            ch += self.Modifier
        else:
            modParse = self.Modifier.split(".", 1)
            ch += self.arguments[modParse[0]][modParse[1]]




class CharacteristicSet(Action):
    """
    elementary action CharacteristicSet
    """
    def Perform(self):
        pass


class AddAgent(Action):
    def Perform(self):
        pass


class DeleteAgent(Action):
    def __init__(self, agentName):
        Action.__init__(self)
        self.agentName = agentName
    def Perform(self):
        agent = self.arguments[self.agentName]
        agent.myWorld.DeleteAgent(agent)


class AddToMemory(Action):
    def Perform(self):
        pass