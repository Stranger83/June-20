using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountWorkingDays
{
	class Program
	{
		static void Main(string[] args)
		{
			DateTime startDate = DateTime.ParseExact(Console.ReadLine(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
			DateTime endDate = DateTime.ParseExact(Console.ReadLine(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
			var workingDaysCount = 0;
			DateTime[] holidays = new DateTime[]{
			new DateTime(1, 12, 25),
			new DateTime(1, 1, 1),
			new DateTime(1, 3, 3),
			new DateTime(1, 5, 1),
			new DateTime(1, 5, 6),
			new DateTime(1, 5, 24),
			new DateTime(1, 9, 6),
			new DateTime(1, 9, 22),
			new DateTime(1, 11, 1),
			new DateTime(1, 12, 24),
			new DateTime(1, 12, 25),
			new DateTime(1, 12, 26)
			};
			for (DateTime currentDate = startDate; currentDate <= endDate; currentDate = currentDate.AddDays(1))
			{
				var isWeekend = currentDate.DayOfWeek == DayOfWeek.Saturday ||
					currentDate.DayOfWeek == DayOfWeek.Sunday;
				var isHoliday = holidays.Any(d => d.Day == currentDate.Day && d.Month == currentDate.Month);
				if (!isWeekend && !isHoliday)
				{
					workingDaysCount++;
				}
			}
			Console.WriteLine(workingDaysCount);
		}
	}
}
