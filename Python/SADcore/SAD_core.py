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

agents = []
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
    action.Perform();
    action = a.Think()
    action.Perform();
    action = a.Think()
    action.Perform();
    print(a.Properties[1].Value)

    pass