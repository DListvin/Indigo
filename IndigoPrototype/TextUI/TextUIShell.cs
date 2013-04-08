using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine;
using IndigoEngine.Agents;

namespace TextUI
{
    /// <summary>
    /// If smb forgot to specify model
    /// </summary>
    public class UnsetModelException : Exception
    {
        public UnsetModelException()
        {
        }

        public UnsetModelException(string message) 
			: base(message)
        {
        }

        public UnsetModelException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    /// <summary>
    /// For wrong input during textUI
    /// </summary>
    public class WrongInputException : Exception
    {
        public WrongInputException()
        {
        }

        public WrongInputException(string message)
            : base(message)
        {
        }

        public WrongInputException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    /// <summary>
    /// Static class, making console UI.
    /// </summary>
    static class TextUIShell
    {        
        private static List<Command> listOfCommands;  //List of available commands        
        private static IObservableModel model = null; //Attached model to observe
		private static bool isRunning = true;         //Exit flag	
        private static EventHandler Handler = new EventHandler(HandleTick); //Handler for world iteration event
        private static List<Command> listOfSubscribedCommands; //List of commands run on world tick event.
        private static bool isHandlingEvent = false;

		#region Constructors

            static TextUIShell()
            {
                Initialise();
            }

		#endregion

		#region Properties

            /// <summary>
            /// Property for attaching model. Think i'll modify thir connection somehow later
            /// </summary>
            public static IObservableModel Model
            {
			    get { return model; }
                set { model = value; }
            }

		    public static bool IsRunning
		    {
			    get { return isRunning; }
			    set { isRunning = value; }
		    }

		    public static List<Command> ListOfCommands
		    {
			    get { return listOfCommands; }
			    set { listOfCommands = value; }
		    }

		#endregion

