using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Net.Security;
using static Crud.Pages.Clientes.IndexModel;

namespace Crud.Pages.Clientes
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            clientInfo.nome = Request.Form["nome"];
            clientInfo.email = Request.Form["email"];
            clientInfo.telefone = Request.Form["telefone"];
            clientInfo.endereco = Request.Form["endereco"];
            clientInfo.criacao_em = Request.Form["criacao_em"];

            if (clientInfo.nome.Length == 0)
            {
                errorMessage = "Todos os campos são requeridos";
                return;
            }

            //Salvando dado no Banco de Dados
            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=mystore;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO clientes (nome, email, telefone, endereco) VALUES (@nome, @email, @telefone, @endereco)";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome", clientInfo.nome);
                        command.Parameters.AddWithValue("@email", clientInfo.nome);
                        command.Parameters.AddWithValue("@telefone", clientInfo.nome);
                        command.Parameters.AddWithValue("@endereco", clientInfo.nome);
                        
                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }


            clientInfo.nome = ""; clientInfo.email = ""; clientInfo.telefone = ""; clientInfo.endereco = ""; clientInfo.criacao_em = "";
            successMessage = "Novo cliente adicionado com sucesso!";

            Response.Redirect("/Clientes/Index");

            
        }

    }
}
