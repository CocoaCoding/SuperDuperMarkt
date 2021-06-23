using System;

namespace SuperDuperMarkt.Data
{
    /// <summary>
    /// Produkt mit Angaben für einen speziellen Tag mit Angaben
    /// von tagesaktuellem Preis, Qualität und Angaben, ob das Produkt
    /// aussortiert werden soll. 
    /// </summary>
    public class ProductDaily
    {
        /// <summary>
        /// Artikelname / Beschreibung
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Der tagesaktuelle Preis
        /// </summary>
        public double PriceDaily { get; set; }

        /// <summary>
        /// Die tagesaktuelle Qualität
        /// </summary>
        public int QualityDaily { get; set; }

        // Verfallen / Ausgelaufen
        public bool IsSortedOut { get; set; }

        public string SortedOutReason { get; set; }

    }
}
