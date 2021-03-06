﻿using Server.Models;
using Server.WcfModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Server
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
	public class LigaService : ILigaService
	{
		public string Test(string test)
		{
			return string.Format("You entered: {0}", test);
		}




		public List<WcfBettor> GetAllBettors()
		{
			List<WcfBettor> bettors = new List<WcfBettor>();
			foreach(Bettor bettor in Database.GetBettors())
			{
				bettors.Add(new WcfBettor(bettor));
			}
			return bettors;
		}

		public WcfBettor GetBettorById(int id)
		{
			return new WcfBettor(Database.GetBettorById(id));
		}

		public WcfBettor GetBettorByName(string name)
		{
			return new WcfBettor(Database.GetBettorByName(name));
		}

		public void AddBettor(WcfBettor bettor)
		{
			Database.AddBettor(bettor.Firstname, bettor.Lastname, bettor.Nickname);
		}

		public void EditBettor(WcfBettor bettor)
		{
			Database.EditBettor(Database.GetBettorById(bettor.Id), bettor.Firstname, bettor.Lastname, bettor.Nickname);
		}

		public void DeleteBettor(WcfBettor bettor)
		{
			Database.DeleteBettor(Database.GetBettorById(bettor.Id));
		}

		public bool CheckBettor(String name)
		{
			return Database.CheckBettor(name);
		}




		public List<WcfTeam> GetAllTeams()
		{
			List<WcfTeam> teams = new List<WcfTeam>();
			foreach(Team team in Database.GetTeams())
			{
				teams.Add(new WcfTeam(team));
			}
			return teams;
		}

		public List<WcfTeam> GetTeams(WcfSeason season)
		{
			List<WcfTeam> teams = new List<WcfTeam>();
			foreach (SeasonsToTeamsRelation relation in Database.GetSeasonById(season.Id).TeamRelations)
			{
				//teams.Add(new WcfTeam(relation.Team));
			}
			return teams;
		}

		public WcfTeam GetTeamById(int id)
		{
			return new WcfTeam(Database.GetTeamById(id));
		}

		public void AddTeam(WcfTeam team)
		{
			Database.AddTeam(team.Name);
		}

		public void EditTeam(WcfTeam team)
		{
			Database.EditTeam(Database.GetTeamById(team.Id), team.Name);
		}

		public void DeleteTeam(WcfTeam team)
		{
			Database.DeleteTeam(Database.GetTeamById(team.Id));
		}

		public void AddTeamToSeason(WcfTeam team, WcfSeason season)
		{
			Database.AddSeasonsToTeamsRelation(Database.GetTeamById(team.Id), Database.GetSeasonById(season.Id));
		}

		public void DeleteTeamFromSeason(WcfTeam team, WcfSeason season)
		{
			Database.DeleteMatches(season.Id, Database.GetTeamById(team.Id));
			Database.AddSeasonsToTeamsRelation(Database.GetTeamById(team.Id), Database.GetSeasonById(season.Id));
		}



		public List<WcfMatch> GetAllMatches()
		{
			List<WcfMatch> matches = new List<WcfMatch>();
			foreach(Match match in Database.GetMatches())
			{
				matches.Add(new WcfMatch(match));
			}
			return matches;
		}

		public List<WcfMatch> GetMatches(WcfSeason season)
		{
			List<WcfMatch> matches = new List<WcfMatch>();
			foreach (Match match in Database.GetSeasonById(season.Id).Matches)
			{
				matches.Add(new WcfMatch(match));
			}
			return matches;
		}

		public WcfMatch GetMatchById(int id)
		{
			return new WcfMatch(Database.GetMatchById(id));
		}

		public void AddMatch(WcfMatch match)
		{
			Database.AddMatch(match.SeasonId, 
				Database.GetTeamById(match.HomeTeamId), 
				Database.GetTeamById(match.AwayTeamId), 
				match.HomeTeamScore, 
				match.AwayTeamScore, 
				match.Date,
				match.MatchDay);
		}

		public void EditMatch(WcfMatch match)
		{
			Database.EditMatch(Database.GetMatchById(match.Id), match.HomeTeamScore, match.AwayTeamScore, match.Date, match.MatchDay);
		}

		public void DeleteMatch(WcfMatch match)
		{
			Database.DeleteMatch(Database.GetMatchById(match.Id));
		}

		public void GenerateMatches(WcfSeason season)
		{
			SeasonGenerator.GenerateMatches(Database.GetSeasonById(season.Id), season.StartDate);
		}


		public List<WcfSeason> GetAllSeasons()
		{
			List<WcfSeason> seasons = new List<WcfSeason>();
			foreach(Season season in Database.GetSeasons())
			{
				seasons.Add(new WcfSeason(season));
			}
			return seasons;
		}

		public WcfSeason GetSeasonById(int id)
		{
			return new WcfSeason(Database.GetSeasonById(id));
		}

		public void AddSeason(WcfSeason season)
		{
			Database.AddSeason(season.Name, season.Description, season.StartDate, season.Sequence);
		}

		public void EditSeason(WcfSeason season)
		{
			Database.EditSeason(Database.GetSeasonById(season.Id), season.Name, season.Description, season.StartDate);
		}

		public void DeleteSeason(WcfSeason season)
		{
			Database.DeleteSeason(Database.GetSeasonById(season.Id));
		}




		public List<WcfBet> GetAllBets()
		{
			List<WcfBet> bets = new List<WcfBet>();
			foreach(Bet bet in Database.GetBets())
			{
				bets.Add(new WcfBet(bet));
			}
			return bets;
		}

		public List<WcfBet> GetBets(WcfBettor bettor)
		{
			List<WcfBet> bets = new List<WcfBet>();
			foreach (Bet bet in Database.GetBettorById(bettor.Id).Bets)
			{
				bets.Add(new WcfBet(bet));
			}
			return bets;
		}

		public WcfBet GetBetById(int id)
		{
			return new WcfBet(Database.GetBetById(id));
		}

		public void AddBet(WcfBet bet)
		{
			Database.AddBet(Database.GetBettorById(bet.BettorId), Database.GetMatchById(bet.MatchId), bet.HomeTeamScore, bet.AwayTeamScore, bet.Date);
		}

		public void EditBet(WcfBet bet)
		{
			Database.EditBet(Database.GetBetById(bet.Id), bet.HomeTeamScore, bet.AwayTeamScore);
		}

		public void DeleteBet(WcfBet bet)
		{
			Database.DeleteBet(Database.GetBetById(bet.Id));
		}




		public List<WcfRelation> GetAllRelations()
		{
			List<WcfRelation> relations = new List<WcfRelation>();
			foreach(SeasonsToTeamsRelation relation in Database.GetSeasonsToTeamsRelations())
			{
				relations.Add(new WcfRelation(relation));
			}
			return relations;
		}

		public List<WcfRelation> GetRelationsByTeam(WcfTeam team)
		{
			List<WcfRelation> relations = new List<WcfRelation>();
			foreach (SeasonsToTeamsRelation relation in Database.GetSeasonsToTeamsRelationsByTeamId(team.Id))
			{
				relations.Add(new WcfRelation(relation));
			}
			return relations;
		}

		public List<WcfRelation> GetRelationsBySeason(WcfSeason season)
		{
			List<WcfRelation> relations = new List<WcfRelation>();
			foreach (SeasonsToTeamsRelation relation in Database.GetSeasonsToTeamsRelationsBySeasonId(season.Id))
			{
				relations.Add(new WcfRelation(relation));
			}
			return relations;
		}

		public void DeleteRelation(WcfRelation relation)
		{
			Database.DeleteSeasonsToTeamsRelation(Database.GetSeasonsToTeamsRelationById(relation.Id));
		}
	}
}
