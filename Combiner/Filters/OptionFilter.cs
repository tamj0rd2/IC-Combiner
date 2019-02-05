﻿using System;

namespace Combiner
{
	public abstract class OptionFilter : CreatureFilter
	{
		public event EventHandler IsOptionCheckChanged;

		public OptionFilter(string name)
			: base(name) { }

		private bool m_IsOptionChecked;
		public bool IsOptionChecked
		{
			get { return m_IsOptionChecked; }
			set
			{
				if (m_IsOptionChecked != value)
				{
					m_IsOptionChecked = value;
					OnPropertyChanged(nameof(IsOptionChecked));
				}
			}
		}

		protected abstract bool OnOptionChecked(Creature creature);

		public override bool Filter(Creature creature)
		{
			if (IsOptionChecked)
			{
				return OnOptionChecked(creature);
			}
			return true;
		}

		public override void ResetFilter()
		{
			IsOptionChecked = false;
		}
	}
}
