using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using System.Drawing;

namespace IndigoEngine
{
    class ActionGo : Action
    {
        Point direction;

        #region Constructors

        public ActionGo(Agent argSubj, Point dir)
            : base()
        {
            Subject = argSubj;
            direction = Normilize(dir);
            MayBeConflict = true;
            AcceptedObj.Add(typeof(AgentLiving));
            AcceptedObj.Add(typeof(AgentLivingIndigo));
        }

        #endregion

        /// <summary>
        /// ITypicalAction
        /// </summary>
        public override void Perform()
        {
            base.Perform();
            Subject.CurrentActionFeedback = new ActionFeedback(() =>
                {

                    direction = new Point(Subject.Location.Value.X + direction.X, Subject.Location.Value.Y + direction.Y);
                    Subject.Location = direction;
                    (Subject.CurrentState as StateLiving).Stamina.CurrentUnitValue--;
                });
        }

        /// <summary>
        /// From direction gives increment to position
        /// </summary>
        /// <returns>Point like (0,1) (-1,0) or (1,-1)</returns>
        private Point Normilize( Point dir)
        {
            if (Math.Abs(dir.X) > Math.Abs(dir.Y))
            {
                return new Point((dir.X < 0) ? -1 : 1, 0);
            }
            if (Math.Abs(dir.Y) > Math.Abs(dir.X))
            {
                return new Point(0,(dir.Y < 0) ? -1 : 1);
            }
            return new Point((dir.X < 0) ? -1 : 1, (dir.Y < 0) ? -1 : 1);
        }

        public override string ToString()
        {
            return "Action: go";
        }
    }
}
