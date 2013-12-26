__author__ = 'Zurk'

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

    def GetActionsByType(self, type):
        result = []
        for p in self.Properties:
            if isinstance(p, type):
                result.append(p)
        return result


    def Think(self):
        for p in self.Properties:
            if isinstance(p, Subjectivity):
                a = p.Action
                a.Arguments.clear()
                a.Arguments.append(self)
                a.Arguments.append(10)
                return a
                break