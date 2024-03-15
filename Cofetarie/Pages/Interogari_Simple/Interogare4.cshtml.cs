using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Cofetarie.Pages.Interogari_Simple
{
    public class Interogare4Model : PageModel
    {
        public List<ResultModel4> RezultateInterogare4 { get; set; } = new List<ResultModel4>();

        public void OnGet()
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Cofetarie;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = "SELECT TOP 3 F.Nume, COUNT(I.ID_Ingrediente) AS 'Numarul de Ingrediente furnizate' " +
                                  "FROM Furnizori F " +
                                  "INNER JOIN Ingrediente I " +
                                  "ON F.ID_Furnizori = I.ID_Furnizori " +
                                  "WHERE I.PretperKg > 35 " +
                                  "GROUP BY F.Id_Furnizori, F.Nume " +
                                  "ORDER BY COUNT(I.ID_Ingrediente) DESC ";


                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ResultModel4 rezultat = new ResultModel4
                            {
                                NumeFurnizor = reader["Nume"].ToString(),
                                NumarIngrediente = Convert.ToInt32(reader["Numarul de Ingrediente furnizate"])
                            };

                            RezultateInterogare4.Add(rezultat);
                        }
                    }
                }
            }
        }
    }

    public class ResultModel4
    {
        public string NumeFurnizor { get; set; }
        public int NumarIngrediente { get; set; }
    }
}
