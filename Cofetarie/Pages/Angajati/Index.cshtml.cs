using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Cofetarie.Pages.Angajati
{

    public class IndexModel : PageModel
    {
        public List<AngajatiInfo> listAngajati = new List<AngajatiInfo>(); //lista cu toti angajatii din baza de date
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Cofetarie;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Angajati";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AngajatiInfo angajatiInfo = new AngajatiInfo();
                                angajatiInfo.ID_Angajati = "" + reader.GetInt32(0);
                                angajatiInfo.Nume = reader.GetString(1);
                                angajatiInfo.Prenume = reader.GetString(2);
                                angajatiInfo.CNP = reader.GetString(3);
                                angajatiInfo.Sex = reader.GetString(4);
                                angajatiInfo.Strada = reader.GetString(5);
                                angajatiInfo.Numar = reader.GetString(6);
                                angajatiInfo.Oras = reader.GetString(7);
                                angajatiInfo.Judet = reader.GetString(8);
                                angajatiInfo.DataNasterii = reader.GetDateTime(9).ToString();
                                angajatiInfo.NumarTelefon = reader.GetString(10);
                                angajatiInfo.Post = reader.GetString(11);
                                angajatiInfo.DataAngajarii = reader.GetDateTime(12).ToString();
                                angajatiInfo.Salariu = "" + reader.GetDecimal(13);
                                angajatiInfo.AdresaEmail = reader.GetString(14);


                                listAngajati.Add(angajatiInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

        }
    }
}
    public class AngajatiInfo
    {
        public String ID_Angajati;
        public String Nume;
        public String Prenume;
        public String CNP;
        public String Sex;
        public String Strada;
        public String Numar;
        public String Oras;
        public String Judet;
        public String DataNasterii;
        public String NumarTelefon;
        public String Post;
        public String DataAngajarii;
        public String Salariu;
        public String AdresaEmail;


    }