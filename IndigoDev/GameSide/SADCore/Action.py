__author__ = 'Zurk'
from Condition import Condition

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
            return self.getArgument(name).value
        except:
            raise Exception('Arguments.getValue: No such argument name')

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
    def __init__(self):
        self.name = []
        #Action name
        self.duration = []
        #How many turns it take
        self.condition = Condition()
        #action applicability condition
        self.actions = []
        #List of actions
        self.arguments = Arguments()
        #Arguments list of arguments

    def Perform(self):
        """
        Main Action function
        @return:Nome
        """
        if self.condition:
            if not self.condition.Calculate():
                return
        for action in self.actions:
            if isinstance(action, CharacteristicChange):
                agent = self.arguments.getValueByType('Agent')
                action.arguments.append(Argument('Agent', agent, 'Agent'))
            else:
                for arg in action.arguments:
                    action.arguments.set(arg.name, self.arguments.getValue(arg.name))
                for arg in action.condition.arguments:
                    action.condition.arguments.set(arg.name, self.arguments.getValue(arg.name))
            action.Perform()


#TODO: must be recode all to Arguments no characteristic or delta
class CharacteristicChange(Action):
    """
    elementary action CharacteristicChange
    """
    def __init__(self):
        Action.__init__(self)

    def Perform(self):
        # it must be checked. that it all is links and += will works
        agent = self.arguments.getValueByType('Agent')
        ch = agent.GetPropertyByName(self.arguments.getValueByType('Characteristic'))
        ch += self.arguments.getValueByType('int')


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
    def Perform(self):
        pass


class AddToMemory(Action):
    def Perform(self):
        pass