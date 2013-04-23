using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace IndigoEngine.Agents
{
	/// <summary>
	/// Class for storaging the characteristics of agents
	/// </summary>
    [Serializable]
    public class State
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        #region Constructors

			public State()
			{
				Health = new Characteristic();
				Health.Name = "Health";
			}

        #endregion

        #region Properties

			public Characteristic Health { get; set; } //Agent health

			protected int NumberOfCharacteristicsInState //Number of characteristics in the state
			{
				get
				{
					int result = 0; //result of the function

					foreach (System.Reflection.PropertyInfo info in (typeof(State)).GetProperties())
					{
						if (info.PropertyType == typeof(Characteristic))
						{
							++result;
						}
					}

					return result;
				}
			}

        #endregion

        #region Static methods

			public static State operator +(State argState1, State argState2)
			{
				State result = new State();

				result.Health = argState1.Health + argState2.Health;

				return result;
			}

			public static State operator -(State argState1, State argState2)
			{
				State result = new State();

				result.Health = argState1.Health - argState2.Health;

				return result;
			}

        #endregion
		

		/// <summary>
		/// Set currentValue in each characteristic between borders. Use it in the end of the iteration
		/// </summary>
		public void Reduct()
		{
			foreach(Characteristic ch in this)
			{
				ch.Reduct();
			}
		}

        /// <summary>
        /// IEnumerator for foreach
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Characteristic> GetEnumerator()
        {
            foreach (System.Reflection.PropertyInfo ch in this.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Where(field =>
            {
                return field.PropertyType == typeof(Characteristic);
            }))
            {
                yield return ch.GetValue(this, null) as Characteristic;
            }
        }

        #region ObjectMethodsOverride

            public override string ToString()
            {
                string result = "\n   ";  //Result of the function
                int timeForNewString = 1; //Showingm if the string should be new (shows 3 characteristics in one string)

                foreach (Characteristic ch in this)
                {
                    result += ch.ToString() + "; " + ((timeForNewString % 3 == 0) ? "\n   " : "");
                    ++timeForNewString;
                }
                return result + "\n";
            }

        #endregion
    }
}
