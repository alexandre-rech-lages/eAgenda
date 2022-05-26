using eAgenda.Dominio.ModuloDespesa;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Infra.BancoDados.ModuloDespesa
{
    public class RepositorioCategoriaEmBancoDados : IRepositorioCategoria
    {
        private const string enderecoBanco =
               "Data Source=(LocalDB)\\MSSqlLocalDB;" +
               "Initial Catalog=eAgendaDb;" +
               "Integrated Security=True;" +
               "Pooling=False";

        #region Sql Queries
        private const string sqlInserir =
            @"INSERT INTO [TBCATEGORIA] 
                (
                    [TITULO]    
                )                
	            VALUES
                (
                    @TITULO

                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
            @"UPDATE [TBCATEGORIA]	
		        SET
			        [TITULO] = @TITULO 
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlExcluir =
            @"DELETE FROM [TBCATEGORIA]
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlSelecionarTodos =
            @"SELECT 
		            [NUMERO], 
		            [TITULO] 
	            FROM 
		            [TBCATEGORIA]";

        private const string sqlSelecionarPorNumero =
            @"SELECT 
		            [NUMERO], 
		            [TITULO]  
	            FROM 
		            [TBCATEGORIA]
		        WHERE
                    [NUMERO] = @NUMERO";

        #endregion

        public ValidationResult Inserir(Categoria novoCategoria)
        {
            var validador = new ValidadorCategoriaDespesa();

            var resultadoValidacao = validador.Validate(novoCategoria);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosCategoriaDespesa(novoCategoria, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            novoCategoria.Numero = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Categoria categoria)
        {
            var validador = new ValidadorCategoriaDespesa();

            var resultadoValidacao = validador.Validate(categoria);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosCategoriaDespesa(categoria, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Categoria categoria)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("NUMERO", categoria.Numero);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public List<Categoria> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorCategoriaDespesa = comandoSelecao.ExecuteReader();

            List<Categoria> categorias = new List<Categoria>();

            while (leitorCategoriaDespesa.Read())
            {
                Categoria categoria = ConverterParaCategoriaDespesa(leitorCategoriaDespesa);

                categorias.Add(categoria);
            }

            conexaoComBanco.Close();

            return categorias;
        }

        public Categoria SelecionarPorNumero(int numero)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("NUMERO", numero);

            conexaoComBanco.Open();
            SqlDataReader leitorCategoriaDespesa = comandoSelecao.ExecuteReader();

            Categoria categoria = null;
            if (leitorCategoriaDespesa.Read())
                categoria = ConverterParaCategoriaDespesa(leitorCategoriaDespesa);

            conexaoComBanco.Close();

            return categoria;
        }

        private Categoria ConverterParaCategoriaDespesa(SqlDataReader leitorCategoriaDespesa)
        {
            var numero = Convert.ToInt32(leitorCategoriaDespesa["NUMERO"]);
            var titulo = Convert.ToString(leitorCategoriaDespesa["TITULO"]);
          
            var categoria = new Categoria
            {
                Numero = numero,
                Titulo = titulo
            };

            return categoria;
        }


        private void ConfigurarParametrosCategoriaDespesa(Categoria categoria, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NUMERO", categoria.Numero);
            comando.Parameters.AddWithValue("TITULO", categoria.Titulo);
        }
    }
}