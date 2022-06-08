using System;
using System.Data.SqlClient;

namespace GeradorTestes.Infra.BancoDados.Compartilhado
{
    public static class Db
    {
        private const string enderecoBanco =
            "Data Source=(LocalDb)\\MSSQLLocalDB;" +
            "Initial Catalog=eAgendaDb;" +
            "Integrated Security=True;" +
            "Pooling=False";      

        public static void ExecutarSql(string sql)
        {            
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comando = new SqlCommand(sql, conexaoComBanco);

            conexaoComBanco.Open();
            comando.ExecuteNonQuery();
            conexaoComBanco.Close();
        }
    }
}
