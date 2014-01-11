__author__ = 'Zurk'
import sys, os
SADPath = os.path.abspath('./SADCore/')
if not SADPath in sys.path:
    sys.path.append(SADPath)

class Condition:
    def __init__(self):
        self.Conditions = [] #List of conditions
        from Action import Arguments
        self.arguments = Arguments()

    def Calculate(self):
        res = True
        for i in self.Conditions:
            if not i.Calculate():
                res = False
        return res


class HaveProperty(Condition):
    def Calculate(self):
        pass


class Comparison(Condition):
    def __init__(self):
        Condition.__init__(self)
        self.CompType = []

    def Calculate(self):
        signs = {
            '<': self.arguments[0] < self.arguments[1],
            '>': self.arguments[0] > self.arguments[1],
            '=': self.arguments[0] == self.arguments[1],
            '!=': self.arguments[0] != self.arguments[1]
        }
        return signs[self.CompType]