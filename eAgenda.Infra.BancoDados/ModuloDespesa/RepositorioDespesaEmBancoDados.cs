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
    public class RepositorioDespesaEmBancoDados : IRepositorioDespesa
    {
        private const string enderecoBanco =
               "Data Source=(LocalDB)\\MSSqlLocalDB;" +
               "Initial Catalog=eAgendaDb;" +
               "Integrated Security=True;" +
               "Pooling=False";

        #region Sql Queries
        private const string sqlInserir =
            @"INSERT INTO [TBDESPESA] 
                (
                    [DESCRICAO],
                    [VALOR],
                    [DATA],
                    [FORMAPAGAMENTO]
	            )
	            VALUES
                (
                    @DESCRICAO,
                    @VALOR,
                    @DATA,
                    @FORMAPAGAMENTO

                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
            @"UPDATE [TBDESPESA]	
		        SET
			        [DESCRICAO] = @DESCRICAO,
			        [VALOR] = @VALOR,
			        [DATA] = @DATA,
			        [FORMAPAGAMENTO] = @FORMAPAGAMENTO
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlExcluir =
            @"DELETE FROM [TBDESPESA]
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlSelecionarTodos =
            @"SELECT 
		            [NUMERO], 
		            [DESCRICAO], 
		            [VALOR],
		            [DATA],
		            [FORMAPAGAMENTO]
	            FROM 
		            [TBDESPESA]";

        private const string sqlSelecionarPorNumero =
            @"SELECT 
		            [NUMERO], 
		            [DESCRICAO], 
		            [VALOR],
		            [DATA],
		            [FORMAPAGAMENTO]
	            FROM 
		            [TBDESPESA]
		        WHERE
                    [NUMERO] = @NUMERO";

        private const string sqlSelecionarCategoriasDaDespesa =
            @"SELECT 
	                C.NUMERO, 
	                C.TITULO
                FROM 
	                TBCATEGORIA AS C INNER JOIN TBDESPESA_TBCATEGORIA AS DC 
                ON 
	                C.NUMERO = DC.CATEGORIA_NUMERO
                WHERE 
	                DC.DESPESA_NUMERO = @DESPESA_NUMERO";

        private const string sqlAdicionarCategoriaNaDespesa =
            @"INSERT INTO [TBDESPESA_TBCATEGORIA] 
                (
                    [DESPESA_NUMERO],
                    [CATEGORIA_NUMERO]
	            )
	            VALUES
                (
                    @DESPESA_NUMERO,
                    @CATEGORIA_NUMERO
                )";

        private const string sqlRemoverCategoriaDaDespesa =
            @"DELETE FROM 
                TBDESPESA_TBCATEGORIA 
            WHERE 
	            [CATEGORIA_NUMERO] = @CATEGORIA_NUMERO";

        #endregion

        public ValidationResult Inserir(Despesa despesa)
        {
            var validador = new ValidadorDespesa();

            var resultadoValidacao = validador.Validate(despesa);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosDespesa(despesa, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            despesa.Numero = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Inserir(Despesa despesa, 
            List<Categoria> categoriasMarcadas)
        {
            var resultadoValidacao = Inserir(despesa);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            foreach (var categoria in categoriasMarcadas)
            {
                AdicionarCategoria(despesa, categoria);
            }

            return resultadoValidacao;
        }
       
        public ValidationResult Editar(Despesa despesa)
        {
            var validador = new ValidadorDespesa();

            var resultadoValidacao = validador.Validate(despesa);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosDespesa(despesa, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Despesa despesa, 
            List<Categoria> categoriasMarcadas, List<Categoria> categoriasDesmarcadas)
        {
            var resultadoValidacao = Editar(despesa);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            foreach (var categoria in categoriasDesmarcadas)
            {
                if (despesa.Categorias.Contains(categoria))
                    RemoverCategoria(categoria);    
            }

            foreach (var categoria in categoriasMarcadas)
            {
                AdicionarCategoria(despesa, categoria);
            }

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Despesa despesa)
        {
            foreach (var categoria in despesa.Categorias)
            {
                RemoverCategoria(categoria);
            }

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("NUMERO", despesa.Numero);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

       

        public List<Despesa> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorDespesa = comandoSelecao.ExecuteReader();

            List<Despesa> despesas = new List<Despesa>();

            while (leitorDespesa.Read())
            {
                Despesa despesa = ConverterParaDespesa(leitorDespesa);

                despesas.Add(despesa);
            }

            conexaoComBanco.Close();

            return despesas;
        }

        public Despesa SelecionarPorNumero(int numero)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("NUMERO", numero);

            conexaoComBanco.Open();
            SqlDataReader leitorDespesa = comandoSelecao.ExecuteReader();

            Despesa despesa = null;
            if (leitorDespesa.Read())
                despesa = ConverterParaDespesa(leitorDespesa);

            conexaoComBanco.Close();

            CarregarCategorias(despesa);

            return despesa;
        }

        private void RemoverCategoria(Categoria categoria)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlRemoverCategoriaDaDespesa, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("CATEGORIA_NUMERO", categoria.Numero);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();            
            conexaoComBanco.Close();
        }

        private void AdicionarCategoria(Despesa despesa, Categoria categoria)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlAdicionarCategoriaNaDespesa, conexaoComBanco);

            comandoInsercao.Parameters.AddWithValue("DESPESA_NUMERO", despesa.Numero);
            comandoInsercao.Parameters.AddWithValue("CATEGORIA_NUMERO", categoria.Numero);

            conexaoComBanco.Open();
            comandoInsercao.ExecuteNonQuery();
            conexaoComBanco.Close();
        }

        private void CarregarCategorias(Despesa despesa)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarCategoriasDaDespesa, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("DESPESA_NUMERO", despesa.Numero);

            conexaoComBanco.Open();
            SqlDataReader leitorCategoria = comandoSelecao.ExecuteReader();

            //List<Categoria> categorias = new List<Categoria>();

            while (leitorCategoria.Read())
            {
                var categoria = ConverterParaCategoria(leitorCategoria);

                despesa.Categorias.Add(categoria);
            }

            conexaoComBanco.Close();

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

        private void ConfigurarParametrosDespesa(Despesa despesa, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NUMERO", despesa.Numero);
            comando.Parameters.AddWithValue("DESCRICAO", despesa.Descricao);
            comando.Parameters.AddWithValue("VALOR", despesa.Valor);
            comando.Parameters.AddWithValue("DATA", despesa.Data);
            comando.Parameters.AddWithValue("FORMAPAGAMENTO", despesa.FormaPagamento);
        }
    }
}
