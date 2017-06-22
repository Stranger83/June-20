using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreyAndBilliard
{
	class Program
	{
		class Customer
		{
			public string Name { get; set; }
			public Dictionary<string, int> BoughtItems { get; set; }
			public decimal Bill { get; set; }
		}
		static void Main(string[] args)
		{
			int n = int.Parse(Console.ReadLine());
			Dictionary<string, decimal> products = new Dictionary<string, decimal>();
			for (int i = 0; i < n; i++)
			{
				var input = Console.ReadLine().Split('-').ToArray();
				if (!products.ContainsKey(input[0]))
				{
					products[input[0]] = 0;
				}
				products[input[0]] = decimal.Parse(input[1]);
			}
			List<Customer> customers = new List<Customer>();
			while (true)
			{
				var input = Console.ReadLine();
				if (input == "end of clients")
				{
					break;
				}
				string[] tokens = input.Split('-').ToArray();
				string[] buyTokens = tokens[1].Split(',');
				Customer cust;
				if (customers.Select(x => x.Name).Contains(tokens[0]))
				{
					cust = customers.Find(x => x.Name == tokens[0]);
					if (products.ContainsKey(buyTokens[0]))
					{
						if (!cust.BoughtItems.ContainsKey(buyTokens[0]))
						{
							cust.BoughtItems[buyTokens[0]] = 0;
						}
						cust.BoughtItems[buyTokens[0]] += int.Parse(buyTokens[1]);
					}
				}
				else
				{
					cust = new Customer
					{
						Name = tokens[0],
						BoughtItems = new Dictionary<string, int>()
					};
					if (!products.ContainsKey(buyTokens[0]))
					{
						continue;
					}
					if (!cust.BoughtItems.ContainsKey(buyTokens[0]))
					{
						cust.BoughtItems[buyTokens[0]] = 0;
					}
					cust.BoughtItems[buyTokens[0]] += int.Parse(buyTokens[1]);
					
					customers.Add(cust);
				}
				cust.Bill = 0;
				foreach (var kvp in cust.BoughtItems)
					{
						cust.Bill += kvp.Value * products[kvp.Key];
					};
	
			}
			var ordered = customers.OrderBy(x => x.Name);

			decimal totalBill = 0;
			foreach (var customer in ordered)
			{
				Console.WriteLine(customer.Name);
				foreach (var kvp in customer.BoughtItems)
				{
					Console.WriteLine($"-- {kvp.Key} - {kvp.Value}");
				}
				Console.WriteLine($"Bill: {customer.Bill:f2}");
				totalBill += customer.Bill;
			}
			
			Console.WriteLine($"Total bill: {totalBill:f2}");
		}
	}
}
