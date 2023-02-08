using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static Projeto.Pages.Equipamento.IndexModel;

namespace Projeto.Pages.Equipamento
{
    public class UpdateModel : PageModel
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
                //String conexao = "Data Source=localhost;Initial Catalog=CRUD;Integrated Security=True";
                String conexao = "Data Source=DOF-FQ0KWT3\\SQLEXPRESS;Initial Catalog=CRUD;Integrated Security=True";
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
            catch(Exception ex) 
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            equip.id = Request.Query["id"];
            equip.nome = Request.Form["nome"];
            equip.num_serie = Request.Form["num_serie"];

            if (equip.id.Length == 0 || equip.nome.Length == 0 || equip.num_serie.Length == 0)
            {
                errorMessage = "Todos os campos devem ser preenchidos";
                return;
            }

            try
            {
                String conexao = "Data Source=localhost;Initial Catalog=CRUD;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(conexao))
                {
                    connection.Open();
                    String sql =    @"UPDATE equipamento 
                                    SET nome = @nome, num_serie = @num_serie 
                                    WHERE id = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome", equip.nome);
                        command.Parameters.AddWithValue("@num_serie", equip.num_serie);
                        command.Parameters.AddWithValue("@id", equip.id);

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

            successMessage = "Atualizado com sucesso";

            Response.Redirect("/Equipamento/Index");
        }
    }
}
