from GameSide.SADCore.Action import *
from GameSide.SADCore.Condition import *
from GameSide.SADCore.Indigo import *
from GameSide.SADCore.Property import *
from copy import deepcopy
import os
import glob
import xml.etree.ElementTree as et

class WorldTemplates:
    """
    Class of world templates
    it can parse XML world presentation to internal presentation(templates)
    and make actions and agents from template
    """
    def __init__(self):
        self.actions = []
        self.agents = []
        self.properties = []

    def parseWorldFromXML(self, folderPath):
        """
        Main XML world presentation parser function
        @param folderPath: path to folder with world. Take from it only .xml files
        @return: None
        """

        paths = glob.glob(os.path.dirname(os.path.realpath(__file__)) + '/' + folderPath +'/*.xml')
        for path in paths:
            xml = et.parse(path)
            root = xml.getroot()
            getattr(self, 'parse' + root.tag)(root)

    def parseAgent(self, root):
        """
        @rtype: Agent
        """
        #TODO: refactor with key in d construction
        try:
            intellectual = root.attrib['intellectual'] == 'yes'
        except:
            intellectual = False
        if intellectual:
            a = Indigo()
        else:
            a = Agent()
        a.Type = root.attrib['Type']
        for child in root:
            a.Properties = self.parseProperties(child)
        self.agents.append(a)

    def parseProperties(self, root):
        properties = []
        for child in root:
            properties.append(self.parseProperty(child))
        return properties

    def parseProperty(self, root):
        """
        @rtype: Property
        """
        #check that properties has not property with such name
        name = root.attrib['Name']
        for indx in range(0, len(self.properties)):
            if self.properties[indx].Name == name:
               break
        else:
            self.properties.append(Property(name))
            indx = - 1

        for child in root:
            self.properties[indx].Properties = self.parseSubProperties(child)
        return self.properties[indx]

    def parseSubProperties(self, root):
        """
        @rtype: Property
        """
        subProperties = []
        for child in root:
            subProperties.append(getattr(self, 'parse' + child.tag)(child))
        return subProperties

    def parseCharacteristic(self, root):
        """
        @rtype: Characteristic
        """
        c = Characteristic(root.attrib['Name'])
        chValue = root.attrib
        if len(root):
            chValue = root[0].attrib
        if 'Type' in chValue:
            c.Type = chValue['Type']
        if 'Default' in chValue:
            c.Value = int(chValue['Default'])
        else:
            #TODO: generate warning: no Default value
            pass
        if 'Min' in chValue:
            c.Min = int(chValue['Min'])
        if 'Max' in chValue:
            c.Max = int(chValue['Max'])
        return c

    def parseObjectivity(self, root):
        """
        @rtype: Objectivity
        """
        obj = Objectivity([], root.attrib['Action'])
        return obj

    #TODO: code parser for other subproperties

    def getActionByName(self, name):
        for action in self.actions:
            if action.name == name:
               return action
        action = Action(name)
        self.actions.append(action)
        return action

    def parseAction(self, root):
        """
        @rtype: Action
        """
        #check that actions has not action with such name
        action = None
        if 'Name' in root.attrib:
            action = self.getActionByName(root.attrib['Name'])
        else:
            action = Action()
            self.actions.append(action)
        if 'duration' in root.attrib:
            action.duration = int(root.attrib['duration'])
        else:
            action.duration = 1

        if len(root):
            switchParam = {
                'Arguments': Arguments(),
                'Condition': None,
                'Actions': []
            }
            for child in root:
                switchParam[child.tag] = getattr(self, 'parse' + child.tag)(child)

            action.arguments = switchParam['Arguments']
            action.condition = switchParam['Condition']
            action.actions = switchParam['Actions']

        return action

    def parseArguments(self, root):
        """
        @rtype: Arguments
        """
        arguments = Arguments()
        for child in root:
            arguments.append(self.parseArgument(child))
        return arguments

    def parseArgument(self, root):
        """
        @rtype: Argument
        """
        argument = Argument(root.attrib['Name'], [], root.attrib['Type'])
        try:
            argument.value = root.attrib['Value']
        except:
            pass
        return argument

    def parseActions(self, root):
        actions = []
        for child in root:
            actions.append(getattr(self, 'parse' + child.tag)(child))
        return actions

    def parseCondition(self, root):
        """
        @rtype: Condition
        """
        condition = Condition()
        self.condArgs = Arguments()
        for child in root:
            condition.Conditions.append(getattr(self, 'parse' + child.tag)(child))
        condition.arguments = self.condArgs
        self.condArgs = Arguments()
        return condition

    #TODO: code parser for all elementary actions
    #TODO: code parser for all elementary conditions
    def parseComparison(self, root):
        """
        @rtype: Comparison
        """
        signs = {
            '$lt': '<',
            '$let': '<',
            '$dt': '>',
            '=':   '=',
            '!=': '!='
        }
        c = Comparison()
        c.CompType = signs[root.attrib['Sign']]
        arg = Argument(root.attrib['Arg1'], None, 'Characteristic')
        c.arguments.append(arg)
        self.condArgs.append(arg)
        try:
            Arg2Value = int(root.attrib['Arg2'])
            arg = Argument(None, Arg2Value, 'const')
        except:
            arg = Argument(root.attrib['Arg2'], None, 'Characteristic')
            self.condArgs.append(arg)
        c.arguments.append(arg)
        return c

    #TODO: here is ambiguity do not know from which agent take property
    def parseCharacteristicChange(self, root):
        """
        @rtype: CharacteristicChange
        """
        c = CharacteristicChange()
        c.ChName = root.attrib['ChName']
        modifier = root.attrib['Modifier']
        try:
            modifier = int(modifier)
        except:
            pass
        c.Modifier = modifier
        return c

    def parseDeleteAgent(self, root):
        return DeleteAgent(root.attrib['Agent'])

    def parseHasProperty(self, root):
        hp = HaveProperty()
        hp.arguments.append(Argument('AgentName', root.attrib['AgentName'], 'Agent'))
        hp.arguments.append(Argument('ChName', root.attrib['ChName'], 'Property'))
        return hp

    def parsePeriodicity(self, root):
        t = "TimeInterval" in root and 1 or int(root.attrib["TimeInterval"])
        return Periodicity(None, self.getActionByName(root.attrib['Action']), t)


    def CreateAgent(self, type):
        """
        create agent from template
        @param type: agent type
        @type type:str
        @return: agent
        @rtype:Agent
        """
        for agent in self.agents:
            if agent.Type == type:
                newAgent = deepcopy(agent)
                newAgent.init()
                return newAgent

        raise Exception('ParseModelXML.CreateAgent: No such agent type')

    def CreateAction(self, name):
        """
        create action from template
        @param name: action name
        @type name:str
        @return: action
        @rtype:Action
        """
        for action in self.actions:
            if action.name == name:
                return deepcopy(action)
        raise Exception('ParseModelXML.CreateAction: No action with such name')

#    def CreateAgentFromTemplate(self, agent):
#        return deepcopy(agent)
#
#    def CreateActionFromTemplate(self, action):
#        return deepcopy(action)