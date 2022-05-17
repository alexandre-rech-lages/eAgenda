using eAgenda.Dominio.ModuloContato;
using System;
using System.Data.SqlClient;
namespace eAgenda.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var contato = ObterContato("Bruno Henrique", "11111111111");

            InserirContato(contato);

            contato.Nome = "Giordian Arrascaeta";
            contato.Telefone = "22222222222";

            EditarContato(contato);

            ExcluirContato(contato.Numero);
        }

        private static void ExcluirContato(int numero)
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

            #region  criar um comando de edição
            SqlCommand comandoExclusao = new SqlCommand();
            comandoExclusao.Connection = conexaoComBanco;
            string sql = @"	DELETE FROM [TBCONTATO]
		                        WHERE
			                        [NUMERO] = @NUMERO"; 

            comandoExclusao.CommandText = sql;

            #endregion

            #region passar os parâmetros para o comando de inserção
            comandoExclusao.Parameters.AddWithValue("NUMERO", numero);
            #endregion

            //executar o comando
            comandoExclusao.ExecuteNonQuery();

            //fechar a conexão
            conexaoComBanco.Close();
        }

        private static void EditarContato(Contato contato)
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

            #region  criar um comando de edição
            SqlCommand comandoEdicao = new SqlCommand();
            comandoEdicao.Connection = conexaoComBanco;
            string sql = @"	UPDATE [TBCONTATO]	
		                        SET
			                        [NOME] = @NOME,
			                        [EMAIL] = @EMAIL,
			                        [TELEFONE] = @TELEFONE,
			                        [EMPRESA] = @EMPRESA,
			                        [CARGO] = @CARGO
		                        WHERE
			                        [NUMERO] = @NUMERO"; 

            comandoEdicao.CommandText = sql;

            #endregion

            #region passar os parâmetros para o comando de inserção
            comandoEdicao.Parameters.AddWithValue("NUMERO", contato.Numero);
            comandoEdicao.Parameters.AddWithValue("NOME", contato.Nome);
            comandoEdicao.Parameters.AddWithValue("EMAIL", contato.Email);
            comandoEdicao.Parameters.AddWithValue("TELEFONE", contato.Telefone);
            comandoEdicao.Parameters.AddWithValue("EMPRESA", contato.Empresa);
            comandoEdicao.Parameters.AddWithValue("CARGO", contato.Cargo);
            #endregion

            //executar o comando
            comandoEdicao.ExecuteNonQuery();

            //fechar a conexão
            conexaoComBanco.Close();
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
                            );";

            sql += "SELECT SCOPE_IDENTITY();";

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
            var id = comandoInsercao.ExecuteScalar();

            novoContato.Numero = Convert.ToInt32(id);

            //fechar a conexão
            conexaoComBanco.Close();
        }

        private static Contato ObterContato(string nome, string telefone)
        {
            return new Contato
            {
                Nome = nome,
                Telefone = telefone,
                Email = "bruno@flamengo.com",
                Cargo = "Centro Avante",
                Empresa = "Mengão"
            };
        }
    }
}
