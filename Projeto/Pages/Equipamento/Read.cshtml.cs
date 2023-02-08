using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Projeto.Pages.Equipamento.IndexModel;
using System.Data.SqlClient;

namespace Projeto.Pages.Equipamento
{
    public class ReadModel : PageModel
    {
        public EquipamentoInfo equip = new EquipamentoInfo();

        public String errorMessage = "";
        public String successMessage = "";


        public void OnGet()
        {
            //Pega a id na url que indica qual deve ser atualizado
            String id = Request.Query["id"];

            try
            {
                //Propriedades para conectar ao Banco de dados
                String conexao = "Data Source=localhost;Initial Catalog=CRUD;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(conexao))
                {
                    connection.Open();
                    String sql = "SELECT * FROM equipamento WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                equip.id = "" + reader.GetInt32(0);
                                equip.nome = reader.GetString(1);
                                equip.num_serie = reader.GetString(2);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
        }
    }
}
