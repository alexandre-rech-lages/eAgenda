using eAgenda.Dominio.ModuloContato;
using System;
using System.Data.SqlClient;
namespace eAgenda.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InserirContato(ObterContato());
        }

        private static void InserirContato(Contato novoContato)
        {
            #region abrir a conexão com o banco de dados
            string enderecoBanco =
                "Data Source=(LocalDB)\\MSSqlLocalDB;" +
                "Initial Catalog=eAgendaDb;" +
                "Integrated Security=True;" +
                "Pooling=False";

            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoBanco;
            conexaoComBanco.Open();
            #endregion

            #region  criar um comando de inserção
            SqlCommand comandoInsercao = new SqlCommand();
            comandoInsercao.Connection = conexaoComBanco;
            string sql = @"INSERT INTO [TBCONTATO] 
                            (
                                [NOME],
                                [EMAIL],
                                [TELEFONE],
                                [EMPRESA],
                                [CARGO]
	                        )
	                        VALUES
                            (
                                @n,
                                @e,
                                @t,
                                @emp,
                                @c
                            )";

            comandoInsercao.CommandText = sql;

            #endregion

            #region passar os parâmetros para o comando de inserção
            comandoInsercao.Parameters.AddWithValue("n", novoContato.Nome);
            comandoInsercao.Parameters.AddWithValue("e", novoContato.Email);
            comandoInsercao.Parameters.AddWithValue("t", novoContato.Telefone);
            comandoInsercao.Parameters.AddWithValue("emp", novoContato.Empresa);
            comandoInsercao.Parameters.AddWithValue("c", novoContato.Cargo);
            #endregion

            //executar o comando
            comandoInsercao.ExecuteNonQuery();

            //fechar a conexão
            conexaoComBanco.Close();
        }

        private static Contato ObterContato()
        {
            return new Contato
            {
                Nome = "Bruno Henrique",
                Telefone = "987654321",
                Email = "bruno@flamengo.com",
                Cargo = "Centro Avante",
                Empresa = "Mengão"
            };
        }
    }
}
