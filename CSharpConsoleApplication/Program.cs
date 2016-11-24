using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FSharpLibrary;

namespace CSharpConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            #region CSV Demo

            Console.WriteLine("\nCSV Demo");
            Console.WriteLine("--------------------------------------------------");

            var csvRepo = new CsvRepository();

            Console.WriteLine("Finance Data with record");
            var headers = csvRepo.Headers;
            foreach (var head in headers)
            {
                Console.Write("{0}\t", head);
            }
            Console.WriteLine();

            foreach (var row in csvRepo.FinanceData.Take(10))
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t", row.Date, row.Open, row.High, row.Low, row.Close, row.Volume, row.AdjClose);
            }

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Finance Data using raw columns");
            foreach (var head in headers)
            {
                Console.Write("{0}\t", head);
            }
            Console.WriteLine();

            foreach (var row in csvRepo.RawData.Take(10))
            {
                foreach (var item in row)
                {
                    Console.Write("{0}\t", item);
                }

                Console.WriteLine();
            }

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();

            #endregion

            #region LocalDB Demo

            Console.WriteLine("\nLocalDB Demo");
            Console.WriteLine("--------------------------------------------------");

            var dbRepo = new LocalDataRepository();
            var t = dbRepo.Employees;

            foreach (var e in t.Skip(5).Take(10).OrderBy(e => e.EmployeeId))
            {
                Console.WriteLine($"{e.EmployeeId}: {e.Surname}, {e.FirstName}");
            }

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();

            #endregion

            #region Northwind OData Demo

            Console.WriteLine("\nNorthwind OData Demo");
            Console.WriteLine("--------------------------------------------------");

            var oCtx = new NorthwindRepository();
            var products = oCtx.Products.ToArray();

            Console.WriteLine($"There are {products.Length} products in the catalog. Here are 10 in no particular order.");
            foreach (var prod in products.Take(10))
            {
                Console.WriteLine($"({prod.ProductID}) {prod.ProductName} | qty. {prod.UnitsInStock} @ ${prod.UnitPrice:#.##}");
            }

            Console.WriteLine();

            var ordersForVICTE = oCtx.GetOrdersForCustomerById("VICTE").ToArray();
            var orderCount = ordersForVICTE.GroupBy(o => o.OrderID).Count();
            var avgDiscount = ordersForVICTE.Average(o => o.Discount);
            var maxDiscount = ordersForVICTE.Max(o => o.Discount);

            Console.WriteLine($"VICTE averaged an item discount of {avgDiscount*100}% over {orderCount} orders with a max discount of {maxDiscount*100}%");
            foreach (var order in ordersForVICTE.GroupBy(o => o.OrderID))
            {
                Console.WriteLine($"Order {order.Key}");
                foreach (var item in order)
                {
                    Console.WriteLine($"\tItem: {item.ProductID} Discount: {item.Discount * 100}%");
                }
            }
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();

            #endregion

            #region WorldBankData Demo

            Console.WriteLine("\nWorld Bank Demo");
            Console.WriteLine("--------------------------------------------------");

            var wbdRepo = new WorldBankRepository();
            
            var countriesWithCapitals = wbdRepo.CountriesAndCapitals;
            foreach(var cc in countriesWithCapitals.OrderBy(c => c.Capital).Skip(100).Take(10))
            {
                Console.WriteLine($"{(string.IsNullOrWhiteSpace(cc.Capital) ? "[N/A]" : cc.Capital)}, {cc.Name}");
            }

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();

            #endregion

            #region Local XML File Demo

            Console.WriteLine("\nXML File Demo");
            Console.WriteLine("--------------------------------------------------");

            var xml = new XmlDataRepository();
            var authors = xml.Authors.OrderBy(a => a).ToArray();

            var count = authors.Length;

            Console.WriteLine($"There are {count} authors in the list.");
            foreach(var aut in authors)
            {
                Console.WriteLine(aut);
            }

            Console.WriteLine("\nHave you read any good books lately? How about one of these:");
            foreach(var book in xml.BookTitles.OrderBy(b => b))
            {
                Console.WriteLine(book);
            }

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();

            #endregion


            Console.WriteLine("\nHit [enter] to close.");
            Console.ReadLine();
        }
    }
}
