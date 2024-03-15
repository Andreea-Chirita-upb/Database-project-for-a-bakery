using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Cofetarie.Pages.Furnizori
{
    public class IndexModel : PageModel
    {
        public List<FurnizoriInfo> listFurnizori = new List<FurnizoriInfo>(); //lista cu toti angajatii din baza de date
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Cofetarie;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Furnizori";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FurnizoriInfo furnizoriInfo = new FurnizoriInfo();
                                furnizoriInfo.ID_Furnizori = "" + reader.GetInt32(0);
                                furnizoriInfo.Nume = reader.GetString(1);
                                furnizoriInfo.Strada = reader.GetString(2);
                                furnizoriInfo.Numar = reader.GetString(3);
                                furnizoriInfo.Oras = reader.GetString(4);
                                furnizoriInfo.Judet = reader.GetString(5);
                                furnizoriInfo.NumarTelefon = reader.GetString(6);
                                furnizoriInfo.AdresaEmail = reader.GetString(7);


                                listFurnizori.Add(furnizoriInfo);
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

public class FurnizoriInfo
{
    public String ID_Furnizori;
    public String Nume;
    public String Strada;
    public String Numar;
    public String Oras;
    public String Judet;
    public String NumarTelefon;
    public String AdresaEmail;


}