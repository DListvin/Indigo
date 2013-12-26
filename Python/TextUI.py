from model import *

active_queues = []

class TestUIShell:
    listOfCommands = []           #List of available commands
    model = []                    #Attached model to observe
    listOfSubscribedCommands = [] #List of commands run on world tick event.
    isRunning = True              #Exit flag
    #Main loop for UI
    @staticmethod
    def run():
        TestUIShell.model.start()
        while TestUIShell.isRunning:
            try:
                string = input()
                if string == '-start':
                    broadcast_event(ModelState.Running)
                if string == '-stop':
                    broadcast_event(ModelState.Stopping)
                if string == '-pause':
                    broadcast_event(ModelState.Paused)
            except:
                pass


def broadcast_event(data):
    for q in active_queues:
        q.put(data)


model = Model(active_queues)
TestUIShell.model = model
TestUIShell.run()