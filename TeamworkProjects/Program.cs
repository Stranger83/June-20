using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkProjects
{
	class Program
	{
		class Team
		{
			public string Name { get; set; }
			public List<string> Members { get; set; }
			public string Creator { get; set; }
		}
		static void Main(string[] args)
		{
			int n = int.Parse(Console.ReadLine());
			List<Team> allTeams = new List<Team>();
			for (int i = 0; i < n; i++)
			{
				var input = Console.ReadLine().Split('-');
				var creator = input[0];
				var teamName = input[1];
				if (allTeams.Select(t => t.Name).Contains(teamName))
				{
					Console.WriteLine($"Team {teamName} was already created!");
				}
				else if (allTeams.Select(c => c.Creator).Contains(creator))
				{
					Console.WriteLine($"{creator} cannot create another team!");
				}
				else
				{
					Team team = new Team
					{
						Creator = creator,
						Name = teamName,
						Members = new List<string>()
					};
					allTeams.Add(team);
					Console.WriteLine($"Team {teamName} has been created by {creator}!");
				}
			}
			while (true)
			{
				var input = Console.ReadLine();
				if (input == "end of assignment")
				{
					break;
				}
				string separator = "->";
				var tokens = input.Split(new[] { separator }, StringSplitOptions.None);
				var user = tokens[0];
				var teamToJoin = tokens[1];
				if (!allTeams.Select(t => t.Name).Contains(teamToJoin))
				{
					Console.WriteLine($"Team {teamToJoin} does not exist!");
				}
				else if (allTeams.Any(m => m.Members.Contains(user)) ||
				  allTeams.Select(c => c.Creator).Contains(user))
				{
					Console.WriteLine($"Member {user} cannot join team {teamToJoin}!");
				}
				else
				{
					foreach (var team in allTeams)
					{
						if (team.Name == teamToJoin)
						{
							team.Members.Add(user);
						}
					}
				}
			}
			var teamsToDisband = allTeams.Where(m => m.Members.Count == 0)
					.OrderBy(t => t.Name)
					.ToList();
			var sortedTeams = allTeams.Where(m => m.Members.Count > 0)
				.OrderByDescending(m => m.Members.Count)
				.ThenBy(t => t.Name)
				.ToList();

			foreach (var team in sortedTeams)
			{
				Console.WriteLine(team.Name);
				Console.WriteLine("- " + team.Creator);
				team.Members = team.Members.OrderBy(m => m).ToList();
				foreach (var member in team.Members)
				{
					Console.WriteLine($"-- {member}");
				}
			}
			Console.WriteLine("Teams to disband:");
			foreach (var team in teamsToDisband)
			{
				Console.WriteLine(team.Name);
			}
		}
	}
}
