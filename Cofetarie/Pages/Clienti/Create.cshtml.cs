using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Cofetarie.Pages.Clienti
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();

        public String errorMessage = "";
        public String succesMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            clientInfo.Nume = Request.Form["nume"];
            clientInfo.Prenume = Request.Form["prenume"];
            clientInfo.CNP = Request.Form["cnp"];
            clientInfo.Sex = Request.Form["sex"];
            clientInfo.Strada = Request.Form["strada"];
            clientInfo.Numar = Request.Form["numar"];
            clientInfo.Oras = Request.Form["oras"];
            clientInfo.Judet = Request.Form["judet"];
            clientInfo.NumarTelefon = Request.Form["numartelefon"];

            if(clientInfo.Nume.Length == 0 || clientInfo.Prenume.Length ==  0 || clientInfo.CNP.Length == 0 || clientInfo.Sex.Length == 0
                || clientInfo.Strada.Length == 0 || clientInfo.Numar.Length == 0 || clientInfo.Oras.Length == 0 || clientInfo.Judet.Length == 0 || clientInfo.NumarTelefon.Length == 0)
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
                    String sql = "INSERT INTO Clienti" +
                                 "(nume, prenume, cnp, sex,strada,numar,oras,judet,numartelefon) VALUES " +
                                 "(@nume,@prenume,@cnp,@sex,@strada,@numar,@oras,@judet,@numartelefon);";

                    using (SqlCommand  command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nume", clientInfo.Nume);
                        command.Parameters.AddWithValue("@prenume", clientInfo.Prenume);
                        command.Parameters.AddWithValue("@cnp", clientInfo.CNP); 
                        command.Parameters.AddWithValue("@sex", clientInfo.Sex);
                        command.Parameters.AddWithValue("@strada", clientInfo.Strada);
                        command.Parameters.AddWithValue("@numar", clientInfo.Numar);
                        command.Parameters.AddWithValue("@oras", clientInfo.Oras);
                        command.Parameters.AddWithValue("@judet", clientInfo.Judet);
                        command.Parameters.AddWithValue("@numartelefon", clientInfo.NumarTelefon);
                        command.ExecuteNonQuery();  

                    }
                }  
            }
            catch (Exception ex) 
            {
                errorMessage = ex.Message;
                return;
            }

            clientInfo.Nume = ""; clientInfo.Prenume = ""; clientInfo.CNP = ""; clientInfo.Sex = "";
            clientInfo.Strada = ""; clientInfo.Numar = ""; clientInfo.Oras = ""; clientInfo.Judet = ""; clientInfo.NumarTelefon = "";
            succesMessage = "New Client Added Correctly";

            Response.Redirect("/Clienti/Index");



        }
    }

}
