﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class StockFilter : SelectionFilter
	{
		public StockFilter()
			: base("Stock") { }

		protected override bool FilterAnySelected(Creature creature)
		{
			return Selected.Contains(creature.Left)
				|| Selected.Contains(creature.Right);
		}

		protected override bool FilterOnlySelected(Creature creature)
		{
			return Selected.Contains(creature.Left)
				&& Selected.Contains(creature.Right);
		}

		protected override ObservableCollection<string> InitChoices()
		{
			ObservableCollection<string> choices = new ObservableCollection<string>();
			var stockNames = Directory.GetFiles(DirectoryConstants.StockDirectory).
				Select(s => s.Replace(".lua", "").Replace(DirectoryConstants.StockDirectory, ""));
			foreach (string stock in stockNames)
			{
				choices.Add(StockNames.ProperStockNames[stock]);
			}
			choices = new ObservableCollection<string>(choices.OrderBy(s => s));
			return choices;
		}

		public override string ToString()
		{
			return nameof(StockFilter);
		}
	}
}
