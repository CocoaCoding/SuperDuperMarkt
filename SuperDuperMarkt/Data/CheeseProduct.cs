using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperDuperMarkt.Data
{
    /// <summary>
    /// Produkt mit den Verarbeitungsregeln für Käse
    /// </summary>
    public class CheeseProduct : ProductBase
    {
        public CheeseProduct(string description, double priceBase, int qualityBase, DateTime expireDate) : base(description, priceBase, qualityBase, expireDate)
        {
        }

        public override ProductDaily CreateProductDaily(DateTime checkDate)
        {
            ProductDaily cheeseProductDaily = new ProductDaily();
            // Die Beschreibung und Type werden übernommen
            cheeseProductDaily.Description = this.Description;

            // Käse verliert täglich einen Qualitätspunkt
            // Ich vermute mal, nach dem Datum der Einsortierung
            TimeSpan timeSpan = checkDate - this.CreationDate;
            int qualityMalus = (int)timeSpan.TotalDays;
            if (qualityMalus < 0)
            {
                qualityMalus = 0;
            }
            cheeseProductDaily.QualityDaily = this.QualityBase - qualityMalus;

            // Zunächst ist alles ok, denn wird geprüft
            cheeseProductDaily.IsSortedOut = false;

            // Käse benötigt ein minimales Qualitätsniveau von 30
            if (cheeseProductDaily.QualityDaily < 30)
            {
                cheeseProductDaily.IsSortedOut = true;
                cheeseProductDaily.SortedOutReason = string.Format("Qualität des Käse zu gering. {0} < 30", cheeseProductDaily.QualityDaily);
            }

            // Käse hat ein Verfallsdatum, daszwischen 50 und 100 Tagen in der Zukunft liegen kann.
            // Wie viele Tage sind es noch bis zum Verfallsdatum?
            TimeSpan ts = this.ExpireDate.Date - checkDate;
            var daysTillExpire = ts.TotalDays;

            if (daysTillExpire < 50)
            {
                cheeseProductDaily.IsSortedOut = true;
                cheeseProductDaily.SortedOutReason = string.Format("Verfallsdatum des Käse zu gering. Nur noch {0} Tage", daysTillExpire);
            }
            else if (daysTillExpire > 100)
            {
                cheeseProductDaily.IsSortedOut = true;
                cheeseProductDaily.SortedOutReason = string.Format("Verfallsdatum des Käse zu hoch. {0} Tage", daysTillExpire);
            }

            // Der Tagespreis wird durch Qualität bestimmt:
            // Grundpreis + 0,1€ * Qualität
            cheeseProductDaily.PriceDaily = this.PriceBase + 0.1 * cheeseProductDaily.QualityDaily;

            return cheeseProductDaily;
        }

        public override string GetProductType()
        {
            return "Cheese";
        }
    }
}
