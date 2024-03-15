using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Cofetarie.Pages.Interogari_Simple
{
    public class IndexModel : PageModel
    {
        public List<ProduseInfo> listProduse = new List<ProduseInfo>();
        public List<ComenziInfo> listComenzi = new List<ComenziInfo>();
        public List<ProduseComandaInfo> listProduseComanda = new List<ProduseComandaInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Cofetarie;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Selectare Produse
                    String sqlProduse = "SELECT * FROM Produse";
                    using (SqlCommand commandProduse = new SqlCommand(sqlProduse, connection))
                    {
                        using (SqlDataReader readerProduse = commandProduse.ExecuteReader())
                        {
                            while (readerProduse.Read())
                            {
                                ProduseInfo produseInfo = new ProduseInfo();
                                produseInfo.ID_Produse = "" + readerProduse.GetInt32(0);
                                produseInfo.ID_Angajati = "" + readerProduse.GetInt32(1);
                                produseInfo.Pret = "" + readerProduse.GetDecimal(2);
                                produseInfo.Denumire = readerProduse.GetString(3);
                                produseInfo.CantitateDisponibila = "" + readerProduse.GetInt32(4);
                                produseInfo.CategorieProdus = readerProduse.GetString(5);
                                produseInfo.Calorii = "" + readerProduse.GetDecimal(6);
                                listProduse.Add(produseInfo);
                            }
                        }
                    }

                    // Selectare Comenzi
                    String sqlComenzi = "SELECT * FROM Comenzi";
                    using (SqlCommand commandComenzi = new SqlCommand(sqlComenzi, connection))
                    {
                        using (SqlDataReader readerComenzi = commandComenzi.ExecuteReader())
                        {
                            while (readerComenzi.Read())
                            {
                                ComenziInfo comenziInfo = new ComenziInfo();
                                comenziInfo.ID_Comenzi = "" + readerComenzi.GetInt32(0);
                                comenziInfo.ID_Clienti = "" + readerComenzi.GetInt32(1);
                                comenziInfo.ID_Angajati = "" + readerComenzi.GetInt32(2);
                                comenziInfo.DataComenzii = readerComenzi.GetDateTime(3).ToString();
                                comenziInfo.MetodaPlata = readerComenzi.GetString(4);
                                comenziInfo.StatusComanda = readerComenzi.GetString(5);
                                listComenzi.Add(comenziInfo);
                            }
                        }
                    }

                    // Selectare ProduseComanda
                    String sqlProduseComanda = "SELECT * FROM ProduseComanda";
                    using (SqlCommand commandProduseComanda = new SqlCommand(sqlProduseComanda, connection))
                    {
                        using (SqlDataReader readerProduseComanda = commandProduseComanda.ExecuteReader())
                        {
                            while (readerProduseComanda.Read())
                            {
                                ProduseComandaInfo produseComandaInfo = new ProduseComandaInfo();
                                produseComandaInfo.ID_Comenzi = "" + readerProduseComanda.GetInt32(0);
                                produseComandaInfo.ID_Produse = "" + readerProduseComanda.GetInt32(1);
                                produseComandaInfo.Cantitate = "" + readerProduseComanda.GetInt32(2);
                                listProduseComanda.Add(produseComandaInfo);
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

    public class ProduseInfo
    {
        public String ID_Produse;
        public String ID_Angajati;
        public String Pret;
        public String Denumire;
        public String CantitateDisponibila;
        public String CategorieProdus;
        public String Calorii;
    }

    public class ComenziInfo
    {
        public String ID_Comenzi;
        public String ID_Clienti;
        public String ID_Angajati;
        public String DataComenzii;
        public String MetodaPlata;
        public String StatusComanda;
    }

    public class ProduseComandaInfo
    {
        public String ID_Comenzi;
        public String ID_Produse;
        public String Cantitate;
    }
}
