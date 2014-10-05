from GameSide.Model import Model, ModelState

active_queues = [] #list of active queues to models. Now can be just one active queue

def broadcast_event(data):
    """
    broadcasts event to all active queues
    Now can be just one active queue
    @param data: some information to broadcast. anything that you want
    @return: no return
    """
    for q in active_queues:
        q.put(data)


class TestUIShell:
    """
    Text interface of world model. Static class
    @type model:Model
    """
    listOfCommands = []
    #List of available commands
    model = []
    #Attached model to observe
    listOfSubscribedCommands = []
    #List of commands run on world tick event.
    isRunning = True
    #Exit flag

    @staticmethod
    def run():
        """
        run shell function
        @return: no return
        """
        TestUIShell.model.start()
        while TestUIShell.isRunning:
            try:
                string = raw_input("Command me!\n")
                if string == '-start':
                    broadcast_event(ModelState.Running)
                if string == '-stop':
                    broadcast_event(ModelState.Stopping)
                    TestUIShell.isRunning = False
                if string == '-pause':
                    broadcast_event(ModelState.Paused)
            except:
                pass


if(__name__ == '__main__'):
    #here is a entry point for Text User Interface
    model = Model(213,active_queues)
    TestUIShell.model = model
    TestUIShell.run()