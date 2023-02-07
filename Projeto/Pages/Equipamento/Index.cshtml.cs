using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Projeto.Pages.Equipamento
{
    public class IndexModel : PageModel
    {
        public List<EquipamentoInfo> equipamentos = new List<EquipamentoInfo>();

        public void OnGet()
        {
            try
            {
                String conexao = "Data Source=(localdb)\\local;Initial Catalog=CRUD;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(conexao))
                {
                    connection.Open();
                    String sql = "SELECT * FROM equipamento";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EquipamentoInfo equip = new EquipamentoInfo();
                                equip.id = "" + reader.GetInt32(0);
                                equip.nome = reader.GetString(1);
                                equip.num_serie = reader.GetString(2);


                                equipamentos.Add(equip);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        //Classe
        public class EquipamentoInfo
        {
            public String id;
            public String nome;
            public String num_serie;
        }
    }
}
