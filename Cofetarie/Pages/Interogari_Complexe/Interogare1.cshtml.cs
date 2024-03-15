using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data.SqlClient;

namespace Cofetarie.Pages.Interogari_Complexe
{
    public class Interogare1Model : PageModel
    {
        public List<ResultModel1C> RezultateInterogare1C { get; set; } = new List<ResultModel1C>();

        public void OnGet()
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Cofetarie;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = "SELECT P.Denumire AS 'Denumire Produs', P.Calorii AS 'Numar Calorii', P.CategorieProdus AS 'Categorie Produs' " +
                                  "FROM Produse P " +
                                  "WHERE P.Calorii = ( SELECT MAX(P1.Calorii) " +
                                  "FROM Produse P1 " +
                                  "WHERE P1.CategorieProdus = P.CategorieProdus) " +
                                  "ORDER BY P.Calorii ASC ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ResultModel1C rezultat = new ResultModel1C
                            {
                                DenumireProdus = reader["Denumire Produs"].ToString(),
                                NumarCalorii = reader["Numar Calorii"].ToString(),
                                CategorieProdus = reader["Categorie Produs"].ToString(),

                            };

                            RezultateInterogare1C.Add(rezultat);
                        }
                    }
                }
            }
        }
    }
    public class ResultModel1C
    {
        public string DenumireProdus { get; set; }
        public string CategorieProdus { get; set; }
        public string NumarCalorii { get; set; }
    }
}
