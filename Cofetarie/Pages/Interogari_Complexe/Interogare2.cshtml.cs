using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Cofetarie.Pages.Interogari_Complexe
{
    public class Interogare2Model : PageModel
    {
        public List<ResultModel2C> RezultateInterogare2C { get; set; } = new List<ResultModel2C>();

        public void OnGet()
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Cofetarie;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = @"
                    SELECT A.Nume AS 'Nume Angajat', A.Prenume AS 'Prenume Angajat', A.Salariu AS 'Salariu'
                    FROM Angajati A
                    WHERE A.Salariu > (
                        SELECT MAX(A1.Salariu)
                        FROM Angajati A1
                        INNER JOIN Comenzi C ON A1.ID_Angajati = C.ID_Angajati
                        WHERE YEAR(A1.DataAngajarii) >= '2017' AND C.ID_Comenzi IN (
                            SELECT C1.ID_Comenzi
                            FROM Comenzi C1
                            INNER JOIN ProduseComanda PC ON C1.ID_Comenzi = PC.ID_Comenzi
                            GROUP BY C1.ID_Comenzi
                            HAVING SUM(PC.Cantitate) = (
                                SELECT TOP 1 SUM(PC1.Cantitate)
                                FROM ProduseComanda PC1
                                GROUP BY PC1.ID_Comenzi
                                ORDER BY SUM(PC1.Cantitate) DESC
                            )
                        )
                    )";


                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ResultModel2C rezultat = new ResultModel2C
                            {
                                nume = reader["Nume Angajat"].ToString(),
                                prenume = reader["Prenume Angajat"].ToString(),
                                salariu = reader["Salariu"].ToString(),
                                

                            };

                            RezultateInterogare2C.Add(rezultat);
                        }
                    }
                }
            }
        }
    }

    public class ResultModel2C
    {
        public string nume { get; set; }
        public string prenume { get; set; }
        public string salariu { get; set; }


    }
}
