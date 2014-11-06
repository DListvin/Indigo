__author__ = 'Zurk'
from Condition import Condition
from Property import Characteristic

class Arguments(list):
    """
    Special list for arguments
    Has specialized functions to access argument or its value
    """

    def set(self, name, value):
        for arg in self:
            if arg.name == name:
                #TODO: type checking
                arg.value = value
                break
        else:
            raise Exception('Arguments.set: No such argument name')

    def getValue(self, name):
        try:
            nameParse = name.split('.', 1)
            if len(nameParse) == 1:
                arg = self.getArgument(name)
                return arg.value
            else:
                argVal = self.getArgument(nameParse[0]).value
                return argVal.GetPropertyByName(nameParse[1]).Value
        except:
            raise Exception('Arguments.getValue: No such argument name {0}'.format(name))

    def getValueByType(self, type):
        try:
            return self.getArgumentByType(type).value
        except:
            raise Exception('Arguments.getValueByType: No argument with such type')

    def getArgument(self, name):
        for arg in self:
            if arg.name == name:
                return arg
        else:
            return None

    def getArgumentByType(self, type):
        for arg in self:
            if arg.type == type:
                return arg
        else:
            return None


class Argument:
    """
    class of action and condition arguments
    """
    def __init__(self, name, value, type):
        self.name = name
        self.value = value
        self.type = type

    #Here we can make type comparison
    def __gt__(self, other):
        """
        @type other: Argument
        """
        return self.value > other.value

    def __lt__(self, other):
        """
        @type other: Argument
        """
        return self.value < other.value

    def __eq__(self, other):
        """
        @type other: Argument
        """
        return self.value == other.value

    def __ne__(self, other):
        """
        @type other: Argument
        """
        return self.value != other.value


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
        ch = self.arguments.getValue(argParse[0]).GetPropertyByName(argParse[1])
        if type(self.Modifier) is int:
            ch += self.Modifier
        else:
            modParse = self.Modifier.split(".", 1)
            ch += self.arguments.getValue(modParse[0]).GetPropertyByName(modParse[1])




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
        agent = self.arguments.getValue(self.agentName)
        agent.myWorld.DeleteAgent(agent)


class AddToMemory(Action):
    def Perform(self):
        pass