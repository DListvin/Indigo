using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;

namespace IndigoEngine
{
    class ActionExample : Action
    {
        int health;

        public ActionExample(Agent obj, Agent subj, int health)
            : base(obj, subj)
        {
            this.health = health;
            this.mayBeConflict = true;
        }

        public override void Perform()
        {
            if (Object.Health.CurrentPercentValue > 60)
            {
                Object.ActionFeedback = new ActionFeedback(() => { Object.Health.CurrentUnitValue -= health; });

                Subject.ActionFeedback = new ActionFeedback(() => { if(Subject.Health.CurrentUnitValue + health <= Subject.Health.MaxValue) Subject.Health.CurrentUnitValue += health; });
            }
        }
    }
}
