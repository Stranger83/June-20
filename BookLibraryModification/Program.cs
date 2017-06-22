using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary
{
	class Program
	{
		class Book
		{
			public string Title { get; set; }
			public string Author { get; set; }
			public string Publisher { get; set; }
			public DateTime ReleaseDate { get; set; }
			public string ISBN { get; set; }
			public double Price { get; set; }
		}
		class Library
		{
			public string Name { get; set; }
			public List<Book> Books { get; set; }
		}

		static void Main(string[] args)
		{
			int n = int.Parse(Console.ReadLine());
			List<Book> books = new List<Book>();
			for (int i = 0; i < n; i++)
			{
				var input = Console.ReadLine().Split(' ').ToArray();
				Book book = new Book
				{
					Title = input[0],
					Author = input[1],
					Publisher = input[2],
					ReleaseDate = DateTime.ParseExact(input[3], "dd.MM.yyyy", CultureInfo.InvariantCulture),
					ISBN = input[4],
					Price = double.Parse(input[5])
				};
				books.Add(book);
			}
			Library myBooks = new Library { Name = "My books", Books = books };
			DateTime controlDate = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
			
			Dictionary<string, DateTime> titleDate = new Dictionary<string, DateTime>();
			foreach (var book in books)
			{
				if (!titleDate.ContainsKey(book.Title))
				{
					titleDate[book.Title] = new DateTime(1,1,1);
				}
				titleDate[book.Title] = book.ReleaseDate;
			}

			var sorted = titleDate.Where(x => x.Value > controlDate).OrderBy(x => x.Value).ThenBy(x => x.Key);

			foreach (var book in sorted)
			{
				Console.WriteLine($"{book.Key} -> {book.Value:dd.MM.yyyy}");
			}
		}
	}
}
