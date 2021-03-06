﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Combiner
{
	public class RangeSpecialConverter : IValueConverter
	{
		public object Convert(object value)
		{
			return Convert(value, null, null, null);
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			int rangeSpecial = (int)(double)value;
			string result;
			switch (rangeSpecial)
			{
				case 0:
					result = "None";
					break;
				case 1:
					result = "Rock";
					break;
				case 2:
					result = "Water";
					break;
				case 3:
					result = "Chem";
					break;
				default:
					result = "??";
					break;
			}
			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
