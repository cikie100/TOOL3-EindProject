using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Tool3.Klassen;

namespace Tool3
{
    public class DataBeheer
    {
        #region standaard stuff ( DbProviderFactory, DbConnection, constructor,... )
         private readonly string connectionString;       

        public DataBeheer(string connectionString)
        {
            this.connectionString = connectionString;           
        }
        private SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);           
            return connection;
        }
        #endregion standaard stuff ( DbProviderFactory, DbConnection, constructor,... )

        #region werkend
        //•Als gebruiker wil ik een lijst van straatIDs kunnen opvragen voor een opgegeven gemeentenaam.
        public List<int> geefStraatIds_vanGemeenteNaam (String naamGemeente){
            SqlConnection connection = getConnection();

            List<int> lg = new List<int>();

            string queryString = "SELECT straatId " +
                "FROM dbo.Gemeente_straat gs " +
                "JOIN dbo.Gemeente g ON g.gemeenteId = gs.gemeenteId " +
                "WHERE gemeenteNaam = @gemeenteNaam";

            using (SqlCommand command = connection.CreateCommand()) {

                command.CommandText = queryString;
                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@gemeenteNaam";
                paramId.DbType = DbType.String;
                paramId.Value = naamGemeente;

                command.Parameters.Add(paramId);

                connection.Open();

                try
                {
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        int id = (int)dataReader["straatId"];
                     //   int straatId = dataReader.GetInt32(1); //verschillende methodes om data op te vragen !
                        lg.Add(id);
                    }
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
            return lg;
        }

        //•Als gebruiker wil ik  alle straatnamen van een  gemeente kunnen opvragen(alfabetisch gesorteerd).
        public List<string> geefStraatnamenLijst_vanGemeenteNaam(string gemeentenaam2)
        {
            SqlConnection connection = getConnection();
            List<string> lg = new List<string>();

            string queryString = "SELECT straatNaam " +
                "FROM Straat s " +
                "JOIN dbo.Gemeente_straat gs ON s.straatId = gs.straatId " +
                "JOIN dbo.Gemeente g ON g.gemeenteId = gs.gemeenteId " +
                "WHERE gemeenteNaam = @gemeenteNaam; ";


            using (SqlCommand command = connection.CreateCommand())
            {

                command.CommandText = queryString;
                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@gemeenteNaam";
                paramId.DbType = DbType.String;
                paramId.Value = gemeentenaam2;

                command.Parameters.Add(paramId);
                connection.Open();

                try
                {
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        string id = (string)dataReader["straatNaam"];
                       
                        lg.Add(id);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
            return lg;

        }

        //--•Als gebruiker wil ik een straat kunnen opvragen op basis van een meegegeven straatID.
        //--wat ik niet makkelijk kon doen in 1 query, heb ik maar in 3 gedaan
        public Straat geefStraat_VanStraatId(int gegstraatId)
        {
            SqlConnection connection = getConnection();

            Straat straat = new Straat();

            //Maakt de straat aan
            string queryString = "SELECT DISTINCT  s.straatId, straatNaam, gemeenteNaam, provincienaam, s.GraafId" +
                " FROM Straat s" +
                " JOIN Gemeente_straat gs ON s.straatId = gs.straatId" +
                " JOIN Gemeente g ON g.gemeenteId = gs.gemeenteId" +
                " JOIN Provincie_Gemeente pg ON pg.provincieID = g.gemeenteId" +
                " JOIN Provincie p ON p.provincieID = pg.provincieID" +
                "  JOIN Graaf_Knoop gk ON s.GraafId = gk.GraafId" +
                "  JOIN Knoop k ON k.knoopId = gk.knoopId" +
                "  JOIN Knoop_Segment ks ON ks.knoopId = k.knoopId" +
                "  JOIN Segment se ON se.SegmentId = ks.SegmentId" +
                "  JOIN Punt pu ON pu.SegmId = se.SegmentId" +
                 " Where s.straatId = @gegstraatId; ";


            using (SqlCommand command = connection.CreateCommand())
            {

                command.CommandText = queryString;
                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@gegstraatId";
                paramId.DbType = DbType.Int32;
                paramId.Value = gegstraatId;

                command.Parameters.Add(paramId);
                connection.Open();

                try
                {

                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        int straatId = (int)dataReader["straatId"];
                        string straatNaam = (string)dataReader["straatNaam"];
                        string gemeenteNaam = (string)dataReader["gemeenteNaam"];
                        string provincienaam = (string)dataReader["provincienaam"];
                        int GraafId = (int)dataReader["GraafId"];

                        straat.StraatID = straatId;
                        straat.Straatnaam = straatNaam;
                        straat.gemeenteNaam = gemeenteNaam;
                        straat.ProvincieNaam = provincienaam;
                        straat.GraafId = GraafId;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }

            //maakt de knopen aan voor deze straat
            string queryString2 = "SELECT DISTINCT k.knoopId, k.puntX, k.puntY FROM Straat JOIN graaf g ON g.GraafId = Straat.graafID JOIN Graaf_Knoop gk ON g.GraafId = gk.GraafId JOIN Knoop k ON k.knoopId = gk.knoopId JOIN Knoop_Segment ks ON ks.knoopId = k.knoopId JOIN Segment se ON se.SegmentId = ks.SegmentId JOIN Punt p ON p.SegmId = se.SegmentId Where Straat.straatId = @gegstraatId; ";

            using (SqlCommand command = connection.CreateCommand())
            {

                command.CommandText = queryString2;
                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@gegstraatId";
                paramId.DbType = DbType.Int32;
                paramId.Value = gegstraatId;

                command.Parameters.Add(paramId);
                connection.Open();

                try
                {

                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        int knoopId = (int)dataReader["knoopId"];
                        double puntX = (double)dataReader["puntX"];
                        double puntY = (double)dataReader["puntY"];

                        straat.knopen.Add(new Knoop(knoopId, puntX, puntY));
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
            //geeft hem segmenten en punten
            string queryString3 = "SELECT DISTINCT k.knoopId,se.SegmentId, se.BeginKnoopId, se.EindKnoopId, p.puntX, p.puntY" +
                " FROM Straat s" +
                " JOIN graaf g ON g.GraafId = s.graafID" +
                " JOIN Graaf_Knoop gk ON g.GraafId = gk.GraafId" +
                " JOIN Knoop k ON k.knoopId = gk.knoopId" +
                " JOIN Knoop_Segment ks ON ks.knoopId = k.knoopId" +
                " JOIN Segment se ON se.SegmentId = ks.SegmentId" +
                " JOIN Punt p ON p.SegmId = se.SegmentId" +
                " Where s.straatId = @gegstraatId; ";

            using (SqlCommand command = connection.CreateCommand())
            {

                command.CommandText = queryString3;
                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@gegstraatId";
                paramId.DbType = DbType.Int32;
                paramId.Value = gegstraatId;

                command.Parameters.Add(paramId);
                connection.Open();

                try
                {

                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        int knoopId = (int)dataReader["knoopId"];
                        int SegmentId = (int)dataReader["SegmentId"];
                        int BeginKnoopId = (int)dataReader["BeginKnoopId"];
                        int EindKnoopId = (int)dataReader["EindKnoopId"];
                        double puntX = (double)dataReader["puntX"];
                        double puntY = (double)dataReader["puntY"];



                        //als de knoop de segment nog niet heeft dan maak je die aan en voeg je punt toe
                        if (!straat.knopen.Where(k => k.knoopID.Equals(knoopId)).FirstOrDefault().segmenten.Any(s => s.segmentID.Equals(SegmentId)))
                        {
                            straat.knopen.Where(k => k.knoopID.Equals(knoopId))
                                .FirstOrDefault().segmenten.Add(new Segment(SegmentId, BeginKnoopId, EindKnoopId));

                            straat.knopen.Where(k => k.knoopID.Equals(knoopId))
                                .FirstOrDefault().segmenten.Where(s => s.segmentID.Equals(SegmentId)).FirstOrDefault()
                            .punten_verticles.Add(new Punt(puntX, puntY));
                        }
                        //als de knoop de segment wel al heeft, dan voeg je enkel de punt toe
                        else
                        {

                            straat.knopen.Where(k => k.knoopID.Equals(knoopId))
                                    .FirstOrDefault().segmenten.Where(s => s.segmentID.Equals(SegmentId)).FirstOrDefault()
                                .punten_verticles.Add(new Punt(puntX, puntY));
                        }


                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
            return straat;

        }

        //--•Als gebruiker   wil ik   een straat   kunnen opvragen   op basis   van de   straatnaam en   de gemeentenaam
        //--wat ik niet makkelijk kon doen in 1 query, heb ik maar in 3 gedaan
        public Straat geefStraat_VanStraatNaam_enGemeenteNaam(string straatNaam, string gemeenteNaam)
        {
            SqlConnection connection = getConnection();

            Straat straat = new Straat();

            //Maakt de straat aan
            string queryString = "SELECT DISTINCT  s.straatId, straatNaam, gemeenteNaam, provincienaam, s.GraafId" +
                " FROM Straat s" +
                " JOIN Gemeente_straat gs ON s.straatId = gs.straatId" +
                " JOIN Gemeente g ON g.gemeenteId = gs.gemeenteId" +
                " JOIN Provincie_Gemeente pg ON pg.provincieID = g.gemeenteId" +
                " JOIN Provincie p ON p.provincieID = pg.provincieID" +
                " JOIN Graaf_Knoop gk ON s.GraafId = gk.GraafId" +
                " JOIN Knoop k ON k.knoopId = gk.knoopId" +
                " JOIN Knoop_Segment ks ON ks.knoopId = k.knoopId" +
                " JOIN Segment se ON se.SegmentId = ks.SegmentId" +
                " JOIN Punt pu ON pu.SegmId = se.SegmentId" +
                " Where s.straatNaam = @gegstraatNaam AND g.gemeenteNaam = @geggemeenteNaam ";


            using (SqlCommand command = connection.CreateCommand())
            {

                command.CommandText = queryString;
                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@gegstraatNaam";
                paramId.DbType = DbType.String;
                paramId.Value = straatNaam;


                command.CommandText = queryString;
                SqlParameter paramId2 = new SqlParameter();
                paramId2.ParameterName = "@geggemeenteNaam";
                paramId2.DbType = DbType.String;
                paramId2.Value = gemeenteNaam;

                command.Parameters.Add(paramId);
                command.Parameters.Add(paramId2);
                connection.Open();

                try
                {

                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        int straatId = (int)dataReader["straatId"];
                        string sstraatNaam = (string)dataReader["straatNaam"];
                        string ggemeenteNaam = (string)dataReader["gemeenteNaam"];
                        string provincienaam = (string)dataReader["provincienaam"];
                        int GraafId = (int)dataReader["GraafId"];

                        straat.StraatID = straatId;
                        straat.Straatnaam = sstraatNaam;
                        straat.gemeenteNaam = ggemeenteNaam;
                        straat.ProvincieNaam = provincienaam;
                        straat.GraafId = GraafId;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }

            //maakt de knopen aan voor deze straat
            string queryString2 = "SELECT DISTINCT k.knoopId, k.puntX, k.puntY" +
                 " FROM Straat s" +
                " JOIN Gemeente_straat gs ON s.straatId = gs.straatId" +
                " JOIN Gemeente g ON g.gemeenteId = gs.gemeenteId" +
                " JOIN Graaf_Knoop gk ON s.GraafId = gk.GraafId" +
                " JOIN Knoop k ON k.knoopId = gk.knoopId" +
                " JOIN Knoop_Segment ks ON ks.knoopId = k.knoopId" +
                " JOIN Segment se ON se.SegmentId = ks.SegmentId" +
                " JOIN Punt pu ON pu.SegmId = se.SegmentId" +
                " Where s.straatNaam = @gegstraatNaam AND g.gemeenteNaam = @geggemeenteNaam ";



            using (SqlCommand command = connection.CreateCommand())
            {

                command.CommandText = queryString2;
                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@gegstraatNaam";
                paramId.DbType = DbType.String;
                paramId.Value = straatNaam;


                command.CommandText = queryString2;
                SqlParameter paramId2 = new SqlParameter();
                paramId2.ParameterName = "@geggemeenteNaam";
                paramId2.DbType = DbType.String;
                paramId2.Value = gemeenteNaam;

                command.Parameters.Add(paramId);
                command.Parameters.Add(paramId2);
                connection.Open();

                try
                {

                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        int knoopId = (int)dataReader["knoopId"];
                        double puntX = (double)dataReader["puntX"];
                        double puntY = (double)dataReader["puntY"];

                        straat.knopen.Add(new Knoop(knoopId, puntX, puntY));
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
            //geeft hem segmenten en punten
            string queryString3 = "SELECT DISTINCT k.knoopId,se.SegmentId, se.BeginKnoopId, se.EindKnoopId, pu.puntX, pu.puntY" +
                  " FROM Straat s" +
                " JOIN Gemeente_straat gs ON s.straatId = gs.straatId" +
                " JOIN Gemeente g ON g.gemeenteId = gs.gemeenteId" +
                " JOIN Graaf_Knoop gk ON s.GraafId = gk.GraafId" +
                " JOIN Knoop k ON k.knoopId = gk.knoopId" +
                " JOIN Knoop_Segment ks ON ks.knoopId = k.knoopId" +
                " JOIN Segment se ON se.SegmentId = ks.SegmentId" +
                " JOIN Punt pu ON pu.SegmId = se.SegmentId" +
                " Where s.straatNaam = @gegstraatNaam AND g.gemeenteNaam = @geggemeenteNaam ";

            using (SqlCommand command = connection.CreateCommand())
            {

                command.CommandText = queryString3;
                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@gegstraatNaam";
                paramId.DbType = DbType.String;
                paramId.Value = straatNaam;


                command.CommandText = queryString3;
                SqlParameter paramId2 = new SqlParameter();
                paramId2.ParameterName = "@geggemeenteNaam";
                paramId2.DbType = DbType.String;
                paramId2.Value = gemeenteNaam;

                command.Parameters.Add(paramId);
                command.Parameters.Add(paramId2);
                connection.Open();


                try
                {

                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        int knoopId = (int)dataReader["knoopId"];
                        int SegmentId = (int)dataReader["SegmentId"];
                        int BeginKnoopId = (int)dataReader["BeginKnoopId"];
                        int EindKnoopId = (int)dataReader["EindKnoopId"];
                        double puntX = (double)dataReader["puntX"];
                        double puntY = (double)dataReader["puntY"];



                        //als de knoop de segment nog niet heeft dan maak je die aan en voeg je punt toe
                        if (!straat.knopen.Where(k => k.knoopID.Equals(knoopId)).FirstOrDefault().segmenten.Any(s => s.segmentID.Equals(SegmentId)))
                        {
                            straat.knopen.Where(k => k.knoopID.Equals(knoopId))
                                .FirstOrDefault().segmenten.Add(new Segment(SegmentId, BeginKnoopId, EindKnoopId));

                            straat.knopen.Where(k => k.knoopID.Equals(knoopId))
                                .FirstOrDefault().segmenten.Where(s => s.segmentID.Equals(SegmentId)).FirstOrDefault()
                            .punten_verticles.Add(new Punt(puntX, puntY));
                        }
                        //als de knoop de segment wel al heeft, dan voeg je enkel de punt toe
                        else
                        {

                            straat.knopen.Where(k => k.knoopID.Equals(knoopId))
                                    .FirstOrDefault().segmenten.Where(s => s.segmentID.Equals(SegmentId)).FirstOrDefault()
                                .punten_verticles.Add(new Punt(puntX, puntY));
                        }


                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
            return straat;
        }
        #endregion

    }
}

