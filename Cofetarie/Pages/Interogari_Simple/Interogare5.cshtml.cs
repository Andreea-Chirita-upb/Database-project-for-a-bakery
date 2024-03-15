using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Cofetarie.Pages.Interogari_Simple
{
    public class Interogare5Model : PageModel
    {
        public List<ResultModel5> RezultateInterogare5 { get; set; } = new List<ResultModel5>();

        public void OnGet()
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Cofetarie;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = "SELECT DISTINCT Cl.Nume + Cl.Prenume AS 'NumeClient', P.Denumire AS 'Nume produs in care apare ingredientul' " +
                                  "FROM Clienti Cl " +
                                  "INNER JOIN Comenzi C " +
                                  "ON Cl.ID_Clienti = C.ID_Clienti " +
                                  "INNER JOIN ProduseComanda PC " +
                                  "ON C.ID_Comenzi = PC.ID_Comenzi " +
                                  "INNER JOIN IngredienteProduse IP " +
                                  "ON PC.ID_Produse = IP.ID_Produse " +
                                  "INNER JOIN Ingrediente I " +
                                  "ON IP.ID_Ingrediente = I.ID_Ingrediente " +
                                  "INNER JOIN Produse P " +
                                  "ON IP.ID_Produse = P.ID_Produse "+
                                  "WHERE I.Nume = 'Arahide caramelizate' ";


                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ResultModel5 rezultat = new ResultModel5
                            {
                                NumeClient = reader["NumeClient"].ToString(),
                                NumeProdus = reader["Nume produs in care apare ingredientul"].ToString()

                            };

                            RezultateInterogare5.Add(rezultat);
                        }
                    }
                }
            }
        }
    }

    public class ResultModel5
    {
        public string NumeClient { get; set; }
        public string NumeProdus{ get; set; }
    }
}
