using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Tool3
{
    public class DataBeheer
    {
        #region standaard stuff ( DbProviderFactory, DbConnection, constructor,... )
         private string connectionString;       

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
 

    #endregion

   }
}

