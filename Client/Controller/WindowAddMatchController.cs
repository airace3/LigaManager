﻿using Client.Framework;
using Client.ViewModels;
using Client.Views;
using Server.WcfModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controller
{
	class WindowAddMatchController
	{
		private WindowAddMatch mView;
		private WindowAddMatchViewModel mViewModel;
		private WcfSeason mSeason;
		public WcfMatch AddMatch(WcfSeason season)
		{
			mSeason = season;
			mView = new WindowAddMatch();
			mViewModel = new WindowAddMatchViewModel
			{
				Match = new WcfMatch(),
				Teams = new ObservableCollection<TeamHelper>(),
				OkCommand = new RelayCommand(ExecuteOkCommand),
				CancelCommand = new RelayCommand(ExecuteCancelCommand)
			};
			ReloadTeams();
			mView.Title = "Add New Match";
			mView.DataContext = mViewModel;
			mViewModel.Match.AwayTeamScore = 0;
			mViewModel.Match.HomeTeamScore = 0;
			mViewModel.Match.SeasonId = season.Id;

			return mView.ShowDialog() == true ? mViewModel.Match : null;
		}

		private void ExecuteOkCommand(object obj)
		{
			mView.DialogResult = true;
			mView.Close();
		}

		private void ExecuteCancelCommand(object obj)
		{
			mView.DialogResult = false;
			mView.Close();
		}

		private void ReloadTeams()
		{
			mViewModel.Teams.Clear();
			foreach (WcfRelation relation in WcfHelper.client.GetRelationsBySeason(mSeason))
			{
				mViewModel.Teams.Add(new TeamHelper(relation));
			}
		}
	}
}
