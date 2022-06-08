using eAgenda.Dominio.ModuloTarefa;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace eAgenda.Infra.BancoDados.ModuloTarefa
{
    public class RepositorioTarefaEmBancoDados : IRepositorioTarefa
    {
        private const string enderecoBanco =
             "Data Source=(LocalDB)\\MSSqlLocalDB;" +
             "Initial Catalog=eAgendaDb;" +
             "Integrated Security=True;" +
             "Pooling=False";

        #region SQL Queries
        private const string sqlInserir =
           @"INSERT INTO [TBTAREFA] 
                (
                    [TITULO],
                    [DATACRIACAO],
                    [DATACONCLUSAO],
                    [PRIORIDADE],
                    [PERCENTUALCONCLUIDO]
	            )
	            VALUES
                (
                    @TITULO,
                    @DATACRIACAO,
                    @DATACONCLUSAO,
                    @PRIORIDADE,
                    @PERCENTUALCONCLUIDO
                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
            @"UPDATE [TBTAREFA]	
		        SET
			        [TITULO] = @TITULO,
			        [DATACRIACAO] = @DATACRIACAO,
			        [DATACONCLUSAO] = @DATACONCLUSAO,
			        [PRIORIDADE] = @PRIORIDADE,
                    [PERCENTUALCONCLUIDO] = @PERCENTUALCONCLUIDO
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlExcluir =
           @"DELETE FROM [TBTAREFA]
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlSelecionarPorNumero =
           @"SELECT
	                [NUMERO],
                    [TITULO],
                    [PRIORIDADE],
                    [DATACRIACAO],
                    [DATACONCLUSAO],
                    [PERCENTUALCONCLUIDO]
              FROM 
	                [TBTAREFA]
              WHERE 
	                [NUMERO] = @NUMERO";

        private const string sqlSelecionarTodos =
          @"SELECT
	                [NUMERO],
                    [TITULO],
                    [PRIORIDADE],
                    [DATACRIACAO],
                    [DATACONCLUSAO],
                    [PERCENTUALCONCLUIDO]
              FROM 
	                [TBTAREFA]";

        private const string sqlSelecionarPendentes =
          @"SELECT
	                [NUMERO],
                    [TITULO],
                    [PRIORIDADE],
                    [DATACRIACAO],
                    [DATACONCLUSAO],
                    [PERCENTUALCONCLUIDO]
              FROM 
	                [TBTAREFA]
              WHERE 
                    [PERCENTUALCONCLUIDO] < 100";

        private const string sqlSelecionarConcluidas =
           @"SELECT
	                [NUMERO],
                    [TITULO],
                    [PRIORIDADE],
                    [DATACRIACAO],
                    [DATACONCLUSAO],
                    [PERCENTUALCONCLUIDO]
              FROM 
	                [TBTAREFA]
              WHERE 
                    [PERCENTUALCONCLUIDO] = 100";

        private const string sqlSelecionarItensTarefa =
            @"SELECT 
	            [NUMERO],
                [TITULO],
                [CONCLUIDO],
                [TAREFA_NUMERO]
              FROM 
	            [TBITEMTAREFA]
              WHERE 
	            [TAREFA_NUMERO] = @TAREFA_NUMERO";

        private const string sqlInserirItensTarefa =
            @"INSERT INTO [DBO].[TBITEMTAREFA]
                (
		            [TITULO],
		            [CONCLUIDO],
		            [TAREFA_NUMERO]
	            )
                 VALUES
                (
		            @TITULO,
		            @CONCLUIDO,
		            @TAREFA_NUMERO		   
	            ); SELECT SCOPE_IDENTITY();";

        private const string sqlEditarItemTarefa =
           @"UPDATE [TBITEMTAREFA]	
		        SET
			        [TITULO] = @TITULO,
			        [CONCLUIDO] = @CONCLUIDO
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlExcluirItensTarefa =
            @"DELETE FROM [TBITEMTAREFA]
		        WHERE
			        [TAREFA_NUMERO] = @TAREFA_NUMERO";

        #endregion

        public ValidationResult Inserir(Tarefa novaTarefa)
        {
            var validador = new ValidadorTarefa();

            var resultadoValidacao = validador.Validate(novaTarefa);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosTarefa(novaTarefa, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            novaTarefa.Numero = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Tarefa tarefa)
        {
            var validador = new ValidadorTarefa();

            var resultadoValidacao = validador.Validate(tarefa);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosTarefa(tarefa, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Tarefa tarefa)
        {
            ExcluirItensTarefa(tarefa);

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("NUMERO", tarefa.Numero);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public Tarefa SelecionarPorNumero(int numero)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("NUMERO", numero);

            conexaoComBanco.Open();
            SqlDataReader leitorTarefa = comandoSelecao.ExecuteReader();

            Tarefa tarefa = null;

            if (leitorTarefa.Read())
                tarefa = ConverterParaTarefa(leitorTarefa);

            conexaoComBanco.Close();

            if (tarefa != null)
                CarregarItensTarefa(tarefa);

            return tarefa;
        }

        public List<Tarefa> SelecionarTodos(StatusTarefaEnum status)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            string sql;

            switch (status)
            {
                case StatusTarefaEnum.Pendentes: sql = sqlSelecionarPendentes; break;

                case StatusTarefaEnum.Concluidas: sql = sqlSelecionarConcluidas; break;

                default: sql = sqlSelecionarTodos; break;
            }

            SqlCommand comandoSelecao = new SqlCommand(sql, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorTarefa = comandoSelecao.ExecuteReader();

            List<Tarefa> tarefas = new List<Tarefa>();

            while (leitorTarefa.Read())
            {
                Tarefa tarefa = ConverterParaTarefa(leitorTarefa);

                tarefas.Add(tarefa);
            }

            conexaoComBanco.Close();

            return tarefas;
        }

        public List<Tarefa> SelecionarTodos()
        {
            return SelecionarTodos(StatusTarefaEnum.Todos);
        }

        public void AtualizarItens(Tarefa tarefaSelecionada, List<ItemTarefa> itensConcluidos, List<ItemTarefa> itensPendentes)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);
            conexaoComBanco.Open();

            foreach (var item in itensConcluidos)
            {
                tarefaSelecionada.ConcluirItem(item);

                SqlCommand comandoEdicao = new SqlCommand(sqlEditarItemTarefa, conexaoComBanco);

                ConfigurarParametrosItemTarefa(item, comandoEdicao);

                comandoEdicao.ExecuteNonQuery();
            }

            foreach (var item in itensPendentes)
            {
                tarefaSelecionada.MarcarPendente(item);

                SqlCommand comandoEdicao = new SqlCommand(sqlEditarItemTarefa, conexaoComBanco);

                ConfigurarParametrosItemTarefa(item, comandoEdicao);

                comandoEdicao.ExecuteNonQuery();
            }

            conexaoComBanco.Close();

            tarefaSelecionada.CalcularPercentualConcluido();

            Editar(tarefaSelecionada);
        }

        public void AdicionarItens(Tarefa tarefaSelecionada, List<ItemTarefa> itens)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);
            conexaoComBanco.Open();

            foreach (var item in itens)
            {
                bool itemAdicionado = tarefaSelecionada.AdicionarItem(item);

                if (itemAdicionado)
                {
                    SqlCommand comandoInsercao = new SqlCommand(sqlInserirItensTarefa, conexaoComBanco);

                    ConfigurarParametrosItemTarefa(item, comandoInsercao);
                    var id = comandoInsercao.ExecuteScalar();
                    item.Numero = Convert.ToInt32(id);
                }
            }

            conexaoComBanco.Close();

            tarefaSelecionada.CalcularPercentualConcluido();

            Editar(tarefaSelecionada);
        }

        #region Métodos Privados
        private void ConfigurarParametrosItemTarefa(ItemTarefa itemTarefa, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NUMERO", itemTarefa.Numero);
            comando.Parameters.AddWithValue("TITULO", itemTarefa.Titulo);
            comando.Parameters.AddWithValue("CONCLUIDO", itemTarefa.Concluido);
            comando.Parameters.AddWithValue("TAREFA_NUMERO", itemTarefa.Tarefa.Numero);
        }

        private void ConfigurarParametrosTarefa(Tarefa tarefa, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NUMERO", tarefa.Numero);
            comando.Parameters.AddWithValue("TITULO", tarefa.Titulo);
            comando.Parameters.AddWithValue("PRIORIDADE", tarefa.Prioridade);
            comando.Parameters.AddWithValue("DATACRIACAO", tarefa.DataCriacao);
            comando.Parameters.AddWithValue("DATACONCLUSAO", tarefa.DataConclusao != null ? tarefa.DataConclusao : DBNull.Value);
            comando.Parameters.AddWithValue("PERCENTUALCONCLUIDO", tarefa.PercentualConcluido);
        }

        private void CarregarItensTarefa(Tarefa tarefa)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarItensTarefa, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("TAREFA_NUMERO", tarefa.Numero);

            conexaoComBanco.Open();
            SqlDataReader leitorItemTarefa = comandoSelecao.ExecuteReader();

            //List<ItemTarefa> itensTarefa = new List<ItemTarefa>();

            while (leitorItemTarefa.Read())
            {
                ItemTarefa itemTarefa = ConverterParaItemTarefa(leitorItemTarefa);

                tarefa.AdicionarItem(itemTarefa);
                //itensTarefa.Add(itemTarefa);
            }

            conexaoComBanco.Close();
        }

        private void ExcluirItensTarefa(Tarefa tarefa)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluirItensTarefa, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("TAREFA_NUMERO", tarefa.Numero);

            conexaoComBanco.Open();
            comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();
        }

        private Tarefa ConverterParaTarefa(SqlDataReader leitorTarefa)
        {
            var numero = Convert.ToInt32(leitorTarefa["NUMERO"]);
            var titulo = Convert.ToString(leitorTarefa["TITULO"]);
            var prioridade = (PrioridadeTarefaEnum)leitorTarefa["PRIORIDADE"];
            var dataCriacao = Convert.ToDateTime(leitorTarefa["DATACRIACAO"]);

            DateTime? dataConclusao = null;

            if (leitorTarefa["DATACONCLUSAO"] != DBNull.Value)
                dataConclusao = Convert.ToDateTime(leitorTarefa["DATACONCLUSAO"]);

            var percentual = Convert.ToDecimal(leitorTarefa["PERCENTUALCONCLUIDO"]);

            var tarefa = new Tarefa
            {
                Numero = numero,
                Titulo = titulo,
                DataCriacao = dataCriacao,
                DataConclusao = dataConclusao,
                Prioridade = prioridade,
                PercentualConcluido = percentual
            };

            return tarefa;
        }

        private ItemTarefa ConverterParaItemTarefa(SqlDataReader leitorItemTarefa)
        {
            var numero = Convert.ToInt32(leitorItemTarefa["NUMERO"]);
            var titulo = Convert.ToString(leitorItemTarefa["TITULO"]);
            var concluido = Convert.ToBoolean(leitorItemTarefa["CONCLUIDO"]);

            var itemTarefa = new ItemTarefa
            {
                Numero = numero,
                Titulo = titulo,
                Concluido = concluido
            };

            return itemTarefa;
        }

        #endregion
    }
}
