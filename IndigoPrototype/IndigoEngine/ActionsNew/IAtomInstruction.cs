﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;

namespace IndigoEngine.ActionsNew
{
    /// <summary>
    /// Most simple action, component of other actions
    /// </summary>
    interface IAtomInstruction
    {
        Agent TargetAgent { get; } //TODO: not agent, but some interface

        void Perform(); //Perform instruction
    }
}