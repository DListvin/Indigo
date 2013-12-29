__author__ = 'Zurk'
from SADcore.Agent import *
from SADcore.Property import *
from SADcore.Condition import *
from SADcore.Action import *
from copy import deepcopy
import glob
import xml.etree.ElementTree as ET

class ParseModelXML:
    def __init__(self):
        self.actions = []
        self.agents = []
        self.properties = []

    def parse(self, folderPath):
        paths = glob.glob(folderPath +'\*.xml')
        for path in paths:
            xml = ET.parse(path)
            root = xml.getroot()
            getattr(self, 'parse' + root.tag)(root)

    def parseAgent(self, root):
        try:
            intellectual = root.attrib['intellectual'] == 'yes'
        except:
            intellectual = False
        if intellectual:
            a = Indigo()
        else:
            a = Agent()
        Agent.Type = root.attrib['Type']
        for child in root:
            a.Properties = self.parseProperties(child)
        self.agents.append(a)

    def parseProperties(self, root):
        properties = []
        for child in root:
            properties.append(self.parseProperty(child))
        return properties

    def parseProperty(self, root):
        #check that properties has not property with such name
        name = root.attrib['Name']
        for indx in range(0, len(self.properties)):
            if self.properties[indx].Name == name:
               break
        else:
            self.properties.append(Property(name))
            indx = len(self.actions) - 1

        for child in root:
            self.properties[indx].Properties = self.parseSubProperties(child)
        return self.properties[indx]

    def parseSubProperties(self,root):
        subProperties = []
        for child in root:
            subProperties.append(getattr(self, 'parse' + child.tag)(child))
        return subProperties

    def parseCharacteristic(self,root):
        c = Characteristic(root.attrib['Name'])
        c.Type = root[0].attrib['Type']
        try:
            c.Value = int(root[0].attrib['Default'])
        except:
            #TODO: generate warning: no Default value
            c.Value = None
        try:
            c.Min = Property(root[0].attrib['Min'])
            c.Max = Property(root[0].attrib['Min'])
        except:
            pass
        return c

    def parseObjectivity(self,root):
        obj = Objectivity([], root.attrib['Action'])
        return obj

    #TODO: code parser for other subproperties

    def parseAction(self, root):
        #check that actions has not action with such name
        name = root.attrib['Name']
        for indx in range(0, len(self.actions)):
            if self.actions[indx].Name == name:
               break
        else:
            self.actions.append(Action())
            indx = len(self.actions) - 1
        self.actions[indx].Name = name
        try:
            self.actions[indx].Duration = int(root.attrib['Duration'])
        except:
            self.actions[indx].Duration = 1

        switchParam = {
            'Arguments': [],
            'Condition': [],
            'Actions': []
        }
        for child in root:
            switchParam[child.tag] = getattr(self, 'parse' + child.tag)(child)

        self.actions[indx].arguments = switchParam['Arguments']
        self.actions[indx].Condition = switchParam['Condition']
        self.actions[indx].Actions =  switchParam['Actions']

        return self.actions[indx]

    def parseArguments(self,root):
        arguments = Arguments()
        for child in root:
            arguments.append(self.parseArgument(child))
        return arguments

    def parseArgument(self,root):
        argument = Argument(root.attrib['Name'], [], root.attrib['Type'])
        try:
            argument.value = root.attrib['Value']
        except:
            pass
        return argument

    def parseActions(self,root):
        actions = []
        for child in root:
            actions.append(getattr(self, 'parse' + child.tag)(child))
        return actions

    def parseCondition(self,root):
        conditions = []
        for child in root:
            conditions.append(getattr(self, 'parse' + child.tag)(child))
        return  conditions

    #TODO: code parser for all elementary actions
    #TODO: code parser for all elementary conditions
    def parseComparison(self, root):
        c = Comparison()
        c.CompType = root.attrib['Sign']
        c.Args.append(Argument(root.attrib['Arg1'], [],[]))
        try:
            Arg2Value = int(root.attrib['Arg2'])
            c.Args.append(Argument('const', Arg2Value, 'int'))
        except:
            Arg2Name = root.attrib['Arg2']
            c.Args.append(Argument(Arg2Name, [],[]))
        return  c

    #TODO: here is ambiguity do not know from which agent take property
    def parseCharacteristicChange(self,root):
        c = CharacteristicChange()
        c.arguments.append(Argument([], root.attrib['PropName'], 'Characteristic'))
        c.arguments.append(Argument('Modifier', int(root.attrib['Modifier']), 'int'))
        return c

    def createAgent(self, type):
        for agent in self.agents:
            if agent.Type == type:
                return self.createAgentFromTemplate(agent)
        raise Exception('ParseModelXML.createAgent: No such agent type')

    def createAction(self, name):
        for action in self.actions:
            if action.Name == name:
                return self.createActionFromTemplate(action)
        raise Exception('ParseModelXML.createAction: No action with such name')

    def createAgentFromTemplate(self, agent):
        return deepcopy(agent)

    def createActionFromTemplate(self, action):
        return deepcopy(action)