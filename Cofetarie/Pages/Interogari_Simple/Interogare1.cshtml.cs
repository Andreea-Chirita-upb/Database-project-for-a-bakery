using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Cofetarie.Pages.Interogari_Simple
{
    public class Interogare1Model : PageModel
    {
        public List<string> RezultateInterogare1 { get; set; } = new List<string>();

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

                string sqlQuery = "SELECT A.Nume, A.Prenume, P.Denumire, P.Calorii " +
                                  "FROM Angajati A " +
                                  "INNER JOIN Produse P ON A.ID_Angajati = P.ID_Angajati AND P.Calorii > @minCalorii";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@minCalorii", minCalorii);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nume = reader["Nume"].ToString();
                            string prenume = reader["Prenume"].ToString();
                            string denumireProdus = reader["Denumire"].ToString();
                            string caloriiProdus = reader["Calorii"].ToString();
                            string rezultat = $"{nume} {prenume} - {denumireProdus} : Calorii: {caloriiProdus}";
                            RezultateInterogare1.Add(rezultat);
                        }
                    }
                }
            }
        }
    }
}
