using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using IndigoEngine.Agents;

namespace IndigoEngine
{
    /// <summary>
    /// Interface for the UI-conversation
    /// </summary>
    public interface IObservableModel
    {
        IEnumerable<Agent> Agents {get;} //All agents in the world
        ModelState State { get; } //Model state
        long ModelIterations { get; } //How many iterations of main loop have passed
        TimeSpan ModelIterationTick { get; set; } //Specify time interval betwin loop iterations

        void Initialise(); //Initialises model's start values
        void Start(); //Run main loop
        void Pause(); //Pause main loop
        void Resume(); //Resume running if paused
        void Stop(); //Stop main loop and process with safe method
    }
    
    /// <summary>
    /// Describes possible state of model
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
        #region Variables

        World world;
        long passedModelIterations;
        TimeSpan modelIterationTick;
        Thread thread; //This object controls working model in other process
        ModelState state = ModelState.Uninitialised;

        #endregion

        #region Properties

        //Interface realisation
        public long ModelIterations { get { return passedModelIterations; } } 

        public TimeSpan ModelIterationTick { get { return modelIterationTick; } set { modelIterationTick = value; } } 

        public ModelState State { get { return state; } } 
        
        public IEnumerable<Agent> Agents
        {
            get
            {
                return world.Agents;
            }
        }

        #endregion

        #region Methods

        public Model()
        {
            Initialise();
        }        

        public void Initialise()
        {
            world = new World();
            passedModelIterations = 0;
            modelIterationTick = TimeSpan.FromSeconds(2);
            thread = new Thread(MainLoop); //Specify the function to be performed in other process
            state = ModelState.Initialised;
        }
        
        public void Start()
        {
            if (state == ModelState.Initialised)
            {
                state = ModelState.Running;
                thread.Start(); //Start process
            }
        }

        public void Pause()
        {
            state = ModelState.Paused;
        }

        public void Resume()
        {
            state = ModelState.Running;
        }

        public void Stop()
        {
            state = ModelState.Stopping;
            thread.Join(); //Waiting for other process to end
            state = ModelState.Initialised;
        }

        public void MainLoop()
        {
            try
            {
                for (; ; )
                {
                    if (state == ModelState.Stopping)
                        break;
                    if (state == ModelState.Paused)
                        continue;
                    if (state == ModelState.Running)
                    {
                        //There out main loop is running
                        world.MainLoopIteration();

                        //Work out the end of iteration
                        ++passedModelIterations;
                        Thread.Sleep(modelIterationTick);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                state = ModelState.Error;
            }
        }

        #endregion
    }
}
