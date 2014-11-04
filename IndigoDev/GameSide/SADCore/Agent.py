from Action import Arguments, Argument
from Property import Periodicity

class Agent:
    """
    Base Class in SADcore.
    Everything is agents agents are everything.
    """
    def __init__(self):
        self.Type = []        #Name of type (Man, dog, rain, axe...)
        self.ID = []          #Unique id, primary key
        self.Properties = []  #List of properties
        self.myWorld = []     # world Interface to agent

    def init(self):
        """
        Init function for Agent initialization before using in world
        make some groundwork for using:
            Configure arguments for periodicity actions performing
        """
        periodicityActions = self.GetActionsByType(Periodicity)
        for prop in periodicityActions:
            prop.Action.arguments.set("self", self)
            #TODO:  prop.Action.arguments["self"] = self

    def GetPropertyByName(self, name):
        """
        Get property by name of agent if exist
        @param name: property name
        @type name:str
        @return: found property or None if agent has not such property
        @rtype:Property
        """
        #TODO FIX Bug with "middle" characteristic search
        for prop in self.Properties:
            ch = prop.getByName(name)
            if not (ch is None):
                return ch
                #may be here we must raise exception
        return None

    def GetActionsByType(self, type):
        """
        Get actions By Type of Property like Feeling or Periodicity
        This properties has Perform function, so it is Actions
        @param type: type of Property
        @return: all found actions
        """
        PropertiesByType = []
        for p in self.Properties:
            PropertiesByType.extend(p.getAllByType(type))
        return PropertiesByType