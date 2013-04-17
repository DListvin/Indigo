using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace IndigoEngine.Agents
{
	/// <summary>
	/// Class for storaging characteristics of alive agents
	/// </summary>
    [Serializable]
    public class StateLiving : State
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        #region Constructors

			public StateLiving()
				: base()
			{
				Strenght = new Characteristic();
				Strenght.Name = "Strenght";

				Stamina = new Characteristic();
				Stamina.Name = "Stamina";

				Intelligence = new Characteristic();
				Intelligence.Name = "Intelligence";

				FoodSatiety = new Characteristic();
				FoodSatiety.Name = "FoodSatiety";

				WaterSatiety = new Characteristic();
				WaterSatiety.Name = "WaterSatiety";

				Peacefulness = new Characteristic();
				Peacefulness.Name = "Peacefulness";
			}

        #endregion

        #region Properties

			public Characteristic Strenght { get; set; }

			public Characteristic Stamina { get; set; }

			public Characteristic Intelligence { get; set; }

			public Characteristic FoodSatiety { get; set; }

			public Characteristic WaterSatiety { get; set; }

			public Characteristic Peacefulness { get; set; }

        #endregion

        #region Static methods

			public static StateLiving operator +(StateLiving argState1, StateLiving argState2)
			{
				StateLiving result = new StateLiving();


				result.Health = argState1.Health + argState2.Health;
				result.Strenght = argState1.Strenght + argState2.Strenght;
				result.Stamina = argState1.Stamina + argState2.Stamina;
				result.Intelligence = argState1.Intelligence + argState2.Intelligence;
				result.FoodSatiety = argState1.FoodSatiety + argState2.FoodSatiety;
				result.WaterSatiety = argState1.WaterSatiety + argState2.WaterSatiety;
				result.Peacefulness = argState1.Peacefulness + argState2.Peacefulness;

				return result;
			}

			public static StateLiving operator -(StateLiving argState1, StateLiving argState2)
			{
				StateLiving result = new StateLiving();

				result.Health = argState1.Health - argState2.Health;
				result.Strenght = argState1.Strenght - argState2.Strenght;
				result.Stamina = argState1.Stamina - argState2.Stamina;
				result.Intelligence = argState1.Intelligence - argState2.Intelligence;
				result.FoodSatiety = argState1.FoodSatiety - argState2.FoodSatiety;
				result.WaterSatiety = argState1.WaterSatiety - argState2.WaterSatiety;
				result.Peacefulness = argState1.Peacefulness - argState2.Peacefulness;

				return result;
			}

        #endregion

        #region ObjectMethodsOverride

        #endregion
    }
}
