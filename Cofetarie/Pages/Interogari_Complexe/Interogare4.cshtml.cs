using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Cofetarie.Pages.Interogari_Complexe
{
    public class Interogare4Model : PageModel
    {

        public List<ResultModel4C> RezultateInterogare4C { get; set; } = new List<ResultModel4C>();

        public void OnGet()
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Cofetarie;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = @"
                                    SELECT  C.Nume AS 'Nume', C.Prenume AS 'Prenume'
                                    FROM Clienti C
                                    INNER JOIN Comenzi Co ON C.ID_Clienti = Co.ID_Clienti
                             
                                   
                                    WHERE Co.ID_Comenzi IN (
                                        SELECT Co1.ID_Comenzi
                                        FROM Angajati A
                                        INNER JOIN Comenzi Co1 ON A.ID_Angajati = Co1.ID_Angajati
                                        WHERE A.ID_Angajati = Co1.ID_Angajati AND A.Post = 'Cofetar Chef'
                                       
                                       
                                    )
                                   GROUP BY C.ID_Clienti,C.Nume, C.Prenume 
                                ";


                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ResultModel4C rezultat = new ResultModel4C
                            {
                                Nume = reader["Nume"].ToString(),
                                Prenume = reader["Prenume"].ToString(),
                      

                            };

                            RezultateInterogare4C.Add(rezultat);
                        }
                    }
                }
            }
        }
    }
    public class ResultModel4C
    {
        public string  Nume { get; set; }
        public string Prenume { get; set; }

    }
}
