using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine;
using NLog;

namespace TextUI
{
    /// <summary>
    /// There is an action in command. Haven't tested parameters yet.
    /// </summary>
    /// <param name="p"></param>
    public delegate void CommandRealisation(params object[] p);

    /// <summary>
    /// A class for one textUI command
    /// </summary>
    class Command : NameableObject
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

        string description;
        CommandRealisation commandBody;
        //List<Type> parameters;

		#region Constructors

        public Command(string argName, string argDescription, CommandRealisation argCommandBody /*, IEnumerable<Type> parameters*/)
			:base()
        {
            Name = argName;
            Description = argDescription;
            commandBody = argCommandBody;
            //this.parameters = parameters.ToList();
        }

		public Command()
			:this("New command", "Does nothing", args => {})
		{
		}

		#endregion

		#region Properties

        public string Description
        {
            get { return "-" + Name + "\n    " + description; }
			set { description = value; }
        }

		public CommandRealisation CommandBody
		{
			get { return commandBody; }
			set { commandBody = value; }
		}

		#endregion

        public void Execute(params object[] p)
        {
            try
            {
                CommandBody.Invoke(p);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error executing command " + Name + ":");
                Console.WriteLine("    " + e.ToString());
            }
        }

    }
}
