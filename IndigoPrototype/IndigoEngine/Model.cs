using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using IndigoEngine.Agents;
using NLog;

namespace IndigoEngine
{   
    /// <summary>
    /// Describes possible states of model
    /// </summary>
    public enum ModelState
    {
        Uninitialised,
        Initialised,
        Running,
        Paused,
        Stopping,
        Error
    }

    /// <summary>
    /// Upper existanse, performing technical moments of engine. Contains world for logical moments.
    /// </summary>
    public class Model : IObservableModel
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

        private World simulatingWorld;                       //Shows, what world is simulating in the model
        private long passedModelIterations;                  //Info about how many iterations of main loop have passed
        private TimeSpan modelIterationTick;                 //Info about time interval betwin loop iterations
        private ModelState state = ModelState.Uninitialised; //Model state from ModelState enum
        IDictionary<long, IEnumerable<Action>> storedActions;
        private Thread modelThread;  //This object controls working model in other process

        public event EventHandler ModelTick;
		
		#region Constructors

        public Model()
        {
            Initialise();
        }      

		#endregion

        #region Properties

            #region IObservableModel realisation

                public int TurnsToStore { get; set; } // For how many turns actions are stored

                public IDictionary<long, IEnumerable<Action>> Actions { get { return storedActions; } private set { storedActions = value; } } // Action storage

                public long PassedModelIterations 
		        {
			        get { return passedModelIterations; } 
			        private set { passedModelIterations = value; }
		        } 

                public TimeSpan ModelIterationTick 
		        {
			        get { return modelIterationTick; }
			        set { modelIterationTick = value; } 
		        } 

                public ModelState State 
		        {
			        get { return state; } 
			        private set { state = value; }
		        } 
        
                public IEnumerable<Agent> Agents
                {
                    get { return simulatingWorld.Agents; }
			        private set { simulatingWorld.Agents = value.ToList(); }
                }

		    #endregion
	
        #endregion

        #region Model management

		    /// <summary>
		    /// IObservableModel
		    /// </summary>
            public void Initialise()
            {
				if(State == ModelState.Error || State == ModelState.Uninitialised)
				{
					simulatingWorld = new World();
					PassedModelIterations = 0;
					TurnsToStore = 100;
					ModelIterationTick = TimeSpan.FromSeconds(0.1);
					modelThread = new Thread(MainLoop); //Specify the function to be performed in other process
					State = ModelState.Initialised;
					storedActions = new Dictionary<long, IEnumerable<Action>>();
				}
            }
        
		    /// <summary>
		    /// IObservableModel
		    /// </summary>
            public void Start()
            {
                if (State == ModelState.Initialised)
                {
                    State = ModelState.Running;
                    modelThread.Start(); //Start process
                }
            }
		
		    /// <summary>
		    /// IObservableModel
		    /// </summary>
            public void Pause()
            {
				if(State == ModelState.Running)
				{
					State = ModelState.Paused;
				}
            }
		
		    /// <summary>
		    /// IObservableModel
		    /// </summary>
            public void Resume()
            {	
			    if(State == ModelState.Paused)
			    {
				    State = ModelState.Running;
			    }
            }
		
		    /// <summary>
		    /// IObservableModel
		    /// </summary>
            public void Stop()
            {
				if(State == ModelState.Running || State == ModelState.Paused)
				{
					State = ModelState.Stopping;
					modelThread.Join(); //Waiting for other process to end
					State = ModelState.Uninitialised;
				}
            }

		#endregion

        public void MainLoop()
        {
            //try
            {
                for (; ; )
                {
                    if (State == ModelState.Stopping)
					{
                        break;
					}
                    if (State == ModelState.Paused)
					{
                        continue;
					}
                    if (State == ModelState.Running)
                    {
                        //There out main loop is running
                        simulatingWorld.MainLoopIteration();

                        //Work out the end of iteration

                        //Sending a message
                        if (ModelTick != null)
                            ModelTick(this, null);


                        ManageActionStorage();
                        ++PassedModelIterations;
                        Thread.Sleep(ModelIterationTick);
                    }
                }
            }
            //catch (Exception e)
            {
                //Console.WriteLine(e.Message);
                //State = ModelState.Error;
            }
        }

        /// <summary>
        /// Updates action storage each turn
        /// </summary>
        void ManageActionStorage()
        {
            //Coping container, cause in main loop it will be cleared by reference
            List<Action> temp = new List<Action>();
            foreach(Action action in simulatingWorld.Actions)
                temp.Add(action);

            Actions.Add(PassedModelIterations, temp);

            //Remove old values
            Actions.Remove(Actions.FirstOrDefault(kvpair => 
            { 
                return kvpair.Key < PassedModelIterations - TurnsToStore; 
            }));
        }
    }
}
