using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{
	/// <summary>
	/// Class for some characteristic of the agent
	/// </summary>
	public class Characteristic : NameableObject, ITypicalCharacteristic
	{
		private int maxValue = 100;     //Maximum value of the characteristic
		private int minValue = 0;   //Minimum value of the characteristic
		private int currentValue;     //Current value of the characteristic

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
			get { return maxValue; }
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
			get	{ return minValue; }
			set
			{ 
				if(value >= MaxValue)
				{
					throw(new Exception(String.Format("Minimum value of {0} is more or equal to maximum: {1}!", this, value)));
				}
				minValue = value;
				if(CurrentUnitValue < MinValue)
				{
					CurrentUnitValue = MinValue;
				}
			}
		}
			
		public int CurrentUnitValue
		{
			get	{ return currentValue; }
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
			get	{ return (int)(((float)CurrentUnitValue / Math.Abs((float)(MaxValue - MinValue))) * 100f); }
			set	{ CurrentUnitValue = MinValue + (int)(Math.Abs((float)(MaxValue - MinValue)) * (float)value / 100f); }
		}

		#endregion

		#endregion

		public override string ToString()
		{
			return Name + ": " + MinValue.ToString() + "..." + CurrentUnitValue.ToString() + "..." + MaxValue.ToString();
		}
	}
}
