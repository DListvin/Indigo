__author__ = 'Zurk'

class Property:
    def __init__(self, name):
        self.Name = name

    Name = [] #Unique name
    Properties = [] #One or many subproperties or properties

    def getAllByType(self, type):
        if self.Properties == []:
            if isinstance(self, type):
                return self
        allObjectivity = []
        for prop in self.Properties:
            if isinstance(prop, type):
                allObjectivity.append(prop.getAllByType(type))
        return allObjectivity

    def getByName(self, name):
        if self.Properties == []:
            if self.Name == name:
                return self
            else:
                return None
        for prop in self.Properties:
            ans = prop.getByName(name)
            if not (ans is None):
                return ans
        return None



class Characteristic(Property):
    Type = [] # Variable type of value
    Value = []
    Max = None
    Min = None
    def __init__(self, name):
        Property.__init__(self, name)

    def __iadd__(self, other):
        self.Value += other
        if not (self.Max is None):
            self.Value = min(self.max, self.Value)
        if not (self.Min is None):
            self.Value = max(self.min, self.Value)

    def __isub__(self, other):
        self.Value -= other
        if not (self.Max is None):
            self.Value = min(self.max, self.Value)
        if not (self.Min is None):
            self.Value = max(self.min, self.Value)


class Objectivity(Property):
    actionName = [] #Name of action which may be applied to agent

    def __init__(self, name, action):
        Property.__init__(self, name)
        self.actionName = action


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

