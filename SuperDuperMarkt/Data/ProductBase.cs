using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperDuperMarkt.Data
{
    public abstract class ProductBase
    {
        /// <summary>
        /// Artikelname / Beschreibung
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Der Grundpreis
        /// </summary>
        public double PriceBase { get; private set; }

        /// <summary>
        /// Die urspüngliche Qualität /vermutlich am Herstellungsdatum
        /// </summary>
        public int QualityBase { get; private set; }

        /// <summary>
        /// Das Verfallsdatum
        /// </summary>
        public DateTime ExpireDate { get; private set; }

        /// <summary>
        ///  Wann wurde der Artikel hergestellt / bzw. in das Sortiment aufgenommen
        /// </summary>
        public DateTime CreationDate { get; private set; }

        protected ProductBase(string description, double priceBase, int qualityBase,  DateTime expireDate)
        {
            this.Description = description;
            this.PriceBase = priceBase;
            this.QualityBase = qualityBase;
            this.ExpireDate = expireDate;
            this.CreationDate = DateTime.Now;
        }

        /// <summary>
        /// Hier müssen die Verarbeitungsregeln hinterlegt werden
        /// </summary>
        /// <param name="checkDate"></param>
        /// <returns></returns>
        public abstract ProductDaily CreateProductDaily(DateTime checkDate);

        /// <summary>
        /// Wird verwendet um die Produkte zu unterscheiden.
        /// Wird beim Speichern des CSV Verwendet.
        /// </summary>
        /// <returns></returns>
        public abstract string GetProductType();
    }
}
