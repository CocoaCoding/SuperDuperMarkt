namespace SuperDuperMarkt.Data.ViewModel
{
    /// <summary>
    /// ViewModel zur Anzeige eines ProductDaily
    /// </summary>
    public class ProductDailyViewModel
    {
        /// <summary>
        /// Artikelname / Beschreibung
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Der Grundpreis
        /// </summary>
        public string PriceDaily { get; private set; }

        /// <summary>
        /// Die urspüngliche Qualität /vermutlich am Herstellungsdatum
        /// </summary>
        public string QualityDaily { get; private set; }

        /// <summary>
        /// Das Verfallsdatum
        /// </summary>
        public string CheckDate { get; private set; }

        /// <summary>
        /// Aussortieren?
        /// </summary>
        public string SortedOut { get; private set; }

        /// <summary>
        /// Warum aussortieren?
        /// </summary>
        public string SortedOutReason { get; private set; }

        public ProductDailyViewModel(ProductDaily productDaily)
        {
            this.Description = productDaily.Description;
           
            this.PriceDaily = productDaily.PriceDaily.ToString("N2") + " Euro";
            this.QualityDaily = productDaily.QualityDaily.ToString("N0");

            if (productDaily.IsSortedOut)
            {
                this.SortedOut = "Ja";
                this.SortedOutReason = productDaily.SortedOutReason;
            }
            else
            {
                this.SortedOut = "Nein";
            }
        }
    }
}
