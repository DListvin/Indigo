__author__ = 'Zurk'
from SADcore.Agent import *
from SADcore.Property import *
from SADcore.Condition import *
from SADcore.Action import *
import os
import xml.etree.ElementTree as ET

class ParseModelXML:
    def __init__(self):
        self.actions = []
        self.agents = []
        self.properties = []

    def parse(self, path):
        xml = ET.parse(path)
        root = xml.getroot()
        getattr(self, 'parse' + root.tag)(root)

    def parseAgent(self, root):
        self.agents.append(Agent())
        Agent.Type = root.attrib['Type']
        for child in root:
            self.agents[-1].Properties = self.parseProperties(child)

    def parseProperties(self, root):
        properties = []
        for child in root:
            properties.append(self.parseProperty(child))
        return properties

    def parseProperty(self, root):
        #check that properties has not property with such name
        name = root.attrib['Name']
        for indx in range(1, len(self.properties)):
            if self.properties[indx].Name == name:
               break
        else:
            self.properties.append(Property(name))
            indx = -1

        for child in root:
            self.properties[indx].Propertires = self.parseSubProperties(child)
        return self.properties[indx]

    def parseSubProperties(self,root):
        subProperties = []
        for child in root:
            subProperties.append(getattr(self, 'parse' + child.tag)(child))
        return subProperties

    def parseCharacteristic(self,root):
        c = Characteristic(Property(root.attrib['Name']))
        c.Value = Property(root[0].attrib['Type'])
        try:
            c.Value = Property(root[0].attrib['Value'])
        except:
            pass
        try:
            c.Min = Property(root[0].attrib['Min'])
            c.Max = Property(root[0].attrib['Min'])
        except:
            pass
        return c

    #TODO: code parser for other subproperties

    def parseObjectivity(self,root):
        pass

    def parseAction(self, root):
        #check that actions has not action with such name
        name = root.attrib['Name']
        for indx in range(1, len(self.actions)):
            if self.actions[indx].Name == name:
               break
        else:
            self.actions.append(Action())
            indx = -1
        self.actions[indx].Name = name
        try:
            self.actions[indx].Duration = int(root[0].attrib['Duration'])
        except:
            self.actions[indx].Duration = 1

        switchParam = {
            'Arguments': self.actions[indx].Arguments,
            'Condition': self.actions[indx].Condition,
            'Actions': self.actions[indx].Actions
        }
        for child in root:
            switchParam[child.tag] = getattr(self, 'parse' + child.tag)(child)
        return self.actions[indx]

    def parseArguments(self,root):
        arguments = []
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
            actions.append(self.parseAction(child))
        return actions

    def parseCondition(self,root):
        conditions = []
        for child in root:
            conditions.append(getattr(self, 'parse' + child.tag)(child))
        return  conditions

    #TODO: code parser for all elementary actions
    #TODO: code parser for all elementary conditions

    def parseComparison(self, root):
        pass


P = ParseModelXML()
P.parse(os.getcwd() + '\WorldModel1\Moving.xml')
print([os.getcwd() + 'WorldModel\Move.xml'])