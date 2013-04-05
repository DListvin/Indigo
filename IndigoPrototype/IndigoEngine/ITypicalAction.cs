using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;

namespace IndigoEngine
{
    /// <summary>
    /// An action, suggested by agent and performed by world
    /// </summary>
    interface ITypicalAction
    {
		/// <summary>
		/// Operations with action object
		/// </summary>
        Agent Object { get; set; }

		/// <summary>
		/// Operations with action subject
		/// </summary>
        Agent Subject { get; set; }

		/// <summary>
		/// Operations with action info: conflict actions can not be performed with one object from different subjects in one moment
		/// </summary>
        bool MayBeConflict { get; set; }

		/// <summary>
		/// Perfoming the action: defines action results for object and subject
		/// </summary>
        void Perform();
    }
}
