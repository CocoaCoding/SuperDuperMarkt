using System;

namespace SuperDuperMarkt.Data
{
    public class WineProduct : ProductBase
    {
        /// <summary>
        /// Produkt mit den Verarbeitungsregeln für Wein
        /// </summary>
        public WineProduct(string description, double priceBase, int qualityBase, DateTime expireDate) : base(description, priceBase, qualityBase, expireDate)
        {
        }

        public override ProductDaily CreateProductDaily(DateTime checkDate)
        {
            ProductDaily productDaily = new ProductDaily();
            // Die Beschreibung und Type werden übernommen
            productDaily.Description = this.Description;
          
            // Weine verändern ihren Preis nicht, nachdem sie einmal ins Regal gestellt wurden
            productDaily.PriceDaily = this.PriceBase;

            // Wein verfällt nicht aber die Qualität kann generell zu schlecht sein.
            // Für Wein wird jedes nicht negative Qualitätsniveau akzeptiert.
            if (this.QualityBase < 0)
            {
                productDaily.IsSortedOut = true;
                productDaily.SortedOutReason = "Qualität des Weines zu gering.";
            }
            else
            {
                productDaily.IsSortedOut = false;
            }

            // Weine die von Anfang an eine negative Qualität haben bekommen keinen
            // Qualitätsbonus sonst würde ein schlechter Wein durch langes Stehen wieder gut!

            // Wein verliert nicht an Qualität, sondern gewinnt ab dem Stichtag ...
            if (this.ExpireDate.Date <= checkDate && !productDaily.IsSortedOut)
            {
                //  ... alle 10 Tage + 1 Qualität hinzu ...
                TimeSpan timeSpan = checkDate - this.ExpireDate;
                double days = timeSpan.TotalDays;
                int qualityBonus = (int)days / 10;
                int qualityDaily = this.QualityBase + qualityBonus;

                // ... bis die Qualität 50 erreicht hat.
                if (qualityDaily > 50)
                {
                    qualityDaily = 50;
                }

                productDaily.QualityDaily = qualityDaily;
            }
            else
            {
                productDaily.QualityDaily = this.QualityBase;
            }

            return productDaily;
        }

        public override string GetProductType()
        {
            return "Wine";
        }
    }
}
