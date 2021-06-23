using System;
using System.Collections.Generic;
using SuperDuperMarkt.Data;
using SuperDuperMarkt.ProductLoader;

namespace SuperDuperMarkt
{
    public class SuperDuperMarktProgram
    {
        private SuperDuperMarktProgram()
        { }

        private static SuperDuperMarktProgram _instance;

        /// <summary>
        /// Instanz für Singleton-Pattern
        /// </summary>
        /// <returns></returns>
        public static SuperDuperMarktProgram GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SuperDuperMarktProgram();
            }
            return _instance;
        }
        
        /// <summary>
        /// Hier geht es los!
        /// </summary>
        public void Run()
        {
            // Ein ProductLoader läd die Produkte. In diesem Fall aus einer Datei.
            FileProductLoader fileProductLoader = new FileProductLoader();

            // Das Repository ist die zentrale Datenhaltung für alle Produkte.
            ProductRepository productRepository = new ProductRepository(fileProductLoader);
            productRepository.LoadProducts();

            ConsoleWriter writer = new ConsoleWriter();
            writer.WriteWelcome();

            List<ProductBase> products = productRepository.GetAllProducts();

            if (products.Count == 0)
            {
                writer.WriteNoProducts();
            }
            else
            {
                ShowProductBase(products, writer);

                // Die Zeitspanne für die Verarbeitung
                DateTime startdate = DateTime.Now.AddDays(1).Date;
                DateTime enddate = startdate.AddDays(30).Date;

                CreateProductDaily(products, writer, startdate, enddate);
            }

            writer.WriteGoodbye();
            Console.ReadLine();
        }

        private void ShowProductBase(List<ProductBase> products, ConsoleWriter writer)
        {
            // Die Produkte mit ihren Grundeigenschaften anzeigen
            writer.WriteProducts(products);

            // Trennlinie
            writer.WriteSeperator();
        }
        
        /// <summary>
        /// Hier werden die Produkte mit den Verarbeitungsregeln verarbeitet
        /// </summary>
        /// <param name="products"></param>
        /// <param name="writer"></param>
        private void CreateProductDaily(List<ProductBase> products, ConsoleWriter writer, DateTime startdate, DateTime enddate)
        {
            // CreateProductDaily erzeugen aus dem Product ein ProductDaily
            // Dort wird die aktuelle Qualität und der Tagespreis ermittelt .
            // Außerdem wird ermittel wann und warum ein Produkt aussortiert werden muss. 

            for (var checkdate = startdate.Date; checkdate.Date <= enddate.Date; checkdate = checkdate.AddDays(1))
            {
                writer.WriteProductsDailyHeadline(checkdate);

                foreach (ProductBase product in products)
                {
                    ProductDaily productDaily = product.CreateProductDaily(checkdate);
                    writer.WriteProductsDailyForDay(productDaily);                    
                }
            }
        }
    }
}
