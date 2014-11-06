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
        for c in self.Conditions:
            c.arguments = self.arguments
            if not c.Calculate():
                res = False
        return res


class HaveProperty(Condition):
    def Calculate(self):
        pass


class Comparison(Condition):
    def __init__(self):
        Condition.__init__(self)
        self.CompType = None
        self.arg1 = None
        self.arg2 = None

    def Calculate(self):
        arg1Parse = self.arg1.split(".", 1)
        ch = self.arguments.getValue(arg1Parse[0]).GetPropertyByName(arg1Parse[1])
        arg2 = self.arg2
        if not type(self.arg2) is int:
            arg2Parse = arg2.split('.', 1)
            arg2 = self.arguments.getValue(arg2Parse[0]).GetPropertyByName(arg2Parse[1])

        signs = {
            '<': ch < arg2,
            '>': ch > arg2,
            '=': ch == arg2,
            '!=': ch != arg2
        }
        return signs[self.CompType]