using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using IndigoEngine.Agents;

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
        private World simulatingWorld;                       //Shows, what world is simulating in the model
        private long passedModelIterations;                  //Info about how many iterations of main loop have passed
        private TimeSpan modelIterationTick;                 //Info about time interval betwin loop iterations
        private ModelState state = ModelState.Uninitialised; //Model state from ModelState enum
				
        private Thread modelThread;  //This object controls working model in other process
		
		#region Constructors

        public Model()
        {
            Initialise();
        }      

		#endregion

        #region Properties

        #region IObservableModel realisation

        public long PassedModelIterations 
		{
			get { return passedModelIterations; } 
			set { passedModelIterations = value; }
		} 

        public TimeSpan ModelIterationTick 
		{
			get { return modelIterationTick; }
			set { modelIterationTick = value; } 
		} 

        public ModelState State 
		{
			get { return state; } 
			set { state = value; }
		} 
        
        public IEnumerable<Agent> Agents
        {
            get { return simulatingWorld.Agents; }
			private set { simulatingWorld.Agents = value.ToList(); }
        }

		#endregion

		public Thread ModelThread
		{
			get { return modelThread; }
			set { modelThread = value; }
		}
	
        #endregion

        #region Model management

		/// <summary>
		/// IObservableModel
		/// </summary>
        public void Initialise()
        {
            SimulatingWorld = new World();
            PassedModelIterations = 0;
            ModelIterationTick = TimeSpan.FromSeconds(2);
            ModelThread = new Thread(MainLoop); //Specify the function to be performed in other process
            State = ModelState.Initialised;
        }
        
		/// <summary>
		/// IObservableModel
		/// </summary>
        public void Start()
        {
            if (State == ModelState.Initialised)
            {
                State = ModelState.Running;
                ModelThread.Start(); //Start process
            }
        }
		
		/// <summary>
		/// IObservableModel
		/// </summary>
        public void Pause()
        {
            State = ModelState.Paused;
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
            State = ModelState.Stopping;
            ModelThread.Join(); //Waiting for other process to end
            State = ModelState.Initialised;
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
                        ++PassedModelIterations;
                        Thread.Sleep(ModelIterationTick);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                State = ModelState.Error;
            }
        }
    }
}
