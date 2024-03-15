using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Cofetarie.Pages.Clienti
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
        public void OnGet()
        { 
          try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Cofetarie;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                { 
                    connection.Open();
                    String sql = "SELECT * FROM Clienti";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        { 
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
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
                                
                                listClients.Add(clientInfo);    
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            { 
                Console.WriteLine("Exception: " + ex.ToString());   
            }
        }
    }

    public class ClientInfo
    {
        public String ID_Clienti;
        public String Nume;
        public String Prenume;
        public String CNP;
        public String Sex;
        public String Strada;
        public String Numar;
        public String Oras;
        public String Judet;
        public String NumarTelefon;
    }
}
