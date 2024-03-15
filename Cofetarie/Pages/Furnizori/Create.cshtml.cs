using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Cofetarie.Pages.Furnizori
{
    public class CreateModel : PageModel
    {
        public FurnizoriInfo furnizoriInfo = new FurnizoriInfo();

        public String errorMessage = "";
        public String succesMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            furnizoriInfo.Nume = Request.Form["nume"];
            furnizoriInfo.Strada = Request.Form["strada"];
            furnizoriInfo.Numar = Request.Form["numar"];
            furnizoriInfo.Oras = Request.Form["oras"];
            furnizoriInfo.Judet = Request.Form["judet"];
            furnizoriInfo.NumarTelefon = Request.Form["numartelefon"];
            furnizoriInfo.AdresaEmail = Request.Form["adresaemail"];

            if (furnizoriInfo.Nume.Length == 0  
                || furnizoriInfo.Strada.Length == 0 || furnizoriInfo.Numar.Length == 0 || furnizoriInfo.Oras.Length == 0 || furnizoriInfo.Judet.Length == 0 || furnizoriInfo.NumarTelefon.Length == 0
                 ||  furnizoriInfo.AdresaEmail.Length == 0)
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
                    String sql = "INSERT INTO Furnizori" +
                                 "(nume,strada,numar,oras,judet,numartelefon,adresaemail) VALUES " +
                                 "(@nume,@strada,@numar,@oras,@judet,@numartelefon,@adresaemail);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nume", furnizoriInfo.Nume);
                        command.Parameters.AddWithValue("@strada", furnizoriInfo.Strada);
                        command.Parameters.AddWithValue("@numar", furnizoriInfo.Numar);
                        command.Parameters.AddWithValue("@oras", furnizoriInfo.Oras);
                        command.Parameters.AddWithValue("@judet", furnizoriInfo.Judet);
                        command.Parameters.AddWithValue("@numartelefon", furnizoriInfo.NumarTelefon);
                        command.Parameters.AddWithValue("@adresaemail", furnizoriInfo.AdresaEmail);
                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            furnizoriInfo.Nume = ""; 
            furnizoriInfo.Strada = ""; furnizoriInfo.Numar = ""; furnizoriInfo.Oras = ""; furnizoriInfo.Judet = ""; furnizoriInfo.NumarTelefon = "";
             furnizoriInfo.AdresaEmail = "";
            succesMessage = "Un nou furnizor a fost adaugat corect";

            Response.Redirect("/Furnizori/Index");



        }
    }
}
