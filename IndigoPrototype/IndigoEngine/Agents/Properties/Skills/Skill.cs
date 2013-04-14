using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace IndigoEngine.Agents
{
	/// <summary>
	/// Class for some skill of the agent
	/// </summary>
    [Serializable]
    public class Skill : NameableObject, ITypicalSkill
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        #region Constructors

        public Skill()
            : base()
        {
            SkillQuality = 0;
        }

        #endregion

        #region Properties

        #region ITypicalSkill realisation

        public uint SkillQuality { get; set; } //Level of skill

        #endregion

        #endregion

        #region ObjectMethodsOverride

        #endregion
    }
}
