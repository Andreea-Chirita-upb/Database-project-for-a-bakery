using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Cofetarie.Pages.Interogari_Simple
{
    public class Interogare3Model : PageModel
    {
        public List<ResultModel3> RezultateInterogare3 { get; set; } = new List<ResultModel3>();

        public void OnGet()
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Cofetarie;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = "SELECT TOP 1 Angajati.Nume AS 'NumeAngajat', Angajati.Prenume AS 'PrenumeAngajat', " +
                   "COUNT(Comenzi.ID_Comenzi) AS 'NumarComenzi' " +
                   "FROM Angajati " +
                   "INNER JOIN Comenzi ON Angajati.ID_Angajati = Comenzi.ID_Angajati " +
                   "WHERE Comenzi.DataComenzii > '2023-10-16' AND Comenzi.MetodaPlata = 'Cash' " +
                   "GROUP BY  Angajati.ID_Angajati, Angajati.Nume, Angajati.Prenume " +
                   "ORDER BY COUNT(Comenzi.ID_Comenzi) DESC";


                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ResultModel3 rezultat = new ResultModel3
                            {
                                NumeAngajat = reader["NumeAngajat"].ToString(),
                                PrenumeAngajat = reader["PrenumeAngajat"].ToString(),
                                NumarComenzi = Convert.ToInt32(reader["NumarComenzi"])
                            };

                            RezultateInterogare3.Add(rezultat);
                        }
                    }
                }
            }
        }
    }

    public class ResultModel3
    {
        public string NumeAngajat { get; set; }
        public string PrenumeAngajat { get; set; }
        public int NumarComenzi { get; set; }
    }
}
