using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{
    public class Quality
    {
        public List<Qualities> Qualities { get; private set; }

        #region Constructiors
        public Quality()
        {
            Qualities = new List<Qualities>();
        }

        public Quality(List<Qualities> argQualities)
        {
            Qualities = argQualities;
        }

        public Quality(params Qualities[] argQualities)
            : this()
        {
            foreach (Qualities qua in argQualities)
                Qualities.Add(qua);
        }
        #endregion

        public bool Is(Qualities qual)
        {
            return Qualities.Exists( val => { return val == qual;});
        }
    }

    public enum Qualities
    {
        Eatable,
        Drinkable,
        Destroyable,
        Desideable,
        Moving,
        Createable
    }
}
