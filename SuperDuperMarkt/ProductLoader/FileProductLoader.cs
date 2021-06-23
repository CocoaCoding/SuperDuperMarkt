using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using SuperDuperMarkt.Data;

namespace SuperDuperMarkt.ProductLoader
{
    /// <summary>
    /// Klasse, die Produkte als CSV läd und speichert
    /// </summary>
    public class FileProductLoader : IProductLoaderInterface
    {
        /// <summary>
        /// Dateiname für CSV
        /// </summary>
        private const string FILENAME = "Products.csv";

        /// <summary>
        /// Anzahl der Eigenschaften der Klasse.
        /// Wird zur überprüfung bei der Genererung eines Produktes aus CSV verwendet
        /// </summary>
        private const int PROPERTIES_COUNT = 5;

        /// <summary>
        /// Produkte laden oder Default-Produkte verwenden wenn keine Datei gefunden wurde
        /// </summary>
        /// <returns></returns>
        public List<ProductBase> LoadProducts()
        {

            List<ProductBase> products = new List<ProductBase>();

            // Existiert eine Datei mit gespeicherten Produkten?
            if (File.Exists(FILENAME))
            {
                using (StreamReader reader = new StreamReader(FILENAME))
                {
                    string csvLine;
                    while ((csvLine = reader.ReadLine()) != null)
                    {
                        // Produkt erstellten. Ist null im Fehlerfall.
                        ProductBase product = this.CreateProductFromCsvLine(csvLine);
                        if (product != null)
                        {
                            products.Add(product);
                        }
                    }
                }
            }

            // Gibt es jetzt Produkte? Sonst Default verwenden.
            if (products.Count == 0)
            {
                products.AddRange(this.DefaultProducts());
            }

            return products;
        }

        /// <summary>
        /// Speichert Produkte als CSV
        /// </summary>
        /// <param name="products"></param>
        public void SaveProducts(List<ProductBase> products)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(FILENAME))
                {
                    foreach (ProductBase product in products)
                    {
                        string csvLine = this.GetCsvLine(product);
                        sw.WriteLine(csvLine);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        /// <summary>
        /// Erzeugt eine Liste von Beispieldaten
        /// </summary>
        /// <returns></returns>
        public List<ProductBase> DefaultProducts()
        {
            List<ProductBase> products = new List<ProductBase>();

            BreadProduct bread = new BreadProduct("Toastbrot", 2.99, 25,DateTime.Now.AddDays(10));
            products.Add(bread);
            
            WineProduct wine = new WineProduct("Palhuber (rot)", 12.99, 20, DateTime.Now.AddDays(0));
            products.Add(wine);

            wine = new WineProduct("Dornfelder (weiss)", 13.49, -5, DateTime.Now.AddDays(50));
            products.Add(wine);

            CheeseProduct cheese = new CheeseProduct("Pfefferkäse", 1.99, 40, DateTime.Now.AddDays(70));
            products.Add(cheese);

            cheese = new CheeseProduct("Butterkäse", 2.49, 55, DateTime.Now.AddDays(100));
            products.Add(cheese);
            
            return products;
        }

        /// <summary>
        /// Erzeugt CSV-Daten für das Produkt
        /// </summary>
        /// <returns></returns>
        private string GetCsvLine(ProductBase product)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(product.GetProductType());
            builder.Append(",");
            builder.Append(product.Description);
            builder.Append(",");
            builder.Append(product.PriceBase.ToString("F2", CultureInfo.InvariantCulture));
            builder.Append(",");
            builder.Append(product.QualityBase.ToString("N0"));
            builder.Append(",");
            builder.Append(string.Format("{0:dd.MM.yyyy}", product.ExpireDate));
            return builder.ToString();
        }

        /// <summary>
        /// Erzeugt ein Produkt aus einer Zeile CSV-Daten
        /// </summary>
        /// <param name="csvLine"></param>
        /// <returns></returns>
        private ProductBase CreateProductFromCsvLine(string csvLine)
        {
            try
            {
                string[] properties = csvLine.Split(',');

                // Die richtige Anzahl von Eigenschaften vorhanden?
                if (properties.Count() == PROPERTIES_COUNT)
                {
                    string type = properties[0];
                    string description = properties[1];

                    double priceBase;
                    bool checkPrice = double.TryParse(properties[2], NumberStyles.Any, CultureInfo.InvariantCulture, out priceBase);

                    int qualityBase;
                    bool checkQuality = int.TryParse(properties[3], out qualityBase);

                    DateTime expireDate;
                    bool checkDate = DateTime.TryParse(properties[4], out expireDate);

                    // Wenn alle Umwandlungen funktioniert haben 
                    // kann man ein Produkt erstellten.
                    if (checkPrice && checkQuality && checkDate)
                    {
                        if (type == "Wine")
                        {
                            return new WineProduct(description, priceBase, qualityBase, expireDate);
                        }
                        else if (type == "Cheese")
                        {
                            return new CheeseProduct(description, priceBase, qualityBase, expireDate);
                        }
                        else if (type == "Bread")
                        {
                            return new BreadProduct(description, priceBase, qualityBase, expireDate);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return null;
        }
    }
}
