using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using IndigoEngine.Agents;
using System.Drawing;
using IndigoEngine.ActionsNew;

namespace IndigoEngine
{
    /// <summary>
    /// Interface for the UI-conversation
    /// </summary>
    public interface IObservableModel
    {
		/// <summary>
		/// Get Agents from world
		/// </summary>
        IEnumerable<Agent> Agents { get; }

        /// <summary>
        /// Event, occured on iteration tick.
        /// </summary>
        event EventHandler ModelTick;

        /// <summary>
        /// Collection of (iteration, actionCollection). Stores actions for some number of turns.
        /// </summary>
        IDictionary<long, IEnumerable<ActionAbstract>> Actions { get; }

        /// <summary>
        /// How many turns actions should be stored
        /// </summary>
        int TurnsToStore { get; set; }

		/// <summary>
		/// Operations with model state
		/// </summary>
        ModelState State { get; }

		/// <summary>
		/// Operations with info about how many iterations of main loop have passed
		/// </summary>
        long PassedModelIterations { get; }     
		
		/// <summary>
		/// Operations with time interval betwin loop iterations
		/// </summary>         
        TimeSpan ModelIterationTick { get; set; } 

		/// <summary>
		/// Initialises model's start values
		/// </summary>
        void Initialise(); 

		/// <summary>
		/// Run main loop
		/// </summary>
        void Start(); 

		/// <summary>
		/// Pause main loop
		/// </summary>
        void Pause();
		
		/// <summary>
		/// Resume running if paused
		/// </summary>
        void Resume(); 

		/// <summary>
		/// Stop main loop and process with safe method
		/// </summary>
        void Stop();

        /// <summary>
        /// Saving the model to file
        /// </summary>
        /// <param name="path">Path to savefile</param>
        void Save(string path);

        /// <summary>
        /// Loading the model from file
        /// </summary>
        /// <param name="path">Path to savefile</param>
        void Load(string path);

        /// <summary>
        /// Computes n iterations
        /// </summary>
        /// <param name="n">number of iterations to compute</param>
        void StepNIterationsForward(int n = 1);

        /// <summary>
        /// Computes model up to n iteration
        /// </summary>
        /// <param name="n">To wich iteration to compute</param>
        void GoToNIteration(long n);		
			
		/// <summary>
		/// Deleting agent from the world completely. 
		/// </summary>
		/// <param name="argAgentToDelete">Agent to delete</param>
		void DeleteAgent(Agent argAgentToDelete);

		/// <summary>
		/// Adding agent to the world. 
		/// </summary>
		/// <param name="argAgentToAdd">Agent to add</param>
		void AddAgent(Agent argAgentToAdd);

		/// <summary>
		/// Getting the number of agents in the world
		/// </summary>
		/// <returns></returns>
		int GetAgentsAmount();
    }
}
