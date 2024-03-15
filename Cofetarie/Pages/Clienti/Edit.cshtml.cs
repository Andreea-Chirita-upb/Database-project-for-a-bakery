using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Cofetarie.Pages.Clienti
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
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
                    String sql = "SELECT * FROM Clienti WHERE ID_Clienti = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                
                                clientInfo.ID_Clienti = "" + reader.GetInt32(0);
                                clientInfo.Nume = reader.GetString(1);
                                clientInfo.Prenume = reader.GetString(2);
                                clientInfo.CNP = reader.GetString(3);
                                clientInfo.Sex = reader.GetString(4);
                                clientInfo.Strada = reader.GetString(5);
                                clientInfo.Numar = reader.GetString(6);
                                clientInfo.Oras = reader.GetString(7);
                                clientInfo.Judet = reader.GetString(8);
                                clientInfo.NumarTelefon = reader.GetString(9);

                              
                            }
                        }
                    }
                }
            }

            

            catch(Exception ex) 
            {
                errorMessage = ex.Message;
                return;
            }
        }

        public void OnPost()
        {
            clientInfo.ID_Clienti = Request.Form["id"];
            clientInfo.Nume = Request.Form["nume"];
            clientInfo.Prenume = Request.Form["prenume"];
            clientInfo.CNP = Request.Form["cnp"];
            clientInfo.Sex = Request.Form["sex"];
            clientInfo.Strada = Request.Form["strada"];
            clientInfo.Numar = Request.Form["numar"];
            clientInfo.Oras = Request.Form["oras"];
            clientInfo.Judet = Request.Form["judet"];
            clientInfo.NumarTelefon = Request.Form["numartelefon"];


            if (clientInfo.ID_Clienti.Length == 0 || clientInfo.Nume.Length == 0 || clientInfo.Prenume.Length == 0 || clientInfo.CNP.Length == 0 || clientInfo.Sex.Length == 0
                || clientInfo.Strada.Length == 0 || clientInfo.Numar.Length == 0 || clientInfo.Oras.Length == 0 || clientInfo.Judet.Length == 0 || clientInfo.NumarTelefon.Length == 0)
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
                    String sql = "UPDATE Clienti " +
                                 "SET Nume = @nume, Prenume = @prenume, CNP = @cnp, Sex = @sex, Strada = @strada, Numar = @numar, Oras = @oras, Judet = @judet, NumarTelefon = @numartelefon " +
                                   "WHERE ID_Clienti = @id";


                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", clientInfo.ID_Clienti);
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

            Response.Redirect("/Clienti/Index");
        }
    }
}
