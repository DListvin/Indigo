using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using IndigoEngine.Agents;
using IndigoEngine.Actions;
using NLog;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

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
        private EventWaitHandle waitEvent = new AutoResetEvent(true); //Using for safe suspend and resume of model process

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
                logger.Trace("Initialise method entered; ModelState now is {0}", State.ToString());

				if(State == ModelState.Error || State == ModelState.Uninitialised)
				{
					simulatingWorld = new World();
					PassedModelIterations = 0;
					TurnsToStore = 100;
					ModelIterationTick = TimeSpan.FromSeconds(2);
					modelThread = new Thread(MainLoop); //Specify the function to be performed in other process
                    storedActions = new Dictionary<long, IEnumerable<ActionAbstract>>();
                    State = ModelState.Initialised;

                    logger.Info("Model initialised");
				}
            }
        
		    /// <summary>
		    /// IObservableModel
		    /// </summary>
            public void Start()
            {
                logger.Trace("Start method entered; ModelState now is {0}", State.ToString());

                if (State == ModelState.Initialised)
                {
                    State = ModelState.Running;

                    logger.Trace("Starting model thread");
                    modelThread.Start(); //Start process
                    logger.Info("Model thread started");
                }
            }
		
		    /// <summary>
		    /// IObservableModel
		    /// </summary>
            public void Pause()
            {
                logger.Trace("Pause method entered; ModelState now is {0}", State.ToString());

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
                logger.Trace("Resume method entered; ModelState now is {0}", State.ToString());

			    if(State == ModelState.Paused)
			    {
				    State = ModelState.Running;
                    waitEvent.Set(); //Tells model thread to stop waiting
			    }
            }
		
		    /// <summary>
		    /// IObservableModel
		    /// </summary>
            public void Stop()
            {
                logger.Trace("Stop method entered; ModelState now is {0}", State.ToString());

				if(State == ModelState.Running || State == ModelState.Paused)
				{
                    waitEvent.Set(); //unpause, if was
					State = ModelState.Stopping;
                    logger.Info("Waiting for model thread to stop");
					modelThread.Join(); //Waiting for other process to end
                    logger.Info("Model thread joined successfuly");
					State = ModelState.Uninitialised;
				}
            }

		#endregion

        public void MainLoop()
        {
            try
            {
                for (; ; )
                {
                    if (State == ModelState.Stopping)
					{
                        logger.Info("Stopping model");
                        break;
					}
                    if (State == ModelState.Paused)
					{
                        logger.Info("Pausing model");
                        waitEvent.WaitOne(); //Wait for waitEvent.Set()
					}
                    if (State == ModelState.Running)
                    {
						System.Diagnostics.Stopwatch timer = System.Diagnostics.Stopwatch.StartNew(); 
                        //There out main loop is running
                        simulatingWorld.MainLoopIteration();

                        //Work out the end of iteration

                        //Sending a message
                        if (ModelTick != null)
                            ModelTick(this, null);


                        ManageActionStorage();
                        ++PassedModelIterations;

						timer.Stop();
						long ticksToSleep = ModelIterationTick.Ticks - timer.ElapsedTicks;
						TimeSpan timeToSleep = new TimeSpan(ticksToSleep < 0 ? 0 : ticksToSleep);
                        Thread.Sleep(timeToSleep);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("Error {0} occupied in main loop", e.Message);
                Console.WriteLine(e.Message);
                State = ModelState.Error;
                throw e;
            }
        }

        /// <summary>
        /// IObservableModel
        /// </summary>
        public void AddAgent(Agent argAgentToAdd)
        {
            simulatingWorld.AddAgent(argAgentToAdd);
        }
		
        /// <summary>
        /// IObservableModel
        /// </summary>
		public void DeleteAgent(Agent argAgentToDelete)
		{
            simulatingWorld.DeleteAgent(argAgentToDelete);
		}
		
        /// <summary>
        /// IObservableModel
        /// </summary>
        public int GetAgentsAmount()
        {
            return simulatingWorld.Agents.Count;
        }
		
        /// <summary>
        /// IObservableModel
        /// </summary>
		public List<Agent> GetAgentsAt(Point argAgentLoc)
		{
			return simulatingWorld.GetAgentsAt(argAgentLoc);
		}

        /// <summary>
        /// Quickly runs n steps forward
        /// </summary>
        /// <param name="n">number of stepsS</param>
        public void StepNIterationsForward(int n = 1)
        {
            logger.Trace("StepNIterationsForward(n = {0}) entered; ModelState is {1}", n, State);
            if (State != ModelState.Paused && State != ModelState.Running)
            {
                logger.Error("Failed to jump {0} iterations, cause the model is {1}", n, State);
				return;
            }
            ModelState prevState = State;
            Pause(); //instead of State = ModelState.Paused
            try
            {
                for (int i = 0; i < n; ++i)
                {
					//There out main loop is running
					simulatingWorld.MainLoopIteration();

					ManageActionStorage();
					++PassedModelIterations;
                }

				//Sending a message about tick
				if (ModelTick != null)
				{
					ModelTick(this, null);
				}

                if (prevState == ModelState.Running)
                    Resume();
                //instead of State = prevState;
                logger.Info("Jumped {0} iterations forward, current iteration is {1}", n, PassedModelIterations);
            }
            catch (Exception e)
            {
                logger.Error("Error {0} occupied in main loop during StepNIterationsForward", e.Message);
                Console.WriteLine(e.Message);
                State = ModelState.Error;
            }
        }

        /// <summary>
        /// Computes model up to n iteration
        /// </summary>
        /// <param name="n">To wich iteration to compute</param>
        public void GoToNIteration(long n)
        {
            logger.Trace("GoToNIteration(n = {0}) entered; ModelState is {1}", n, State);
            if (n > PassedModelIterations)
            {
                StepNIterationsForward((int)(n - PassedModelIterations));
            }
        }

        /// <summary>
        /// Saving the model to file
        /// </summary>
        /// <param name="path">Path to savefile</param>
        public void Save(string path)
        {
            logger.Trace("Save({0}) entered", path);
            FileStream stream;
            BinaryFormatter formatter = new BinaryFormatter();
            stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream, this);
            stream.Close();
            Console.WriteLine("Model saved!");
            logger.Info("Model has been serialized to {0}", path);
        }

        /// <summary>
        /// Loading the model from file
        /// </summary>
        /// <param name="path">Path to savefile</param>
        public void Load(string path)
        {
            logger.Trace("Load({0}) entered", path);
            FileStream stream = new FileStream(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            Model loaded = (Model)formatter.Deserialize(stream);

            lock (simulatingWorld)
            {
                this.simulatingWorld = loaded.simulatingWorld;
            }

            this.passedModelIterations = loaded.passedModelIterations;

            this.modelIterationTick = loaded.modelIterationTick;

            this.state = loaded.state;

            lock (storedActions)
            {
                this.storedActions = loaded.storedActions;
            }

            stream.Close();
            Console.WriteLine("Model loaded!");
            this.Pause();
            logger.Info("Model has been deserialized from {0}", path);
        }

        /// <summary>
        /// Updates action storage each turn
        /// </summary>
        void ManageActionStorage()
        {

            lock (Actions) //No other thread can access Actions while this process is in this block
            {
                //Coping container, cause in main loop it will be cleared by reference
                List<ActionAbstract> temp = new List<ActionAbstract>();
                foreach (ActionAbstract action in simulatingWorld.Actions)
                    temp.Add(action);

                Actions.Add(PassedModelIterations, temp);

                //Remove old values
                Actions.Remove(Actions.FirstOrDefault(kvpair =>
                {
                    return kvpair.Key < PassedModelIterations - TurnsToStore;
                }));
            }
        }

        #region ObjectMethodsOverride

           
        #endregion
    }
}
