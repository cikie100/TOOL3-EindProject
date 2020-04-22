using System.Collections.Generic;

namespace Tool3.Klassen
{
    public class Segment
    //Een wegsegment wordt begrensd door een begin-en eindknoop
    //en verwijst naar een lijst van punten die het segment beschrijven.
    {
        #region Properties

        public int segmentID { get; set; }
        public int beginknoop { get; set; }
        public int eindknoop { get; set; }
        public List<Punt> punten_verticles { get; set; }

        #endregion Properties

        public Segment(int segmentID, int beginknoopID, int eindknoopID)
        {
            this.segmentID = segmentID;
            this.beginknoop = beginknoopID;
            this.eindknoop = eindknoopID;
            this.punten_verticles = new List<Punt>();
        }


        public override bool Equals(object obj)
        {
            return obj is Segment segment &&
                   segmentID == segment.segmentID &&
                   beginknoop == segment.beginknoop &&
                   eindknoop == segment.eindknoop &&
                   EqualityComparer<List<Punt>>.Default.Equals(punten_verticles, segment.punten_verticles);
        }

        public override string ToString()
        {
           
            string x = ("\n\t[Segment" + segmentID.ToString() + ", begin: " + beginknoop.ToString() + ", eind: " + eindknoop.ToString()+ "\n" );
            foreach (Punt punt in punten_verticles)
            {
               x += punt.ToString();
            }

            return x;
                ;
        }
    }
}