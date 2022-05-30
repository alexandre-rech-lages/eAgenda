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

        private const string sqlSelecionarDespesasDaCategoria =
        @"SELECT 
	                D.[NUMERO], 
		            D.[DESCRICAO], 
		            D.[VALOR],
		            D.[DATA],
		            D.[FORMAPAGAMENTO]

                FROM 
	                TBDESPESA AS D INNER JOIN TBDESPESA_TBCATEGORIA AS DC 
                ON 
	                D.NUMERO = DC.DESPESA_NUMERO
                WHERE 
	                DC.CATEGORIA_NUMERO = @CATEGORIA_NUMERO";


        #endregion

        public ValidationResult Inserir(Categoria novoCategoria)
        {
            var validador = new ValidadorCategoria();

            var resultadoValidacao = validador.Validate(novoCategoria);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosCategoria(novoCategoria, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            novoCategoria.Numero = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Categoria categoria)
        {
            var validador = new ValidadorCategoria();

            var resultadoValidacao = validador.Validate(categoria);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosCategoria(categoria, comandoEdicao);

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
            SqlDataReader leitorCategoria = comandoSelecao.ExecuteReader();

            List<Categoria> categorias = new List<Categoria>();

            while (leitorCategoria.Read())
            {
                Categoria categoria = ConverterParaCategoria(leitorCategoria);

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
            SqlDataReader leitorCategoria = comandoSelecao.ExecuteReader();

            Categoria categoria = null;
            if (leitorCategoria.Read())
                categoria = ConverterParaCategoria(leitorCategoria);

            conexaoComBanco.Close();

            CarregarDespesas(ref categoria);

            return categoria;
        }

        private void CarregarDespesas(ref Categoria categoria)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarDespesasDaCategoria, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("CATEGORIA_NUMERO", categoria.Numero);

            conexaoComBanco.Open();
            SqlDataReader leitorDespesa = comandoSelecao.ExecuteReader();

            while (leitorDespesa.Read())
            {
                Despesa despesa = ConverterParaDespesa(leitorDespesa);

                categoria.RegistrarDespesa(despesa);
            }

            conexaoComBanco.Close();
        }

        private Despesa ConverterParaDespesa(SqlDataReader leitorDespesa)
        {
            var numero = Convert.ToInt32(leitorDespesa["NUMERO"]);
            var descricao = Convert.ToString(leitorDespesa["DESCRICAO"]);
            var valor = Convert.ToDecimal(leitorDespesa["VALOR"]);
            var data = Convert.ToDateTime(leitorDespesa["DATA"]);
            var formaPgto = (FormaPgtoDespesaEnum)leitorDespesa["FORMAPAGAMENTO"];

            var despesa = new Despesa
            {
                Numero = numero,
                Descricao = descricao,
                Valor = valor,
                Data = data,
                FormaPagamento = formaPgto
            };

            return despesa;
        }

        private Categoria ConverterParaCategoria(SqlDataReader leitorCategoria)
        {
            var numero = Convert.ToInt32(leitorCategoria["NUMERO"]);
            var titulo = Convert.ToString(leitorCategoria["TITULO"]);
          
            var categoria = new Categoria
            {
                Numero = numero,
                Titulo = titulo
            };

            return categoria;
        }


        private void ConfigurarParametrosCategoria(Categoria categoria, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NUMERO", categoria.Numero);
            comando.Parameters.AddWithValue("TITULO", categoria.Titulo);
        }
    }
}