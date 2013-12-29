__author__ = 'Zurk'

class Property:
    def __init__(self, name):
        self.Name = name

    Name = [] #Unique name
    Properties = [] #One or many subproperties or properties

    def getAllObjectivity(self):
        if self.Properties == []:
            return self.Action
        allObjectivity = []
        for prop in self.Properties:
            if isinstance(prop, Objectivity):
                allObjectivity.append(prop.getAllObjectivity())
        return allObjectivity


class Characteristic(Property):
    Type = [] # Variable type of value
    Value = []

    def __init__(self, name):
        Property.__init__(self, name)


class Objectivity(Property):
    Action = [] #Name of action which may be applied to agent

    def __init__(self, name, action):
        Property.__init__(self, name)
        self.Action = action


class Subjectivity(Property):
    Action = [] #Name of action which may be applied by agent

    def __init__(self, name, action):
        Property.__init__(self, name)
        self.Action = action


class Reactivity(Property):
    TriggerAction = [] #Name or Type of action, which will lead to usage
    Action = [] #Name of action, which will follow trigger
    ActionArgs = [] #Args of Action ! My be incapsulated in Action


class Periodicity(Property):
    TimeInterval = [] # Peridiosity
    Action = [] #action, which will be repeated


class Feeling(Property):
    Action = [] #Name of action, which will be repeated before every turn as feeling


class Memory(Property):
    pass

