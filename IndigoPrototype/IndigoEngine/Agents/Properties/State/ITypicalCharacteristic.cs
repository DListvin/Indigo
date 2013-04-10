using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{
	interface ITypicalCharacteristic
	{
		/// <summary>
		/// Operations with maximum value of the characteristic
		/// </summary>
		int MaxValue { get; set; }

		/// <summary>
		/// Operations with minimum value of the characteristic
		/// </summary>
		int MinValue { get; set; }
		
		/// <summary>
		/// Operations with characteristic value in units
		/// </summary>
		int CurrentUnitValue { get; set; }

        /// <summary>
        /// Operations with characteristic critical value in units
        /// </summary>
        int CriticalPercentValue { get; set; }

		/// <summary>
		/// Operations with characteristic value in percents
		/// </summary>
		int CurrentPercentValue { get; set; }
	}
}
