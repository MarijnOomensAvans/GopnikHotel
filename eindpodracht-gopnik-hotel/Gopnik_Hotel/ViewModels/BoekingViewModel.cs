using Gopnik_Hotel.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gopnik_Hotel.ViewModels
{
    public class BoekingViewModel
    {
        private Boeking _boeking;

        public BoekingViewModel(Boeking boeking)
        {
            this._boeking = boeking;
        }

        public Boeking ToModel()
        {
            return _boeking;
        }

        public int BoekingId { get { return _boeking.BoekingId; } set { _boeking.BoekingId = value; } }

        [DataType(DataType.DateTime)]
        public DateTime Datum { get { return _boeking.Datum; } set { _boeking.Datum = value; } }

        [DisplayName("Kamer")]
        public int IdKamer { get { return _boeking.IdKamer; } set { _boeking.IdKamer = value; } }

        [DisplayName("Klant")]
        public int IdKlant { get { return _boeking.IdKlant; } set { _boeking.IdKlant = value; } }

        public KamerViewModel Kamer { get { return new KamerViewModel(_boeking.Kamer); } set { _boeking.Kamer = value.ToModel(); } }

        public KlantViewModel Klant { get { return new KlantViewModel(_boeking.Klant); } set { _boeking.Klant = value.ToModel(); } }

    }
}