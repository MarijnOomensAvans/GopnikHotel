using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopnik_Hotel.Models
{
    public class Korting
    {
        DateTime dt;
        int aantalKamers;
        Klant[] klanten;
        public Korting(DateTime dtm, int rooms, Klant[] names)
        {
            dt = dtm;
            aantalKamers = rooms;
            klanten = names;
        }

        public int getKorting()
        {
            int korting = 0;

            if (dt.DayOfWeek == DayOfWeek.Monday || dt.DayOfWeek == DayOfWeek.Tuesday)
            {
                korting = korting + 15;
            }
            Console.WriteLine(korting);
            if (GetWeekNumber(dt) % 2 != 0)
            {
                korting = korting + 3;
            }
            korting = korting + aantalKamers;
            int bestKorting = 0;
            int bestcurrentKorting = 0;
            for (int i = 0; i < klanten.Length; i++)
            {
                bestcurrentKorting = 0;
                for (int x = 0; x < klanten[i].Naam.Length; x++)
                {
                    int ch = 97 + x;
                    if (klanten[i].Naam.ToLower().Contains((char)ch))
                    {
                        bestcurrentKorting = bestcurrentKorting + 2;
                    }
                    else
                    {
                        break;
                    }
                }
                if (bestKorting < bestcurrentKorting)
                {
                    bestKorting = bestcurrentKorting;
                }
            }
            korting = korting + bestKorting;
            Random r = new Random();
            int dobbelsteen = r.Next(1, 6);
            korting = korting * dobbelsteen;
            if (korting > 60)
            {
                korting = 60;
            }
            return korting;
        }
        public int GetWeekNumber(DateTime dt)
        {
            DayOfWeek dtc = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(dt);
            if (dtc >= DayOfWeek.Monday && dtc <= DayOfWeek.Wednesday)
            {
                dt = dt.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(dt, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}