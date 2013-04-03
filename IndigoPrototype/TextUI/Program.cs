using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine;
using IndigoEngine.Agents;

namespace TextUI
{
    class Program
    {
        static void Main(string[] args)
        {
            IObservableModel model = new Model();

            TextUIShell.Model = model;

            TextUIShell.Run();
        }
    }
}
