using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Cofetarie.Pages.Interogari_Simple
{
    public class Interogare6Model : PageModel
    {
        public List<ResultModel6> RezultateInterogare6 { get; set; } = new List<ResultModel6>();

        public void OnGet()
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Cofetarie;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = "SELECT Cl.Nume + ' ' + Cl.Prenume AS 'NumeClient',  " +
                                  "SUM(P.Calorii * PC.Cantitate) AS 'Calorii per comanda' " +
                                  "FROM Clienti Cl " +
                                  "INNER JOIN Comenzi C ON Cl.ID_Clienti = C.ID_Clienti " +
                                  "INNER JOIN ProduseComanda PC ON C.ID_Comenzi = PC.ID_Comenzi " +
                                  "INNER JOIN Produse P ON PC.ID_Produse = P.ID_Produse " +
                                  "WHERE P.CategorieProdus = 'Patiserie dulce' " +
                                  "GROUP BY C.ID_Comenzi, Cl.Nume, Cl.Prenume " +
                                  "HAVING SUM(P.Calorii * PC.Cantitate) > 600 ";



                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ResultModel6 rezultat = new ResultModel6
                            {
                                NumeClient = reader["NumeClient"].ToString(),
                                Calorii = reader["Calorii per comanda"].ToString()

                            };

                            RezultateInterogare6.Add(rezultat);
                        }
                    }
                }
            }
        }
    }

    public class ResultModel6
    {
        public string NumeClient { get; set; }
        public string Calorii { get; set; }
    }
}
