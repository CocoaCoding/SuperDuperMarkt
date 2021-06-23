using System.Collections.Generic;
using SuperDuperMarkt.Data;

namespace SuperDuperMarkt.ProductLoader
{
    /// <summary>
    /// Interface für eine Klasse die Produkte läd und speichert
    /// </summary>
    interface IProductLoaderInterface
    {
        List<ProductBase> LoadProducts();
        void SaveProducts(List<ProductBase> products);
        List<ProductBase> DefaultProducts();
    }
}
