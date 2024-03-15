using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Cofetarie.Pages.Interogari_Complexe
{
    public class Interogare3Model : PageModel
    {
        public List<string> RezultateInterogare3C { get; set; } = new List<string>();

        public void OnGet(double? minCalorii)
        {
            if (!minCalorii.HasValue || minCalorii < 0)
            {
                ModelState.AddModelError("minCalorii", "Introduceți un număr valid de calorii (mai mare sau egal cu 0).");
                return;
            }

            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Cofetarie;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = @"
                SELECT P.Denumire AS 'Denumire', P.Calorii AS 'Calorii'
                FROM Produse P
                WHERE P.Calorii > @minCalorii AND EXISTS (
                    SELECT COUNT(IP.ID_Produse)
                    FROM IngredienteProduse IP
                    INNER JOIN Ingrediente I ON IP.ID_Ingrediente = I.ID_Ingrediente
                    WHERE IP.ID_Produse = P.ID_Produse AND I.ID_Furnizori IN (
                        SELECT F.ID_Furnizori
                        FROM Furnizori F
                        INNER JOIN Ingrediente I2 ON F.ID_Furnizori = I2.ID_Furnizori
                        GROUP BY F.ID_Furnizori
                        HAVING COUNT(I2.ID_Ingrediente) = (
                            SELECT TOP 1 COUNT(I1.ID_Ingrediente)
                            FROM Furnizori F1
                            INNER JOIN Ingrediente I1 ON F1.ID_Furnizori = I1.ID_Furnizori
                            GROUP BY I1.ID_Furnizori
                            ORDER BY COUNT(I1.ID_Ingrediente) DESC
                        )
                    )
                );
            ";


                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@minCalorii", minCalorii);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string Denumire = reader["Denumire"].ToString();
                            string Calorii = reader["Calorii"].ToString();
                            
                            string rezultat = $"{Denumire}  : Calorii: {Calorii}";
                            RezultateInterogare3C.Add(rezultat);
                        }
                    }
                }
            }
        }
    }
}
