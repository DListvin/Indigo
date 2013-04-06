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
		/// <summary>
		/// Operations with the world, that is simulating in the model
		/// </summary>
        IEnumerable<Agent> Agents { get; set; }

		/// <summary>
		/// Operations with model state
		/// </summary>
        ModelState State { get; set; }

		/// <summary>
		/// Operations with info about how many iterations of main loop have passed
		/// </summary>
        long PassedModelIterations { get; set; }     
		
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
    }
}
