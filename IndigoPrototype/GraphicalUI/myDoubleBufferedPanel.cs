using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;

namespace GraphicalUI
{
    class myDoubleBufferedPanel : Panel
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

        public myDoubleBufferedPanel()
        {
            this.DoubleBuffered = true;
        }
    }
}
