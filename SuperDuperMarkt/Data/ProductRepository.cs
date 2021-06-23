using System.Collections.Generic;
using SuperDuperMarkt.ProductLoader;

namespace SuperDuperMarkt.Data
{
    /// <summary>
    /// Datenhaltung und Verwaltung der Produkte
    /// </summary>
    class ProductRepository
    {
        /// <summary>
        /// Verweis auf eine Klasse, welche die Produkte tatsächlich läd und speichert.
        /// Interface!
        /// </summary>
        private readonly IProductLoaderInterface productLoader;
        
        /// <summary>
        /// Liste alle Produkte
        /// </summary>
        private readonly List<ProductBase> products;
        
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="productLoader">Beliebige Klasse die IProductLoaderInterface implementiert, zum Laden und Speichern</param>
        public ProductRepository(IProductLoaderInterface productLoader)
        {
            this.productLoader = productLoader;
            this.products = new List<ProductBase>();
        }

        /// <summary>
        /// Produkte laden
        /// </summary>
        public void LoadProducts()
        {
            this.products.Clear();
            this.products.AddRange(this.productLoader.LoadProducts());
        }

        /// <summary>
        /// Produkte speichern
        /// </summary>
        public void SaveProducts()
        {
            this.productLoader.SaveProducts(this.products);
        }

        /// <summary>
        /// Liefert eine Liste sämtlicher Produkte 
        /// </summary>
        /// <returns>Alle Produkte</returns>
        public List<ProductBase> GetAllProducts()
        {
            return this.products;
        }
    }
}
