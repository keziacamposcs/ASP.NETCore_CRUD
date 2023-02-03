using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static CRUD.Pages.Clients.IndexModel;

namespace CRUD.Pages.Clients
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
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];
            clientInfo.created_at = Request.Form["created_at"];

            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=CRUD;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO clients (name, email, phone, address, created_at) VALUES (@name, @email, @phone, @address, @created_at)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);
                        command.Parameters.AddWithValue("created_at", clientInfo.created_at);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            clientInfo.name = ""; clientInfo.email = ""; clientInfo.phone = ""; clientInfo.address = ""; clientInfo.created_at = "";
            successMessage = "Novo cliente adicionado com sucesso!";
            Response.Redirect("/Clients/Index");
        }

    }
}
