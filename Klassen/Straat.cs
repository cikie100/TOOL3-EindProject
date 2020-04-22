using System;
using System.Collections.Generic;
using System.Text;

namespace Tool3.Klassen
{
    public class Straat
    {
        public string gemeenteNaam { get; set; }
        public string ProvincieNaam {get; set; }
        public int StraatID { get; set; }
        public string Straatnaam { get; set; }
        public int GraafId { get; set; }

        public List<Knoop> knopen { get; set; }

        public Straat(int straatID, string straatnaam, string gemeenteNaam, string provincieNaam, int graafId)
        {
           this.StraatID = straatID;
            this.Straatnaam = straatnaam;
            this.gemeenteNaam = gemeenteNaam;
            this.ProvincieNaam = provincieNaam;
            this.GraafId = graafId;

            knopen = new List<Knoop>();
        }
        public Straat()
        {
        

            knopen = new List<Knoop>();
        }

        public override string ToString()
        {
            int countplz = 0;
            knopen.ForEach(k => countplz += k.segmenten.Count);

            string x= (StraatID + ", " + Straatnaam.Trim() + ", " + gemeenteNaam.Trim() + ", " + ProvincieNaam+"\n" +
                "Graaf:" + GraafId.ToString() + "\n"+
                "aantal knopen:" + knopen.Count + "\n" +
                "aantal wegsegmenten:" + countplz + "\n" 
                );
            foreach (Knoop knoop in knopen)
            {
                x += knoop.ToString();
            }

            return x;
        }
    }
}
