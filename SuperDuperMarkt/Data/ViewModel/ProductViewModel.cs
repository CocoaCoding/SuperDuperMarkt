namespace SuperDuperMarkt.Data.ViewModel
{
    /// <summary>
    /// ViewModel zur Anzeige eines Product
    /// </summary>
    public class ProductViewModel
    {
        /// <summary>
        /// Artikelname / Beschreibung
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Der Grundpreis
        /// </summary>
        public string PriceBase { get; private set; }

        /// <summary>
        /// Die urspüngliche Qualität /vermutlich am Herstellungsdatum
        /// </summary>
        public string QualityBase { get; private set; }

        /// <summary>
        /// Der Typ : Käse, Wein, etc
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Das Verfallsdatum
        /// </summary>
        public string ExpireDate { get; private set; }

        public ProductViewModel(ProductBase product)
        {
            this.Description = product.Description;
            this.PriceBase = product.PriceBase.ToString("N2") + " Euro";
            this.QualityBase = product.QualityBase.ToString("N0");
            this.ExpireDate = string.Format("{0:dd.MM.yyyy}", product.ExpireDate);
        }
    }
}
