#-------------------------------------------------------------------------------
# Name:        SAD_core
# Purpose:
#
# Author:      Dlistvin
#
# Created:     10.11.2013
# Copyright:   (c) Dlistvin 2013
# Licence:     <your licence>
#-------------------------------------------------------------------------------

from xml.dom import minidom

agents = [];


def main():
    a = Agent()
    a.TypeName = 'Man';
    p = Characteristic('Stupidity')
    p.Value = 100
    p2 = Characteristic('Location')
    p2.Value = [0]

    Go = ActionMove()
    Go.Duration = 2
    Go.Name = 'Go'
    c = Comparison()
    c.CompType = 0
    Go.Condition = c

    p3 = Subjectivity('You shall pass!', Go)

    a.Properties = [p, p2, p3]

    agents = [agents, a]
    print(a.Properties[1].Value)
    action = a.Think()
    action.Do();
    action = a.Think()
    action.Do();
    action = a.Think()
    action.Do();
    print(a.Properties[1].Value)

    pass


def GetAgentById(id):
    return agents[id];


class Agent:
    TypeName = [] #Name of type (Man, dog, rain, axe...)
    ID = [] #Unique id, primary key
    Properties = [] #List of properties

    def GetCharacteristicByName(self, name):
        for p in self.Properties:
            if isinstance(p, Characteristic):
                if p.Name == name:
                    return p
                    break

    def Think(self):
        for p in self.Properties:
            if isinstance(p, Subjectivity):
                a = p.Action
                a.Arguments.clear()
                a.Arguments.append(self)
                a.Arguments.append(10)
                return a
                break


class Action:
    Name = []
    Duration = [] #How many turns it take
    Condition = [] # action applicability condition
    Actions = [] #List of actions
    Arguments = [] #List of arguments

    def Do(self):
        pass

# this is wrong
class ActionMove(Action):
    def __init__(self, ):
        pass

    def Do(self):
        l = self.Arguments[0].GetCharacteristicByName("Location").Value[0]
        self.Arguments[0].GetCharacteristicByName("Location").Value[0] = l + self.GetDirection(l, self.Arguments[1]);

    def GetDirection(self, begin, end):
        return 1;


class Condition:
    Conditions = [] #List of conditions

    def Calculate(self):
        res = True
        for i in self.Conditions:
            if i.Calculate() != True:
                res = False
        return res


class Comparison(Condition):
    CompType = []

    def Calculate(agentId1, agentId2, CharacteristicName1, CharacteristicName2):
        #swich CompType
        return GetAgentById(agentId1).GetCharacteristicByName(CharacteristicName1).Value <= \
               GetAgentById(agentId2).GetCharacteristicByName(CharacteristicName2).Value


class Property:
    def __init__(self, name):
        self.Name = name

    Name = [] #Unique name
    Properties = [] #One or many subproperties


class Characteristic(Property):
    Type = [] # Variable type of value
    Value = []


class Objectivity(Property):
    def __init__(self, name, action):
        self.Name = name
        self.Action = action

    Action = [] #Name of action which may be applied to agent
    pass


class Subjectivity(Property):
    def __init__(self, name, action):
        self.Name = name
        self.Action = action

    Action = [] #Name of action which may be applied by agent
    pass


class Reactivity(Property):
    TriggerAction = [] #Name or Type of action, which will lead to usage
    Action = [] #Name of action, which will follow trigger
    ActionArgs = [] #Args of Action ! My be incapsulated in Action
    pass


class Periodicity(Property):
    TimeInterval = [] # Peridiosity
    Action = [] #Name of action, which will be repeated
    ActionArgs = [] #Args of Action ! May be incapsulated in Action
    pass


class Feeling(Property):
    Action = [] #Name of action, which will be repeated before every turn as feeling
    pass


class Memory(Property):
    pass


if __name__ == '__main__':
    main()
