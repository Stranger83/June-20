using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AverageGrades
{
	class Program
	{
		class Student
		{
			public string Name { get; set; }
			public List<double> Grades { get; set; }
			public double AverageGrade { get { return Grades.Average(); } }
		}
		static void Main(string[] args)
		{
			int n = int.Parse(Console.ReadLine());
			Student[] students = new Student[n];
			for (int i = 0; i < n; i++)
			{
				var input = Console.ReadLine().Split().ToList();
				Student student = new Student
				{
					Name = input[0],
				};
				input.RemoveAt(0);
				student.Grades = input.Select(double.Parse).ToList();
				students[i] = student;
			}
			foreach (var s in students.OrderBy(s => s.Name).ThenByDescending(s => s.AverageGrade))
			{
				if (s.AverageGrade >= 5.00)
				{
					Console.WriteLine($"{s.Name} -> {s.AverageGrade:f2}");
				}
			}
		}
	}
}
