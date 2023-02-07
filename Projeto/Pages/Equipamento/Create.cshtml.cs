using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static Projeto.Pages.Equipamento.IndexModel;

namespace Projeto.Pages.Equipamento
{
    public class CreateModel : PageModel
    {

        public EquipamentoInfo equip = new EquipamentoInfo();

        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }
        public void OnPost()
        {
            equip.nome = Request.Form["nome"];
            equip.num_serie = Request.Form["num_serie"];

            if (equip.nome.Length == 0 || equip.num_serie.Length == 0)
            {
                errorMessage = "Algum campo vazio";
                return;
            }

            try
            {
                String conexao = "Data Source=localhost;Initial Catalog=CRUD;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(conexao))
                {
                    connection.Open();
                    String sql = "INSERT INTO equipamento (nome, num_serie) VALUES (@nome, @num_serie)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome", equip.nome);
                        command.Parameters.AddWithValue("@num_serie", equip.num_serie);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            equip.nome = "";
            equip.num_serie = "";

            successMessage = "Adicionado com sucesso";

            Response.Redirect("/Equipamento/Index");
        }
    }
}
