using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using System.Drawing;
using NLog;
using IndigoEngine.Agents.Properties.Memory;

namespace IndigoEngine.ActionsOld
{
    /// <summary>
    /// Action to wander around the world, if nothing to do.
    /// </summary>
    [Serializable]
    [ActionInfo(
                    new Type[]
					{
						typeof(AgentLivingIndigo),
					},
                    new Type[]
					{
					},
                    IsConflict = true,
                    RequiresObject = false
                )]
    class ActionWander : ActionGo
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        #region Constructors

            public ActionWander(Agent argSubj)
                : base(argSubj)
            {
                Direction = Normilize(GetDirection(argSubj), argSubj.CurrentLocation.Coords);
            }

        #endregion

        Point GetDirection(Agent argSubj)
        {

            //Check, if already have a target
            Agent target = null;
            foreach (var kvp in argSubj.CurrentMemory.StoredAgents)
            {
                if(kvp.Value.Exists(i =>
                {
                    if (i.StoredInfo.GetType() == typeof(TargetInfo))
                        return (i.StoredInfo as TargetInfo).IsTarget;
                    return false;
                }))
                {
                    if (Agent.Distance(argSubj, kvp.Key) < Math.Sqrt(2))
                    {
                        //Target already reached and should be deleted
                        //Better do do it in feedback
                        kvp.Value.Remove(kvp.Value.First(i =>
                        {
                            if (i.StoredInfo.GetType() == typeof(TargetInfo))
                                return (i.StoredInfo as TargetInfo).IsTarget;
                            return false;
                        }));
                    }
                    else
                    {
                        target = kvp.Key;
                    }
                }
            }

            if (target != null)
            {
                //Has a proper target
                return target.CurrentLocation.Coords;
            }

            //Find new target
            Random rnd = new Random();
            int n = rnd.Next(0, argSubj.CurrentVision.CurrentViewAgents.Where(i => { return !i.CurrentLocation.HasOwner; }).Count() - 1);
            target = argSubj.CurrentVision.CurrentViewAgents.Where(i => { return !i.CurrentLocation.HasOwner; }).ToList()[n];

            if (target != null)
            {
                //new target was selected
                //Updating memory, better have proper methods of memory for this and do it in feedback
                StoredInformation info = new StoredInformation();
                info.StoredInfo = new TargetInfo(true);
                if (argSubj.CurrentMemory.StoredAgents.ContainsKey(target))
                    argSubj.CurrentMemory.StoredAgents[target].Add(info);
                else
                {
                    List<StoredInformation> temp = new List<StoredInformation>();
                    temp.Add(info);
                    argSubj.CurrentMemory.StoredAgents.Add(target, temp);
                }

                return target.CurrentLocation.Coords;
            }

            return new Point();
        }


    }
}
