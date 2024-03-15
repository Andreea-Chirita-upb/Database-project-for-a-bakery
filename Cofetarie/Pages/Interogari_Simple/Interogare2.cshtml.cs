using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Cofetarie.Pages.Interogari_Simple
{
    public class Interogare2Model : PageModel
    {
        public List<ResultModel> RezultateInterogare2 { get; set; } = new List<ResultModel>();

        public void OnGet()
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Cofetarie;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = "SELECT Cl.Nume AS 'NumeClient', Cl.Prenume AS 'PrenumeClient', " +
                        "C.DataComenzii AS 'DataComenzii', " +
                        "P.Denumire AS 'ProdusCumparat', PC.Cantitate AS 'CantitateaDinProdus' " +
                        "FROM Clienti Cl " +
                        "INNER JOIN Comenzi C " +
                        "ON Cl.ID_Clienti = C.ID_Clienti " +
                        "INNER JOIN ProduseComanda PC " +
                        "ON C.ID_Comenzi = PC.ID_Comenzi " +
                        "INNER JOIN Produse P " +
                        "ON PC.ID_Produse = P.ID_Produse " +
                        "ORDER BY C.DataComenzii";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ResultModel rezultat = new ResultModel
                            {
                                NumeClient = reader["NumeClient"].ToString(),
                                PrenumeClient = reader["PrenumeClient"].ToString(),
                                DataComenzii = DateTime.Parse(reader["DataComenzii"].ToString()),
                                ProdusCumparat = reader["ProdusCumparat"].ToString(),
                                CantitateDinProdus = int.Parse(reader["CantitateaDinProdus"].ToString())
                            };

                            RezultateInterogare2.Add(rezultat);
                        }
                    }
                }
            }
        }
    }

    public class ResultModel
    {
        public string NumeClient { get; set; }
        public string PrenumeClient { get; set; }
        public DateTime DataComenzii { get; set; }
        public string ProdusCumparat { get; set; }
        public int CantitateDinProdus { get; set; }
    }
}
