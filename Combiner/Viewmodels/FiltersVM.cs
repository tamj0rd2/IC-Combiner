﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Combiner
{
	public class FiltersVM : BaseViewModel
	{
		private CreatureDataVM m_CreatureDataVM;

		public FiltersVM(CreatureDataVM creatureDataVM)
		{
			m_CreatureDataVM = creatureDataVM;
		}


		private ObservableCollection<CreatureFilter> m_FilterChoices;
		public ObservableCollection<CreatureFilter> FilterChoices
		{
			get
			{
				return m_FilterChoices ??
					(m_FilterChoices = InitFilterChoices());
			}
			set
			{
				if (m_FilterChoices != value)
				{
					m_FilterChoices = value;
					OnPropertyChanged(nameof(FilterChoices));
				}
			}
		}

		private ObservableCollection<CreatureFilter> m_ChosenFilters;
		public ObservableCollection<CreatureFilter> ChosenFilters
		{
			get
			{
				return m_ChosenFilters ??
					(m_ChosenFilters = new ObservableCollection<CreatureFilter>());
			}
			set
			{
				if (m_ChosenFilters != value)
				{
					m_ChosenFilters = value;
					OnPropertyChanged(nameof(ChosenFilters));
				}
			}
		}

		private ObservableCollection<CreatureFilter> InitFilterChoices()
		{
			ObservableCollection<CreatureFilter> filterChoices = new ObservableCollection<CreatureFilter>();
			filterChoices.Add(new RankFilter());
			filterChoices.Add(new CoalFilter());
			filterChoices.Add(new ElectricityFilter());
			filterChoices.Add(new PowerFilter());
			filterChoices.Add(new HitpointsFilter());
			filterChoices.Add(new ArmourFilter());
			filterChoices.Add(new SightRadiusFilter());
			filterChoices.Add(new SizeFilter());
			filterChoices.Add(new EffectiveHitpointsFilter());
			filterChoices.Add(new LandSpeedFilter());
			filterChoices.Add(new WaterSpeedFilter());
			filterChoices.Add(new AirSpeedFilter());
			filterChoices.Add(new MeleeDamageFilter());
			filterChoices.Add(new RangeDamageFilter());
			filterChoices.Add(new AbilityFilter());
			filterChoices.Add(new StockFilter());
			filterChoices.Add(new SingleRangedFilter());
			filterChoices.Add(new HornsFilter());
			filterChoices.Add(new BarrierDestroyFilter());
			filterChoices.Add(new PoisonFilter());
			filterChoices.Add(new RangeOptionsFilter());
			return new ObservableCollection<CreatureFilter>(filterChoices.OrderBy(s => s.Name));
		}

		public CreatureFilter SelectedFilter { get; set; }

		private RelayCommand m_AddFilterCommand;
		public RelayCommand AddFilterCommand
		{
			get
			{
				return m_AddFilterCommand ??
					  (m_AddFilterCommand = new RelayCommand(AddFilter));
			}
			set
			{
				if (m_AddFilterCommand != value)
				{
					m_AddFilterCommand = value;
					OnPropertyChanged(nameof(AddFilter));
				}
			}
		}

		private void AddFilter(object o)
		{
			if (SelectedFilter != null)
			{
				ChosenFilters.Add(SelectedFilter);
				ChosenFilters = new ObservableCollection<CreatureFilter>(ChosenFilters.OrderBy(s => s.Name));
				FilterChoices.Remove(SelectedFilter);
			}
		}

		private RelayCommand m_DropFilterCommand;
		public RelayCommand DropFilterCommand
		{
			get
			{
				return m_DropFilterCommand ??
					(m_DropFilterCommand = new RelayCommand(DropFilter));
			}
			set
			{
				if (m_DropFilterCommand != value)
				{
					m_DropFilterCommand = value;
					OnPropertyChanged(nameof(DropFilterCommand));
				}
			}
		}

		private void DropFilter(object o)
		{
			CreatureFilter filter = o as CreatureFilter;
			if (filter != null)
			{
				FilterChoices.Add(filter);
				FilterChoices = new ObservableCollection<CreatureFilter>(FilterChoices.OrderBy(s => s.Name));
				ChosenFilters.Remove(filter);
			}
		}

		private RelayCommand m_DropAllFiltersCommand;
		public RelayCommand DropAllFiltersCommand
		{
			get
			{
				return m_DropAllFiltersCommand ??
					(m_DropAllFiltersCommand = new RelayCommand(DropAllFilters));
			}
			set
			{
				if (m_DropAllFiltersCommand != value)
				{
					m_DropAllFiltersCommand = value;
					OnPropertyChanged(nameof(DropAllFiltersCommand));
				}
			}
		}

		private void DropAllFilters(object o)
		{
			ChosenFilters.ToList().ForEach(FilterChoices.Add);
			FilterChoices = new ObservableCollection<Combiner.CreatureFilter>(FilterChoices.OrderBy(s => s.Name));
			ChosenFilters = new ObservableCollection<Combiner.CreatureFilter>();
		}

		private RelayCommand m_AddAllFiltersCommand;
		public RelayCommand AddAllFiltersCommand
		{
			get
			{
				return m_AddAllFiltersCommand ??
					(m_AddAllFiltersCommand = new RelayCommand(AddAllFilters));
			}
			set
			{
				if (m_AddAllFiltersCommand != value)
				{
					m_AddAllFiltersCommand = value;
					OnPropertyChanged(nameof(AddAllFiltersCommand));
				}
			}
		}

		private void AddAllFilters(object o)
		{
			FilterChoices.ToList().ForEach(ChosenFilters.Add);
			ChosenFilters = new ObservableCollection<Combiner.CreatureFilter>(ChosenFilters.OrderBy(s => s.Name));
			FilterChoices = new ObservableCollection<Combiner.CreatureFilter>();
		}

		private RelayCommand m_ResetFiltersCommand;
		public RelayCommand ResetFiltersCommand
		{
			get
			{
				return m_ResetFiltersCommand ??
					(m_ResetFiltersCommand = new RelayCommand(ResetFilters));
			}
			set
			{
				if (m_ResetFiltersCommand != value)
				{
					m_ResetFiltersCommand = value;
					OnPropertyChanged(nameof(ResetFiltersCommand));
				}
			}
		}

		private void ResetFilters(object o)
		{
			DropAllFilters(o);
			foreach (CreatureFilter filter in FilterChoices)
			{
				filter.ResetFilter();
			}
		}

		private ICommand m_FilterCreaturesCommand;
		public ICommand FilterCreaturesCommand
		{
			get
			{
				return m_FilterCreaturesCommand ??
				  (m_FilterCreaturesCommand = new RelayCommand(FilterCreatures));
			}
			set
			{
				if (value != m_FilterCreaturesCommand)
				{
					m_FilterCreaturesCommand = value;
					OnPropertyChanged(nameof(FilterCreaturesCommand));
				}
			}
		}
		private void FilterCreatures(object obj)
		{
			m_CreatureDataVM.CreaturesView.Filter = CreatureFilter;
		}

		public bool CreatureFilter(object obj)
		{
			if (ChosenFilters.Count == 0)
			{
				return true;
			}

			Creature creature = obj as Creature;
			if (creature != null)
			{
				bool result = true;
				foreach (CreatureFilter filter in ChosenFilters)
				{
					result = result && filter.Filter(creature);
				}
				return result;
			}
			return false;
		}
	}
}
