using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGroups
{
	class Program
	{
		class Student
		{
			public string Name { get; set; }
			public string Email { get; set; }
			public DateTime RegistrationDate { get; set; }
		}
		class Town
		{
			public string Name { get; set; }
			public int SeatCount { get; set; }
			public List<Student> Students { get; set; }
		}
		class Groups
		{
			public Town Town { get; set; }
			public List<Student> Students { get; set; }
		}
		static void Main(string[] args)
		{
			
			var towns = ReadTownsAndStudents();
			var groups = DistributeStudentsIntoGroups(towns);
			PrintResult(groups, towns);
		}

		private static void PrintResult(List<Groups> groups, List<Town> towns)
		{
			var townNames = towns.Distinct().OrderBy(x => x.Name).ToList();
			Console.WriteLine($"Created {groups.Count} groups in {townNames.Count} towns:");
			foreach (var item in groups)
			{
				Console.Write($"{item.Town.Name}=> ");
				foreach (var student in item.Students)
				{
					Console.Write($"{student.Email}" + (student.Equals(item.Students.Last()) ? "" : ", "));
				}
				Console.WriteLine();
				
			}
		}

		private static List<Groups> DistributeStudentsIntoGroups(List<Town> towns)
		{
			var groups = new List<Groups>();
			towns = towns.OrderBy(x => x.Name).ToList();
			foreach (var town in towns)
			{
				town.Students = town.Students.OrderBy(x => x.RegistrationDate)
					.ThenBy(x => x.RegistrationDate)
					.ThenBy(x => x.Email)
					.ToList();
				while (town.Students.Any())
				{
					var group = new Groups();
					group.Town = town;
					group.Students = town.Students.Take(group.Town.SeatCount).ToList();
					town.Students = town.Students.Skip(group.Town.SeatCount).ToList();
					groups.Add(group);
				}
			}
			return groups;
		}

		private static List<Town> ReadTownsAndStudents()
		{
			var towns = new List<Town>();
			var input = Console.ReadLine();
			while (true)
			{
				if (input == "End")
				{
					break;
				}
				if (input.Contains("=>"))
				{
					var separator = "=>";
					var townNameSeats = input
						.Split(new[]{ separator }, StringSplitOptions.RemoveEmptyEntries)
						.ToArray();
					var name = townNameSeats[0];
					var seatCount = townNameSeats[1]
						.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
						.Take(1)
						.Select(x => int.Parse(x))
						.ToArray();
						
					Town town = new Town
					{
						Name = name,
						SeatCount = seatCount[0],
						Students = new List<Student>()
					};
					towns.Add(town);
				}
				else
				{
					var studentsPerTown = input
					.Split(new[] { '|', ' ' }, StringSplitOptions.RemoveEmptyEntries)
					.ToArray();
					Student student = new Student
					{
						Name = studentsPerTown[0] + " " + studentsPerTown[1],
						Email = studentsPerTown[2],
						RegistrationDate = DateTime.ParseExact(studentsPerTown[3], "d-MMM-yyyy", CultureInfo.InvariantCulture)
					};
					towns.Last().Students.Add(student);
				}
				input = Console.ReadLine();
			}
			return towns;
		}
	}
}
