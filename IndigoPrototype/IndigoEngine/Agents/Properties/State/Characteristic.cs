using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace IndigoEngine.Agents
{
	/// <summary>
	/// Class for some characteristic of the agent
	/// </summary>
    [Serializable]
    public class Characteristic : NameableObject, ITypicalCharacteristic
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private int maxValue = 100;            //Maximum value of the characteristic
        private int minValue = 0;              //Minimum value of the characteristic
        private int criticalPercentValue = 20; //Minimum value of the satisfied characteristic 
        private int currentValue;              //Current value of the characteristic

        #region Constructors

        public Characteristic()
            : base()
        {
            CurrentUnitValue = MaxValue;
        }

        #endregion

        #region Properties

			#region ITypicalCharacteristic realisation

				public int MaxValue
				{
					get
					{
						return maxValue;
					}
					set
					{
						if (value <= MinValue)
						{
							throw (new Exception(String.Format("Maximum value of {0} is less or equal to minimum: {1}!", this, value)));
						}
						maxValue = value;
						if (CurrentUnitValue > MaxValue)
						{
							CurrentUnitValue = MaxValue;
						}
					}
				}

				public int MinValue
				{
					get
					{
						return minValue;
					}
					set
					{
						if (value >= MaxValue)
						{
							throw (new Exception(String.Format("Minimum value of {0} is more or equal to maximum: {1}!", this, value)));
						}
						minValue = value;
						if (CurrentUnitValue < MinValue)
						{
							CurrentUnitValue = MinValue;
						}
					}
				}

				public int CurrentUnitValue
				{
					get
					{
						return currentValue;
					}
					set
					{
						if (value < MinValue)
						{
							currentValue = MinValue;
							return;
						}
						if (value > MaxValue)
						{
							currentValue = MaxValue;
							return;
						}
						currentValue = value;
					}
				}

				public int CriticalPercentValue
				{
					get
					{
						return criticalPercentValue;
					}
					set
					{
						if (value < 0)
						{
							criticalPercentValue = 0;
							return;
						}
						if (value > 100)
						{
							criticalPercentValue = 100;
							return;
						}
						criticalPercentValue = value;
					}
				}

				public int CurrentPercentValue
				{
					get
					{
						return (int)(((float)CurrentUnitValue / Math.Abs((float)(MaxValue - MinValue))) * 100f);
					}
					set
					{
						CurrentUnitValue = MinValue + (int)(Math.Abs((float)(MaxValue - MinValue)) * (float)value / 100f);
					}
				}

			#endregion

        #endregion

        #region Static methods

			public static Characteristic operator +(Characteristic argChar1, Characteristic argChar2)
			{
				Characteristic result = new Characteristic();
				if (argChar1.Name != argChar2.Name)
				{
					throw (new Exception(String.Format("You are trying to add 2 different characteristics: {0} and {1}", argChar1.Name, argChar2.Name)));
				}
				if (argChar1.MinValue != argChar2.MinValue || argChar1.MaxValue != argChar2.MaxValue)
				{
					throw (new Exception(String.Format("You are trying to add 2 characteristics with different borders: {0} and {1}", argChar1, argChar2)));
				}
				if (argChar1.CriticalPercentValue != argChar2.CriticalPercentValue)
				{
					throw (new Exception(String.Format("You are trying to add 2 characteristics with different critical values: {0} and {1}", argChar1.CriticalPercentValue, argChar2.CriticalPercentValue)));
				}
				result.CurrentUnitValue = (argChar1.CurrentUnitValue + argChar2.CurrentUnitValue);

				return result;
			}

			public static Characteristic operator -(Characteristic argChar1, Characteristic argChar2)
			{
				Characteristic result = new Characteristic();
				if (argChar1.Name != argChar2.Name)
				{
					throw (new Exception(String.Format("You are trying to substract 2 different characteristics: {0} and {1}", argChar1.Name, argChar2.Name)));
				}
				if (argChar1.MinValue != argChar2.MinValue || argChar1.MaxValue != argChar2.MaxValue)
				{
					throw (new Exception(String.Format("You are trying to substract 2 characteristics with different borders: {0} and {1}", argChar1, argChar2)));
				}
				if (argChar1.CriticalPercentValue != argChar2.CriticalPercentValue)
				{
					throw (new Exception(String.Format("You are trying to substract 2 characteristics with different critical values: {0} and {1}", argChar1.CriticalPercentValue, argChar2.CriticalPercentValue)));
				}
				result.CurrentUnitValue = (argChar1.CurrentUnitValue - argChar2.CurrentUnitValue);

				return result;
			}

			public static Characteristic operator +(Characteristic argChar1, int argValue)
			{
				Characteristic result = argChar1;
				result.CurrentUnitValue = argChar1.CurrentUnitValue + argValue;

				return result;
			}

			public static Characteristic operator -(Characteristic argChar1, int argValue)
			{
				Characteristic result = argChar1;
				result.CurrentUnitValue = argChar1.CurrentUnitValue - argValue;

				return result;
			}

        #endregion

        #region ObjectMethodsOverride

            public override string ToString()
            {
                return Name + ": " + MinValue.ToString() + "." + CurrentUnitValue.ToString() + "." + MaxValue.ToString();
            }

        #endregion
    }
}
