using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{
    /// <summary>
    /// Class of all Characteristics in world
    /// </summary>
    static class Characteristics
    {
        public static Characteristic Health
        {
            get { return new Characteristic("Health"); }
        }

        public static Characteristic Strenght
        {
            get { return new Characteristic("Strenght"); }
        }
        public static Characteristic Stamina
        {
            get { return new Characteristic("Stamina"); }
        }
        public static Characteristic Intelligence
        {
            get { return new Characteristic("Intelligence"); }
        }
        public static Characteristic FoodSatiety
        {
            get { return new Characteristic("FoodSatiety"); }
        }
        public static Characteristic WaterSatiety
        {
            get { return new Characteristic("WaterSatiety"); }
        }
        public static Characteristic Peacefulness
        {
            get { return new Characteristic("Peacefulness"); }
        }

        public static Characteristic Prolificacy
        {
            get
            {
                var Prolificacy = new Characteristic("Prolificacy");
                Prolificacy.CriticalPercentValue = 10;
                return Prolificacy;
            }
        }
    }
}
