#-------------------------------------------------------------------------------
# Name:        модуль1
# Purpose:
#
# Author:      Dlistvin
#
# Created:     10.11.2013
# Copyright:   (c) Dlistvin 2013
# Licence:     <your licence>
#-------------------------------------------------------------------------------

from xml.dom import minidom

def main():
    agents = []
    temp = Agent()
    temp.typename = 'Man'
    temp.id = len(agents) + 1

    #this will be in func!
    t = Characteristic()
    t.name = 'health'
    t.type = 'int'
    t.value = 50
    temp.Properties = [temp.Properties, t]
    agents = [agents, temp]
    pass

class Property:
    pass

class Characteristic(Property):
    name = []
    type = []
    value = []

class Agent:
    typename = []
    id = []
    Properties = []

if __name__ == '__main__':
    main()
