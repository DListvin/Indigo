using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextUI
{
    /// <summary>
    /// There is an action in command. Haven't tested parameters yet.
    /// </summary>
    /// <param name="p"></param>
    delegate void CommandBody(params object[] p);

    /// <summary>
    /// A class for one textUI command
    /// </summary>
    class Command
    {
        string name;
        string description;
        CommandBody command;
        //List<Type> parameters;

        public Command(string name, string description, CommandBody command /*, IEnumerable<Type> parameters*/)
        {
            this.name = name;
            this.description = description;
            this.command = command;
            //this.parameters = parameters.ToList();
        }

        public string Description
        {
            get
            {
                return "-" + name + "\n    " + description;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public void Execute(params object[] p)
        {
            try
            {
                command.Invoke(p);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error executing command " + name + ":");
                Console.WriteLine("    " + e.ToString());
            }
        }

    }
}
