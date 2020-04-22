using System.Collections.Generic;

namespace Tool3.Klassen
{
    public class Knoop
    {
        #region Properties

        public int knoopID { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public List<Segment> segmenten { get; set; }

        #endregion Properties

        public Knoop(int id, double x, double y)
        {
            this.knoopID = id;
            this.x = x;
            this.y = y;
            this.segmenten = new List<Segment>();
        }

        public override bool Equals(object obj)
        {
            return obj is Knoop knoop &&
                   knoopID == knoop.knoopID &&
                   x == knoop.x &&
                   y == knoop.y;
        }

        public override string ToString()
        {
            string xx = ("Knoop [" + knoopID.ToString() + ",[" + x.ToString() + ", " + y.ToString() + "]]");
            foreach (Segment seg in segmenten)
            {
                xx += seg.ToString();
            }

                return xx;
        }





        //
    }
}