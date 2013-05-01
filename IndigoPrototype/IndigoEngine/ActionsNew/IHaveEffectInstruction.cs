using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;

namespace IndigoEngine.ActionsNew
{
    public interface IHaveEffectInstruction
    {
        bool PositiveEffect { get; }
        bool NegativeEffect { get; }
        Characteristic Characteristic { get;}
    }
}
