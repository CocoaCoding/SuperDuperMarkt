using System;
using System.Collections.Generic;
using SuperDuperMarkt.Data;
using SuperDuperMarkt.Data.ViewModel;

namespace SuperDuperMarkt
{
    class ConsoleWriter
    {
        public void WriteWelcome()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Willkommen zum SuperDuperMarkt");
            Console.WriteLine("------------------------------");
            Console.WriteLine();
        }

        public void WriteProducts(List<ProductBase> products)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Produkte: \n");

            foreach (ProductBase product in products)
            {
                ProductViewModel productViewModel = new ProductViewModel(product);
                Console.WriteLine("Name          : " + productViewModel.Description);
                Console.WriteLine("Typ           : " + productViewModel.Type);
                Console.WriteLine("Qualität      : " + productViewModel.QualityBase);
                Console.WriteLine("Preis         : " + productViewModel.PriceBase);
                Console.WriteLine("Verfallsdatum : " + productViewModel.ExpireDate);
                Console.WriteLine();
            }
        }

        public void WriteProductsDailyHeadline(DateTime checkDate)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(string.Format("Produkte für : {0:dd.MM.yyyy} \n", checkDate));
        }

        public void WriteProductsDailyForDay(ProductDaily productDaily)
        {
            Console.ForegroundColor = ConsoleColor.White;

            ProductDailyViewModel productDailyViewModel = new ProductDailyViewModel(productDaily);
            Console.WriteLine("Name          : " + productDailyViewModel.Description);
            Console.WriteLine("Qualität      : " + productDailyViewModel.QualityDaily);
            Console.WriteLine("Preis         : " + productDailyViewModel.PriceDaily);

            Console.Write("Aussortieren  : ");
            if (productDaily.IsSortedOut == true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(productDailyViewModel.SortedOut + " - " + productDaily.SortedOutReason);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(productDailyViewModel.SortedOut);
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        public void WriteNoProducts()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Keine Produkte gefunden!\n");
        }

        public void WriteSeperator()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("------------------------------");
            Console.WriteLine();
        }

        public void WriteGoodbye()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Fertig! Drücken Sie ENTER um das Programm zu beenden.");
        }
    }
}
