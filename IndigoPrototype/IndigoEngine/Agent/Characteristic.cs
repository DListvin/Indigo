using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{
	public class Characteristic :NameableObject, ITypicalCharacteristic
	{
		private string name;   //Characteristic name

		private int maxValue;           //Maximum value of the characteristic
		private const int minValue = 0; //Minimum value of the characteristic
		private int currentValue;       //Current value of the characteristic

		#region Constructors

			public Characteristic() : base()
			{
				MaxValue = 100;
				CurrentUnitValue = MaxValue;
			}

		#endregion

		#region Properties

			#region INamabelObject realisation
				
				public string Name
				{
					get
					{
						return name;
					}
					set
					{
						name = value;
					}
				}

			#endregion

			#region ITypicalCharacteristic realisation

				public int MaxValue
				{
					get
					{
						return maxValue;
					}
					set
					{	
						if(value <= MinValue)
						{
							throw(new Exception(String.Format("Maximum value of {0} is less or equal to minimum: {1}!", this, value)));
						}
						maxValue = value;
						if(CurrentUnitValue > MaxValue)
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
				}
			
				public int CurrentUnitValue
				{
					get
					{
						return currentValue;
					}
					set
					{
						if(value < MinValue || value > MaxValue)
						{
							throw(new Exception(String.Format("Current value of {0} is out of borders: {1}!", this, value)));						
						}
						currentValue = value;
					}
				}

				public int CurrentPercentValue
				{
					get
					{
						return (int)(((float)CurrentUnitValue / (float)MaxValue) * 100f);
					}
					set
					{
						CurrentUnitValue = (int)((float)MaxValue * (float)value / 100f);
					}
				}

			#endregion

		#endregion

		public override string ToString()
		{
			return Name + ": " + MinValue.ToString() + "..." + CurrentUnitValue.ToString() + "..." + MaxValue.ToString();
		}
	}
}
