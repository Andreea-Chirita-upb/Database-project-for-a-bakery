using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Cofetarie.Pages.Furnizori
{
    public class EditModel : PageModel
    {
        public FurnizoriInfo furnizoriInfo = new FurnizoriInfo();

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
                    String sql = "SELECT * FROM Furnizori WHERE ID_Furnizori = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                furnizoriInfo.ID_Furnizori = "" + reader.GetInt32(0);
                                furnizoriInfo.Nume = reader.GetString(1);
                                furnizoriInfo.Strada = reader.GetString(2);
                                furnizoriInfo.Numar = reader.GetString(3);
                                furnizoriInfo.Oras = reader.GetString(4);
                                furnizoriInfo.Judet = reader.GetString(5);
                                furnizoriInfo.NumarTelefon = reader.GetString(6);
                                furnizoriInfo.AdresaEmail = reader.GetString(7);


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
        public void OnPost()
        {
            furnizoriInfo.ID_Furnizori = Request.Form["id"];
            furnizoriInfo.Nume = Request.Form["nume"];
            furnizoriInfo.Strada = Request.Form["strada"];
            furnizoriInfo.Numar = Request.Form["numar"];
            furnizoriInfo.Oras = Request.Form["oras"];
            furnizoriInfo.Judet = Request.Form["judet"];
            furnizoriInfo.NumarTelefon = Request.Form["numartelefon"];
            furnizoriInfo.AdresaEmail = Request.Form["adresaemail"];

            if (furnizoriInfo.ID_Furnizori.Length == 0 || furnizoriInfo.Nume.Length == 0 
                || furnizoriInfo.Strada.Length == 0 || furnizoriInfo.Numar.Length == 0 || furnizoriInfo.Oras.Length == 0 || furnizoriInfo.Judet.Length == 0 || furnizoriInfo.NumarTelefon.Length == 0
                ||  furnizoriInfo.AdresaEmail.Length == 0)
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



                    String sql = "UPDATE Furnizori " +
                                 "SET Nume = @nume, Strada = @strada, Numar = @numar, Oras = @oras, Judet = @judet,NumarTelefon = @numartelefon,AdresaEmail = @adresaemail " +
                                 "WHERE ID_Furnizori=@id;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", furnizoriInfo.ID_Furnizori);
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
            Response.Redirect("/Furnizori/Index");
        }
    }
}
