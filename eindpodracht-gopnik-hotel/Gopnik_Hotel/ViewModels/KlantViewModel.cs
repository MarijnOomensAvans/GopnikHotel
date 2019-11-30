using Gopnik_Hotel.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gopnik_Hotel.ViewModels
{
    public class KlantViewModel
    {
        private Klant _klant;

        public KlantViewModel(Klant klant)
        {
            this._klant = klant;
        }

        public Klant ToModel()
        {
            return _klant;
        }

        public int KlantId { get { return _klant.KlantId; } set { _klant.KlantId = value; } }

        [DisplayName("Klantnaam")]
        [Required]
        public string Naam { get { return _klant.Naam; } set { _klant.Naam = value; } }

        [StringLength(255, MinimumLength = 1)]
        public string Mail { get { return _klant.Mail; } set { _klant.Mail = value; } }

        [Required]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"/^[1 - 9][0 - 9]{3}[\s]?[A - Za - z]{2}$/i")]
        public string Postcode { get { return _klant.Postcode; } set { _klant.Postcode = value; } }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Woonplaats { get { return _klant.Woonplaats; } set { _klant.Woonplaats = value; } }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Straat { get { return _klant.Straat; } set { _klant.Straat = value; } }

        [Required]
        [Range(1,9999)]
        public int Huisnummer { get { return _klant.Huisnummer; } set { _klant.Huisnummer = value; } }

        [StringLength(255, MinimumLength = 1)]
        public string Toevoeging { get { return _klant.Toevoeging; } set { _klant.Toevoeging = value; } }
    }
}