using Gopnik_Hotel.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gopnik_Hotel.ViewModels
{
    public class KamerViewModel
    {
        private Kamer _kamer;

        public KamerViewModel(Kamer kamer)
        {
            this._kamer = kamer;
        }

        public Kamer ToModel()
        {
            return this._kamer;
        }

        public int KamerId { get { return _kamer.KamerId; } set { _kamer.KamerId = value; } }

        [DisplayName("Kamernaam")]
        [StringLength(255, MinimumLength = 1)]
        [Required]
        public string Naam { get { return _kamer.Naam; } set { _kamer.Naam = value; } }

        [Range(1,5)]
        [MinLength(1)]
        [Required]
        public int Grootte { get { return _kamer.Grootte; } set { _kamer.Grootte = value; } }

        [Range(0,1000000000)]
        [DataType(DataType.Currency)]
        [MinLength(1)]
        [Required]
        public decimal Prijs { get { return _kamer.Prijs; } set { _kamer.Prijs = value; } }

        [StringLength(255, MinimumLength = 1)]
        [Required]
        public string Afbeelding { get { return _kamer.Afbeelding; } set { _kamer.Afbeelding = value; } }

        public List<BoekingViewModel> Boekingen {
            get {
                if(_kamer.Boekings.Count != 0)
                {
                    List<BoekingViewModel> tempList = new List<BoekingViewModel>();
                    foreach (var b in _kamer.Boekings)
                    {
                        tempList.Add(new BoekingViewModel(b));
                    }
                    return tempList;
                }
                return null;
            }
            set {
                Boekingen = value;
            } }

    }
}