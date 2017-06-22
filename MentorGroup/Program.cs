using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorGroup
{
	class Program
	{
		class User
		{
			public string Name { get; set; }
			public List<DateTime> Dates { get; set; }
			public List<string> Comments { get; set; }
		}
		static void Main(string[] args)
		{
			List<User> allUsers = new List<User>();
			while (true)
			{
				string input = Console.ReadLine();
				if (input == "end of dates")
				{
					break;
				}
				string[] nameDates = input.Split(' ', ',');
				string name = nameDates[0];
				var textDates = nameDates.Skip(1).ToList();
				List<DateTime> dates = textDates
					.Select(d => DateTime
					.ParseExact(d, "dd/MM/yyyy", CultureInfo.InvariantCulture))
					.ToList();
				if (allUsers.Select(u => u.Name).Contains(name))
				{
					for (int i = 0; i < allUsers.Count; i++)
					{
						if (allUsers[i].Name == name)
						{
							allUsers[i].Dates.AddRange(dates);
						}
					}
					
				}
				else
				{
					User user = new User
					{
						Name = name,
						Dates = dates,
						Comments = new List<string>()
					};
					allUsers.Add(user);
				}
				
			}

			while (true)
			{
				var input = Console.ReadLine();
				if (input == "end of comments")
				{
					break;
				}
				var nameComment = input.Split('-');
				var name = nameComment[0];
				var comment = nameComment[1];
				foreach (var user in allUsers)
				{
					if (user.Name == name)
					{
						user.Comments.Add(comment);
					}
				}
			}

			allUsers = allUsers.OrderBy(u => u.Name).ToList();
			foreach (var user in allUsers)
			{
				Console.WriteLine(user.Name);
				Console.WriteLine("Comments:");
				foreach (var comment in user.Comments)
				{
					Console.WriteLine($"- {comment}");
				}
				Console.WriteLine("Dates attended:");
				user.Dates = user.Dates.OrderBy(x => x).ToList();
				foreach (var date in user.Dates)
				{
					Console.WriteLine($"-- {date:dd/MM/yyyy}");
				}
			}
		}
	}
}
