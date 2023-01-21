using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Net.Security;
using static Crud.Pages.Clientes.IndexModel;

namespace Crud.Pages.Clientes
{
    public class EditModel : PageModel
    {
        //public List<ClientInfo> listClients = new List<ClientInfo>();

        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String Id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clientes WHERE id = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("id", Id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.nome = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.telefone = reader.GetString(3);
                                clientInfo.endereco = reader.GetString(4);
                                clientInfo.criacao_em = reader.GetDateTime(5).ToString();
                            }
                        }



                        command.Parameters.AddWithValue("@nome", clientInfo.nome);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@telefone", clientInfo.telefone);
                        command.Parameters.AddWithValue("@endereco", clientInfo.endereco);

                        command.ExecuteNonQuery();

                    }
                }

            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost()
        {
            clientInfo.id = Request.Form["id"];
            clientInfo.nome = Request.Form["nome"];
            clientInfo.email = Request.Form["email"];
            clientInfo.telefone = Request.Form["telefone"];
            clientInfo.endereco = Request.Form["endereco"];

            if(clientInfo.nome.Length == 0)
            {
                errorMessage = "Todos os campos são requeridos";
                return;
            }

            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql =    @"UPDATE clientes 
                                    SET nome = @nome, email = @email, telefone = @telefone, endereco = @endereco
                                    WHERE id = @id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome", clientInfo.nome);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@telefone", clientInfo.telefone);
                        command.Parameters.AddWithValue("@endereco", clientInfo.endereco);
                        command.Parameters.AddWithValue("@id", clientInfo.id);

                        command.ExecuteNonQuery();

                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }

            Response.Redirect("/Clientes/Index");
        }
    }
}
