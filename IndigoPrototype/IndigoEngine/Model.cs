using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using IndigoEngine.Agents;
using IndigoEngine.Actions;
using NLog;

namespace IndigoEngine
{   
    /// <summary>
    /// Describes possible states of model
    /// </summary>
    [Serializable]
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
    [Serializable]
    public class Model : IObservableModel
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

        private World simulatingWorld;                       //Shows, what world is simulating in the model
        private long passedModelIterations;                  //Info about how many iterations of main loop have passed
        private TimeSpan modelIterationTick;                 //Info about time interval betwin loop iterations
        private ModelState state = ModelState.Uninitialised; //Model state from ModelState enum
        IDictionary<long, IEnumerable<ActionAbstract>> storedActions;
        [NonSerialized] private Thread modelThread;  //This object controls working model in other process

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

                public IDictionary<long, IEnumerable<ActionAbstract>> Actions { get { return storedActions; } private set { storedActions = value; } } // Action storage

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
					ModelIterationTick = TimeSpan.FromSeconds(2);
					modelThread = new Thread(MainLoop); //Specify the function to be performed in other process
					State = ModelState.Initialised;
					storedActions = new Dictionary<long, IEnumerable<ActionAbstract>>();
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
           // try
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
        /// For testing during test = )
        /// </summary>
        /// <param name="agent"></param>
        public void AddAgent(Agent agent)
        {
            simulatingWorld.AddAgent(agent);
        }

        /// <summary>
        /// I wrote it for testing needs
        /// </summary>
        public int GetAgentsAmount()
        {
            return simulatingWorld.Agents.Count;
        }

        /// <summary>
        /// Quickly runs n steps forward
        /// </summary>
        /// <param name="n">number of stepsS</param>
        public void StepNIterationsForward(int n = 1)
        {
            ModelState prevState = State;
            try
            {
                for (int i = 0; i < n; ++i)
                {
                    if (State == ModelState.Stopping)
                    {
                        break;
                    }
                    if (State == ModelState.Paused)
                    {
                        State = ModelState.Running;
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
                    }
                }
                State = prevState;
            }
            catch (Exception)
            {
                //Console.WriteLine(e.Message);
                State = ModelState.Error;
            }
        }

        /// <summary>
        /// Computes model up to n iteration
        /// </summary>
        /// <param name="n">To wich iteration to compute</param>
        public void GoToNIteration(long n)
        {
            if (n > PassedModelIterations)
            {
                StepNIterationsForward((int)(n - PassedModelIterations));
            }
        }
        /// <summary>
        /// Updates action storage each turn
        /// </summary>
        void ManageActionStorage()
        {
            //Coping container, cause in main loop it will be cleared by reference
            List<ActionAbstract> temp = new List<ActionAbstract>();
            foreach(ActionAbstract action in simulatingWorld.Actions)
                temp.Add(action);

            Actions.Add(PassedModelIterations, temp);

            //Remove old values
            Actions.Remove(Actions.FirstOrDefault(kvpair => 
            { 
                return kvpair.Key < PassedModelIterations - TurnsToStore; 
            }));
        }

        #region ObjectMethodsOverride

           
        #endregion
    }
}
