using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.ActionsNew
{
    public class ActionAbstract : NameableObject
    {
        List<ActionForOneAgent> actions;
        ActionInfo actionInfo;

        public ActionAbstract()
        {
            actions = new List<ActionForOneAgent>();
            actionInfo = new ActionInfo();
        }

        public ActionAbstract(List<ActionForOneAgent> argActionsForOneAgent)
        {
            actions = argActionsForOneAgent;
        }
        public ActionAbstract(params ActionForOneAgent[] argActionsForOneAgent)
        {
            actions = new List<ActionForOneAgent>();
            foreach (ActionForOneAgent AOA in argActionsForOneAgent)
                actions.Add(AOA);
        }
        public void Add(ActionForOneAgent argActionsForOneAgent)
        {
            actions.Add(argActionsForOneAgent);
        }
        /// <summary>
        /// Complete all regular instructions of action and return all global instructions of action. It is optimization
        /// </summary>
        /// <returns>all global instructions of action</returns>
        public List<GlobalInstruction> CompleteRegular()
        {
            List<GlobalInstruction> ans = new List<GlobalInstruction>();
            foreach (ActionForOneAgent action in actions)
                ans.Concat(action.CompleteRegular());
            return ans;
        }

    }
}
