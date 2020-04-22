using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using Tool3.Klassen;

namespace Tool3
{
    class Program
    {
       private static void Main(string[] args)
        {
            #region databank

            
            //die @ moet erbij, anders geeft die gezaag over de "\"
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=LaboProjectDB;Integrated Security=True";

            DataBeheer db = new DataBeheer(connectionString);


            #endregion databank

            //•Als gebruiker wil ik een lijst van straatIDs kunnen opvragen voor een opgegeven gemeentenaam.
            // String Gemeentenaam = "Aalst";
            // StraatIdsOpvragenVanGemeenteNaam( db,  Gemeentenaam);

            //•Als gebruiker wil ik  alle straatnamen van een  gemeente kunnen opvragen(alfabetisch gesorteerd).
            // String Gemeentenaam2 = "Aalst";
            // StraatnamenOpvragenVanGemeenteNaam(db, Gemeentenaam2);


            // --•Als gebruiker wil ik een straat kunnen opvragen op basis van een meegegeven straatID.
            //int straatId = 15;
            //StraatOpvragenMetId(db, straatId);

            // --•Als gebruiker   wil ik   een straat   kunnen opvragen   op basis   van de   straatnaam en   de gemeentenaam.
           // String straatNaam = "Lageweg";
           // String gemeenteNaam = "Antwerpen";
           // StraatOpvragenMetStraatNaam_enGemeenteNaam(db,straatNaam, gemeenteNaam);

            Console.ReadLine();
        }

        #region methodes
        //•Als gebruiker wil ik  alle straatnamen van een  gemeente kunnen opvragen(alfabetisch gesorteerd).
         public static void StraatnamenOpvragenVanGemeenteNaam(DataBeheer db, String Gemeentenaam2) {
            Console.WriteLine("Straatnamen voor gemeente: " + Gemeentenaam2);
            List<String> straatNamenlijst = db.geefStraatnamenLijst_vanGemeenteNaam(Gemeentenaam2);
            straatNamenlijst.Sort();
            straatNamenlijst.ForEach(intt => Console.WriteLine(intt));

        }

        //•Als gebruiker wil ik een lijst van straatIDs kunnen opvragen voor een opgegeven gemeentenaam.
        public static void StraatIdsOpvragenVanGemeenteNaam(DataBeheer db, String Gemeentenaam)
        {
            List<int> intlijst = db.geefStraatIds_vanGemeenteNaam(Gemeentenaam);
            Console.WriteLine("StraatIds voor gemeente: " + Gemeentenaam);
            intlijst.ForEach(intt => Console.WriteLine(intt));

        }

        public static void StraatOpvragenMetId(DataBeheer db, int straatId) {
            Straat x = db.geefStraat_VanStraatId(straatId);
            Console.WriteLine(x.ToString());

        }

        public static void StraatOpvragenMetStraatNaam_enGemeenteNaam(DataBeheer db, string straatNaam, string gemeenteNaam)
        {
            Straat x = db.geefStraat_VanStraatNaam_enGemeenteNaam(straatNaam, gemeenteNaam);
            Console.WriteLine(x.ToString());

        }
        #endregion
    }
}
