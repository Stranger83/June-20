using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntersectionOfCircles
{
	class Program
	{
		class Point
		{
			public int X { get; set; }
			public int Y { get; set; }
		}

		class Circle
		{
			public Point Center { get; set; }
			public double Radius { get; set; }

			public bool Intersect(Circle c)
			{
				int deltaX = Center.X - c.Center.X;
				int deltaY = Center.Y - c.Center.Y;
				double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
				if (distance <= Radius + c.Radius)
				{
					return true;
				}
				return false;
			}
		}
		static void Main(string[] args)
		{
			var input1 = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
			Circle circle1 = new Circle {
				Center = new Point {X = input1[0], Y = input1[1]},
				Radius = input1[2] };
			var input2 = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
			Circle circle2 = new Circle
			{
				Center = new Point { X = input2[0], Y = input2[1] },
				Radius = input2[2]
			};
			var intersect = circle1.Intersect(circle2);
			Console.WriteLine(intersect ? "Yes": "No");
		}
	}
}
