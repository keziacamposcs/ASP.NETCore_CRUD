using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Crud.Pages.Clientes
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clientes";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.nome = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.telefone = reader.GetString(3);
                                clientInfo.endereco = reader.GetString(4);
                                clientInfo.criacao_em = reader.GetDateTime(5).ToString();

                                listClients.Add(clientInfo);
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {

            }
        }
    

        public class ClientInfo
        {
            public String id;
            public String nome;
            public String email;
            public String telefone;
            public String endereco;
            public String criacao_em;
        }
    }
}
