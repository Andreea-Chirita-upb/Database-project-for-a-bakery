

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Cofetarie.Pages.Angajati
{
    public class EditModel : PageModel
    {
        public AngajatiInfo angajatiInfo = new AngajatiInfo();

        public String errorMessage = "";
        public String succesMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Cofetarie;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Angajati WHERE ID_Angajati = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                               
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


                            }
                        }
                    }
                }
            }



            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }
        public void OnPost() {
            angajatiInfo.ID_Angajati = Request.Form["id"];
            angajatiInfo.Nume = Request.Form["nume"];
            angajatiInfo.Prenume = Request.Form["prenume"];
            angajatiInfo.CNP = Request.Form["cnp"];
            angajatiInfo.Sex = Request.Form["sex"];
            angajatiInfo.Strada = Request.Form["strada"];
            angajatiInfo.Numar = Request.Form["numar"];
            angajatiInfo.Oras = Request.Form["oras"];
            angajatiInfo.Judet = Request.Form["judet"];
            angajatiInfo.NumarTelefon = Request.Form["numartelefon"];
            angajatiInfo.DataNasterii = Request.Form["datanasterii"];
            angajatiInfo.Post = Request.Form["post"];
            angajatiInfo.DataAngajarii = Request.Form["dataangajarii"];
            angajatiInfo.Salariu = Request.Form["salariu"];
            angajatiInfo.AdresaEmail = Request.Form["adresaemail"];

            if ( angajatiInfo.ID_Angajati.Length == 0 || angajatiInfo.Nume.Length == 0 || angajatiInfo.Prenume.Length == 0 || angajatiInfo.CNP.Length == 0 || angajatiInfo.Sex.Length == 0
                || angajatiInfo.Strada.Length == 0 || angajatiInfo.Numar.Length == 0 || angajatiInfo.Oras.Length == 0 || angajatiInfo.Judet.Length == 0 || angajatiInfo.NumarTelefon.Length == 0
                || angajatiInfo.DataAngajarii.Length == 0 || angajatiInfo.DataNasterii.Length == 0 || angajatiInfo.Post.Length == 0 || angajatiInfo.Salariu.Length == 0 || angajatiInfo.AdresaEmail.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Cofetarie;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();


            
                    String sql = "UPDATE Angajati " +
                                 "SET Nume = @nume, Prenume = @prenume ,CNP = @cnp, Sex = @sex,Strada = @strada, Numar = @numar, Oras = @oras, Judet = @judet, DataNasterii = @datanasterii,NumarTelefon = @numartelefon,Post = @post,DataAngajarii = @dataangajarii,Salariu = @salariu,AdresaEmail = @adresaemail " +
                                 "WHERE ID_Angajati=@id;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", angajatiInfo.ID_Angajati);
                        command.Parameters.AddWithValue("@nume", angajatiInfo.Nume);
                        command.Parameters.AddWithValue("@prenume", angajatiInfo.Prenume);
                        command.Parameters.AddWithValue("@cnp", angajatiInfo.CNP);
                        command.Parameters.AddWithValue("@sex", angajatiInfo.Sex);
                        command.Parameters.AddWithValue("@strada", angajatiInfo.Strada);
                        command.Parameters.AddWithValue("@numar", angajatiInfo.Numar);
                        command.Parameters.AddWithValue("@oras", angajatiInfo.Oras);
                        command.Parameters.AddWithValue("@judet", angajatiInfo.Judet);
                        command.Parameters.AddWithValue("@datanasterii", angajatiInfo.DataNasterii);
                        command.Parameters.AddWithValue("@numartelefon", angajatiInfo.NumarTelefon);
                        command.Parameters.AddWithValue("@post", angajatiInfo.Post);
                        command.Parameters.AddWithValue("@dataangajarii", angajatiInfo.DataAngajarii);
                        command.Parameters.AddWithValue("@salariu", angajatiInfo.Salariu);
                        command.Parameters.AddWithValue("@adresaemail", angajatiInfo.AdresaEmail);
                        command.ExecuteNonQuery();

                    }
                }

                }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Angajati/Index");
        }
    }
}
