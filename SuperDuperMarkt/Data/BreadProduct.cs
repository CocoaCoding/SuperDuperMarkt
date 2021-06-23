using System;

namespace SuperDuperMarkt.Data
{
    public class BreadProduct : ProductBase
    {
        /// <summary>
        /// Produkt mit den Verarbeitungsregeln für Brot
        /// </summary>
        public BreadProduct(string description, double priceBase, int qualityBase, DateTime expireDate) : base(description, priceBase, qualityBase, expireDate)
        {
        }

        public override ProductDaily CreateProductDaily(DateTime checkDate)
        {
            // Mit Brot passiert nicht viel.
            // Es wird aussortiert, wenn das Verfallsdatum ereicht wird.

            ProductDaily productDaily = new ProductDaily();
            productDaily.Description = this.Description;
            productDaily.PriceDaily = this.PriceBase;
            productDaily.QualityDaily = this.QualityBase;

            if (this.ExpireDate.Date < checkDate)
            {
                productDaily.IsSortedOut = true;
                productDaily.SortedOutReason = "Verfallsdatum erreicht";
            }
            else
            {
                productDaily.IsSortedOut = false;
            }

            return productDaily;
        }

        public override string GetProductType()
        {
            return "Bread";
        }
    }
}