        /// <summary>
        /// Method for commands storage. Add them here.
        /// </summary>
        static void Initialise()
        {
			ListOfCommands= new List<Command>();
            listOfSubscribedCommands = new List<Command>();

            ListOfCommands.Add(new Command("test", "Writes some test output", args => 
			{
				Console.WriteLine("Some Test Output"); 
			}));

            ListOfCommands.Add(new Command("help", "Prints all possible commands with description", args =>
            {
                foreach (Command c in ListOfCommands)
				{
                    Console.WriteLine(c.Description);
				}
            }));

            ListOfCommands.Add(new Command("init", "Initialising the model again", args =>
            {
                if(Model.State == ModelState.Uninitialised || Model.State == ModelState.Error)
				{
					Console.WriteLine("Initialising model...");
					Model.Initialise();
					Console.WriteLine("Model initialised.");					
				}
				else
				{
					Console.WriteLine("Model is " + Model.State.ToString() + ". You can't initialise it!");
				}
            }));

            ListOfCommands.Add(new Command("exit", "Stops the entire UI", args =>
            {
                if(Model.State == ModelState.Running || Model.State == ModelState.Paused)
				{
					Console.WriteLine("Stopping model...");
					Model.Stop();
					Console.WriteLine("Model stopped.");					
				}
                IsRunning = false;
            }));

            ListOfCommands.Add(new Command("start", "Starts model", args => 
			{
				if(Model.State == ModelState.Initialised)
				{
					Console.WriteLine("Starting model...");
					Model.Start(); 
					Console.WriteLine("Model started.");
				}
				else
				{
					Console.WriteLine("Model is " + Model.State.ToString() + ". You can't start it!");
				}
			}));

            ListOfCommands.Add(new Command("pause", "Pauses model", args => 
			{
				if(Model.State == ModelState.Running)
				{
					Console.WriteLine("Pausing model...");
					Model.Pause(); 
					Console.WriteLine("Model paused.");
				}
				else
				{
					Console.WriteLine("Model is " + Model.State.ToString() + ". You can't pause it!");
				}
			}));

            ListOfCommands.Add(new Command("stop", "Stops model", args => 
			{
				if(Model.State == ModelState.Running || Model.State == ModelState.Paused)
				{
					Console.WriteLine("Stopping model...");
					Model.Stop(); 
					Console.WriteLine("Model stopped.");
				}
				else
				{
					Console.WriteLine("Model is " + Model.State.ToString() + ". You can't stop it!");
				}
			}));

            ListOfCommands.Add(new Command("resume", "Resumes model from pause", args => 
			{
				if(Model.State == ModelState.Paused)
				{
					Console.WriteLine("Resuming model...");
					Model.Resume(); 
					Console.WriteLine("Model resumed.");
				}
				else
				{
					Console.WriteLine("Model is " + Model.State.ToString() + ". You can't resume it!");
				}
			}));

            ListOfCommands.Add(new Command("state", "Shows model state", args => 
			{ 
				Console.WriteLine("Model state: " + Model.State.ToString()); 
			}));

            ListOfCommands.Add(new Command("iteration", "Shows model loop iteration", args => 
			{
				Console.WriteLine("Current model iteration: " + Model.PassedModelIterations); 
			}));

            ListOfCommands.Add(new Command("tick", "Shows model loop iteration tick interval", args => 
			{
				Console.WriteLine("Current tick interval: " + Model.ModelIterationTick); 
			}));

            ListOfCommands.Add(new Command("settick", "Sets model loop iteration in ms (ex: -settick 3000)", args =>
            {
                var ms = args[1] as string;
                Model.ModelIterationTick = TimeSpan.FromMilliseconds(Convert.ToInt16(ms));
				Console.WriteLine("Tick set to: " + Model.ModelIterationTick); 
            }));

            ListOfCommands.Add(new Command("agents", "Lists all agents in the world", args =>
            {
                foreach (Agent agent in Model.Agents)
				{
                    Console.WriteLine(agent.ToString() + "\n");                
				}
            }));

            ListOfCommands.Add(new Command("subscribe", "Subscribes commands on model tick (ex: -subscribe iteration agents)", args =>
            {                
                foreach(string command in args)
                {
                    if(command == "subscribe")
                        continue;
                    listOfSubscribedCommands.Add(ListOfCommands.FirstOrDefault(comm => { return comm.Name == command; }));
                }
                if(!isHandlingEvent)
                    Model.ModelTick += Handler;

                isHandlingEvent = true;
            }));

            ListOfCommands.Add(new Command("unsubscribe", "Unsubscribes all commands form event", args =>
            {
                isHandlingEvent = false;
                Model.ModelTick -= Handler;
                listOfSubscribedCommands.Clear();
            }));

            ListOfCommands.Add(new Command("actions", "list of all actions for last n turns (ex: -actions 5)", args =>
            {
                int? n;
                if (args.Length > 1)
                {
                    n = Convert.ToInt16(args[1] as string);
                    if (!n.HasValue)
                        n = 5;
                }
                else
                    n = 5;

                for (int i = 0; i < n.Value; ++i)
                {
                    foreach (var action in Model.Actions[Math.Max(Model.PassedModelIterations - 1 - i, 0)])
                    {
                        Console.WriteLine("Iteration: " + (Model.PassedModelIterations - 1 - i).ToString() + "; Action: " + action.Name);
                    }
                }
            }));

            ListOfCommands.Add(new Command("showshortmem", "Showing the short memory of the agent (ex: -showshortmem <agent_name>)", args =>
            {
                var agentName = args[1] as string;
                
                Console.WriteLine(
					((AgentLiving)(Model.Agents.First(ag => 
					{
						return ag.Name == agentName;
					}
				))).AgentsShortMemory.ToString());        
            }));

            ListOfCommands.Add(new Command("showlongmem", "Showing the long memory of the agent (ex: -showlongmem <agent_name>)", args =>
            {
                var agentName = args[1] as string;
                
                Console.WriteLine(
					((AgentLiving)(Model.Agents.First(ag => 
					{
						return ag.Name == agentName;
					}
				))).AgentsLongMemory.ToString());        
            }));
        }

        /// <summary>
        /// Main loop for UI
        /// </summary>
        public static void Run()
        {
            if (model == null)
            {
                throw new UnsetModelException("You shoud specify model to watch for using Model property before calling Run method");
            }

            Console.WriteLine("Indigo prj, text UI.");
            Console.WriteLine("Type -help to get list of all available commands");

            while(isRunning)
            {
                try
                {
					string input = Console.ReadLine();

                    if (input == null || input.Length == 0)
                        throw new WrongInputException("Empty string in input. Type -help to see available commands.");

					string[] parsedInput = input.Split(' ');					

					if(!parsedInput[0].StartsWith("-"))
						throw new WrongInputException("Each command shoud start with -. Don't know why, actualy.");

					if(!listOfCommands.Exists(c => c.Name == parsedInput[0].Substring(1)))
						throw new WrongInputException("There in no command with this name. Type -help to see list of available commands.");

					//Execute typed command
					ListOfCommands.First(c => c.Name == parsedInput[0].Substring(1)).Execute(parsedInput);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            //Stop the model here
        }

        static void HandleTick(object sender, EventArgs e)
        {
            foreach (var c in listOfSubscribedCommands.Where(val => { return val != null; }))
                c.Execute();
        }
    }
}
