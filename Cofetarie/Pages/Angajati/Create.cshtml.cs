using Cofetarie.Pages.Clienti;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Cofetarie.Pages.Angajati
{
    public class CreateModel : PageModel
    {
        public AngajatiInfo angajatiInfo = new AngajatiInfo();

        public String errorMessage = "";
        public String succesMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
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

            if (angajatiInfo.Nume.Length == 0 || angajatiInfo.Prenume.Length == 0 || angajatiInfo.CNP.Length == 0 || angajatiInfo.Sex.Length == 0
                || angajatiInfo.Strada.Length == 0 || angajatiInfo.Numar.Length == 0 || angajatiInfo.Oras.Length == 0 || angajatiInfo.Judet.Length == 0 || angajatiInfo.NumarTelefon.Length == 0
                || angajatiInfo.DataAngajarii.Length == 0 || angajatiInfo.DataNasterii.Length == 0 || angajatiInfo.Post.Length == 0 || angajatiInfo.Salariu.Length == 0 || angajatiInfo.AdresaEmail.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            // save the new client into the database

            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Cofetarie;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Angajati" +
                                 "(nume, prenume, cnp, sex,strada,numar,oras,judet,datanasterii,numartelefon,post,dataangajarii,salariu,adresaemail) VALUES " +
                                 "(@nume,@prenume,@cnp,@sex,@strada,@numar,@oras,@judet,@datanasterii,@numartelefon,@post,@dataangajarii,@salariu,@adresaemail);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
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
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            angajatiInfo.Nume = ""; angajatiInfo.Prenume = ""; angajatiInfo.CNP = ""; angajatiInfo.Sex = "";
            angajatiInfo.Strada = ""; angajatiInfo.Numar = ""; angajatiInfo.Oras = ""; angajatiInfo.Judet = ""; angajatiInfo.NumarTelefon = "";
            angajatiInfo.DataNasterii = ""; angajatiInfo.Post = ""; angajatiInfo.DataAngajarii = ""; angajatiInfo.Salariu = ""; angajatiInfo.AdresaEmail = "";
            succesMessage = "Un nou angajat a fost adaugat corect";

            Response.Redirect("/Angajati/Index");



        }
    }
}
