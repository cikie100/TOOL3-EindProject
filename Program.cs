using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

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
            String Gemeentenaam = "Aalst";

            List<int> intlijst = db.geefStraatIds_vanGemeenteNaam(Gemeentenaam);
            Console.WriteLine("StraatIds voor gemeente: " + Gemeentenaam);
            intlijst.ForEach(intt => Console.WriteLine(intt));

            //•Als gebruiker wil ik  alle straatnamen van een  gemeente kunnen opvragen(alfabetisch gesorteerd).
            String Gemeentenaam2 = "Aalst";
            Console.WriteLine("Straatnamen voor gemeente: " + Gemeentenaam2);
            List<String> straatNamenlijst = db.geefStraatnamenLijst_vanGemeenteNaam(Gemeentenaam2);
            straatNamenlijst.Sort();
            straatNamenlijst.ForEach(intt => Console.WriteLine(intt));

            Console.ReadLine();
        }
    }
}
