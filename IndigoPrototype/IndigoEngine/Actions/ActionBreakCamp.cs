using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using System.Drawing;

namespace IndigoEngine
{
    /// <summary>
    /// Action to break the camp
    /// </summary>
    public class ActionBreakCamp : Action
    {
        Point direction; // where from object will be camp

        #region Constructors

        public ActionBreakCamp(Agent argObj, Point dir)
            : base(argObj, null)
        {
            direction = Normilize(dir);
            MayBeConflict = true;
            AcceptedObj.Add(typeof(AgentLiving));
            AcceptedObj.Add(typeof(AgentLivingIndigo));
            AcceptedSubj.Add(typeof(AgentCamp));
            Name = "To Break a Camp";
        }

        #endregion

        #region Properties



        #endregion

        /// <summary>
        /// ITypicalAction
        /// </summary>
        public override void Perform()
        {
            base.Perform();

            int k = Subject.Inventory.CountNumberOfAgentsByType(typeof(AgentItemLog));      //Number of logs int subject's inventory

            if (k < 2)
            {
                throw (new Exception("Agent " + Object.ToString() + " does not have 2 logs to break Camp"));
                // here must not be exception, but smth like event to say to agent, that he is LOX
            }

            Subject.CurrentActionFeedback = new ActionFeedback(() =>
            {
                Subject.Inventory.DeleteAgentsByType(typeof(AgentItemLog), 2);
                //generate event to world to create the camp

                ((AgentLiving)Subject).AgentsShortMemory.StoreAction(Subject, this);
            });
        }

        public override string ToString()
        {
            return "Action: " + Name;
        }
    }
}
