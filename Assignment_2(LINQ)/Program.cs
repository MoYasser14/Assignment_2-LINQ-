using LINQ.DataSources;

namespace Assignment_2_LINQ
{
    internal class Program
    {
        static void Main()
        {
            // Q1:
            var q1 = Source.ProductList.OrderByDescending(p => p.UnitPrice).Take(3);
            foreach (var p in q1)
            {
                Console.WriteLine($"Name: {p.ProductName}, Price: {p.UnitPrice}");
            }

            // Q2:
            var q2 = Source.ProductList.Skip(5).Take(5);
            foreach (var p in q2)
            {
                Console.WriteLine($"Name: {p.ProductName}");
            }

            // Q3:
            var q3 = Source.ProductList.OrderBy(p => p.UnitPrice).TakeWhile(p => p.UnitPrice < 25);
            foreach (var p in q3)
            {
                Console.WriteLine($"Name: {p.ProductName}, Price: {p.UnitPrice}");
            }

            // Q4:
            var q4 = Source.ProductList.Where(p => p.Category == "Seafood").All(p => p.UnitsInStock > 0);
            Console.WriteLine(q4);

            // Q5:
            int[] ids = { 3, 9, 13, 18 };
            var q5 = ids.All(id => Source.ProductList.Any(p => p.ProductID == id));
            Console.WriteLine(q5);

            // Q6:
            var q6 = Source.ProductList.GroupBy(p => p.Category);
            foreach (var group in q6)
            {
                Console.WriteLine($"Category: {group.Key}, Count: {group.Count()}");
            }

            // Q7:
            var q7 = Source.ProductList.GroupBy(p => p.Category);
            foreach (var group in q7)
            {
                Console.WriteLine($"Category: {group.Key}");
                foreach (var name in group.Select(p => p.ProductName))
                {
                    Console.WriteLine($"  - {name}");
                }
            }

            // Q8:
            var q8 = Source.ProductList.GroupBy(p => p.Category).Where(g => g.Count() > 3);
            foreach (var group in q8)
            {
                Console.WriteLine($"Category: {group.Key}, Count: {group.Count()}");
            }

            // Q9:
            var q9 = from customer in Source.CustomerList
                     group customer by customer.Country into g
                     select new
                     {
                         Country = g.Key,
                         Count = g.Count(),
                         TotalOrderValue = g.SelectMany(c => c.Orders).Sum(o => o.Total)
                     };
            foreach (var item in q9)
            {
                Console.WriteLine($"Country: {item.Country}, Count: {item.Count}, Total: {item.TotalOrderValue}");
            }

            // Q10:
            var q10 = Source.ProductList.Sum(p => p.UnitsInStock);
            Console.WriteLine(q10);

            // Q11:
            var q11Min = Source.ProductList.Min(p => p.UnitPrice);
            var q11Max = Source.ProductList.Max(p => p.UnitPrice);
            Console.WriteLine($"Cheapest: {q11Min}, Most Expensive: {q11Max}");

            // Q12:
            var q12 = Source.ProductList.Select(p => p.Category).Distinct();
            foreach (var cat in q12)
            {
                Console.WriteLine(cat);
            }

            // Q13:
            int[] setA = { 1, 3, 5, 7, 9, 11, 13 };
            int[] setB = { 3, 6, 9, 12, 15, 13 };
            var q13 = setA.Except(setB);
            foreach (var id in q13)
            {
                Console.WriteLine(id);
            }

            // Q14:
            string[] list1 = { "Germany", "France", "UK", "Spain" };
            string[] list2 = { "france", "SPAIN", "Italy" };
            var q14 = list1.Except(list2, StringComparer.OrdinalIgnoreCase);
            foreach (var country in q14)
            {
                Console.WriteLine(country);
            }

            // Q15:
            var q15 = Source.ProductList.DistinctBy(p => p.ProductID).ToDictionary(p => p.ProductID);
            var targetProduct = q15[18];
            Console.WriteLine($"Name: {targetProduct.ProductName}");

            // Q16:
            var q16 = Source.ProductList.First(p => p.UnitPrice > 50);
            Console.WriteLine($"Name: {q16.ProductName}, Price: {q16.UnitPrice}");

            // Q17:
            var q17 = Source.ProductList.FirstOrDefault(p => p.UnitPrice > 500);
            Console.WriteLine(q17 == null ? "null" : q17.ProductName);

            // Q18:
            var q18 = Enumerable.Range(1, 12).Select(i => i * 7);
            foreach (var val in q18)
            {
                Console.WriteLine(val);
            }

            // Q19:
            var q19 = Enumerable.Range(1, 30).Where(x => x % 2 == 0);
            foreach (var val in q19)
            {
                Console.WriteLine(val);
            }

            // Q20:
            var q20 = Source.ProductList.Select(p => p.ProductName).Take(3)
                      .Concat(Source.CustomerList.Select(c => c.CustomerName).Take(3));
            foreach (var item in q20)
            {
                Console.WriteLine(item);
            }

            // Q21:
            var q21 = Source.ProductList.Select(p => p.ProductName)
                      .Zip(Source.CustomerList.Select(c => c.CustomerName), (prod, cust) => $"{prod} sold to {cust}");
            foreach (var item in q21)
            {
                Console.WriteLine(item);
            }
        }
    }
}