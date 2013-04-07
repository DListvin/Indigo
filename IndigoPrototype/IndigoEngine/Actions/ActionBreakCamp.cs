using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using System.Drawing;

namespace IndigoEngine
{
    /// <summary>
    /// Example of action. Delete it after you invent some actual actions
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
            int k = 0;
            foreach (Agent ag in Subject.Inventory)
            {
                if (ag.GetType() == typeof(AgentItemLog))
                    k++;
            }
            if (k < 2)
            {
                throw (new Exception("Agent " + Object.ToString() + " does not have 2 logs to break Camp"));
                // here must not be exception, but smth like event to say to agent, that he is LOX
            }

            Object.CurrentActionFeedback = new ActionFeedback(() =>
            {
                Object.Inventory.DeleteAgentByType(typeof(AgentItemLog));
                Object.Inventory.DeleteAgentByType(typeof(AgentItemLog));
                //generate event to world to create the camp

                ((AgentLiving)Object).AgentsShortMemory.StoreAction(Subject, this);
            });
        }

        /// <summary>
        /// From direction gives increment to position
        /// </summary>
        /// <returns>Point like (0,1) (-1,0) or (1,-1)</returns>
        private Point Normilize(Point dir)
        {
            if (Math.Abs(dir.X) > Math.Abs(dir.Y))
            {
                return new Point((dir.X < 0) ? -1 : 1, 0);
            }
            if (Math.Abs(dir.Y) > Math.Abs(dir.X))
            {
                return new Point(0, (dir.Y < 0) ? -1 : 1);
            }
            return new Point((dir.X < 0) ? -1 : 1, (dir.Y < 0) ? -1 : 1);
        }

        public override string ToString()
        {
            return "Action: " + Name;
        }
    }
}
