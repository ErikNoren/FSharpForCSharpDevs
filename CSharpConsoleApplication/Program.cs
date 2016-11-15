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

            //#region CSV Demo

            //var csvRepo = new CsvRepository();
            //var headers = csvRepo.Headers;
            //foreach (var head in headers)
            //{
            //    Console.Write("{0}\t", head);
            //}
            //Console.WriteLine();

            //foreach (var row in csvRepo.FinanceData)
            //{
            //    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t", row.Date, row.Open, row.High, row.Low, row.Close, row.Volume, row.AdjClose);
            //}

            //Console.WriteLine();

            //foreach (var head in headers)
            //{
            //    Console.Write("{0}\t", head);
            //}
            //Console.WriteLine();

            //foreach (var row in csvRepo.RawData)
            //{
            //    foreach (var item in row)
            //    {
            //        Console.Write("{0}\t", item);
            //    }

            //    Console.WriteLine();
            //}

            //#endregion

            //#region LocalDB Demo

            //var dbRepo = new LocalDataRepository();
            //var t = dbRepo.Employees;

            //#endregion

            //#region Northwind OData Demo



            //#endregion

            #region WorldBankData Demo

            var wbdRepo = new WorldBankRepository();

            var knownCountries = wbdRepo.Countries.OrderBy(c => c);
            var usIndicators = wbdRepo.GetIndicatorsForCountry("United States");
            var indicator = usIndicators.First();

            foreach (var i in indicator.GroupBy(i => i.Source))
            {
                Console.WriteLine(i.Key);
                foreach (var v in i)
                {
                    Console.WriteLine("{0}:", v.Source);
                    foreach(var f in v.Years.OrderBy(y => y))
                    {
                        Console.WriteLine("{0}: {1}", f, v[f]);
                    }
                }
            }

            #endregion

            #region Local XML File Demo



            #endregion

        }
    }
}
