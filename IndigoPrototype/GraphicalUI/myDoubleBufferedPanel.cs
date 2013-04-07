using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GraphicalUI
{
    class myDoubleBufferedPanel : Panel
    {
        public myDoubleBufferedPanel()
        {
            this.DoubleBuffered = true;
        }
    }
}
