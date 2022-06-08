using eAgenda.Dominio.ModuloTarefa;
using eAgenda.Infra.BancoDados.ModuloTarefa;
using GeradorTestes.Infra.BancoDados.Compartilhado;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Infra.BancoDados.Tests.ModuloTarefa
{
    [TestClass]
    public class RepositorioTarefaEmBancoDadosTest
    {
        public RepositorioTarefaEmBancoDadosTest()
        {
            string sql =
                @"DELETE FROM TBITEMTAREFA;
                  DBCC CHECKIDENT (TBITEMTAREFA, RESEED, 0)

                  DELETE FROM TBTAREFA;
                  DBCC CHECKIDENT (TBTAREFA, RESEED, 0)";

            Db.ExecutarSql(sql);
        }

        [TestMethod]
        public void Deve_inserir_tarefa()
        {
            //arrange
            Tarefa novaTarefa = new Tarefa("Corrigir provas");

            var repositorio = new RepositorioTarefaEmBancoDados();

            //action
            repositorio.Inserir(novaTarefa);

            //assert
            Tarefa tarefaEncontrada = repositorio.SelecionarPorNumero(novaTarefa.Numero);

            Assert.IsNotNull(tarefaEncontrada);
            Assert.AreEqual(novaTarefa.Numero, tarefaEncontrada.Numero);
            Assert.AreEqual(novaTarefa.Titulo, tarefaEncontrada.Titulo);
            Assert.AreEqual(novaTarefa.Prioridade, tarefaEncontrada.Prioridade);
            Assert.AreEqual(novaTarefa.DataCriacao.Date, tarefaEncontrada.DataCriacao.Date);
            Assert.AreEqual(novaTarefa.DataConclusao, tarefaEncontrada.DataConclusao);
            Assert.AreEqual(novaTarefa.PercentualConcluido, tarefaEncontrada.PercentualConcluido);

            Assert.AreEqual(0, novaTarefa.Itens.Count);
        }

        [TestMethod]
        public void Deve_adicionar_itens_na_tarefa()
        {
            //arrange
            Tarefa novaTarefa = new Tarefa("Corrigir provas");

            var repositorio = new RepositorioTarefaEmBancoDados();

            repositorio.Inserir(novaTarefa);

            var itens = new List<ItemTarefa>
            {
                new ItemTarefa("Item 01"),
                new ItemTarefa("Item 02"),
                new ItemTarefa("Item 03"),
                new ItemTarefa("Item 04")
            };

            //action
            repositorio.AdicionarItens(novaTarefa, itens);

            //assert
            Tarefa tarefaEncontrada = repositorio.SelecionarPorNumero(novaTarefa.Numero);

            Assert.IsNotNull(tarefaEncontrada);
            Assert.AreEqual(novaTarefa.Numero, tarefaEncontrada.Numero);
            Assert.AreEqual(novaTarefa.Titulo, tarefaEncontrada.Titulo);
            Assert.AreEqual(novaTarefa.Prioridade, tarefaEncontrada.Prioridade);
            Assert.AreEqual(novaTarefa.DataCriacao.Date, tarefaEncontrada.DataCriacao.Date);
            Assert.AreEqual(novaTarefa.DataConclusao, tarefaEncontrada.DataConclusao);
            Assert.AreEqual(novaTarefa.PercentualConcluido, tarefaEncontrada.PercentualConcluido);

            Assert.AreEqual(4, tarefaEncontrada.Itens.Count);
        }

        [TestMethod]
        public void Deve_editar_tarefa()
        {
            //arrange
            Tarefa novaTarefa = new Tarefa("Corrigir provas");
            var repositorio = new RepositorioTarefaEmBancoDados();
            repositorio.Inserir(novaTarefa);

            Tarefa tarefaAtualizada = repositorio.SelecionarPorNumero(novaTarefa.Numero);
            tarefaAtualizada.Titulo = "Preparar palestra";
            tarefaAtualizada.Prioridade = PrioridadeTarefaEnum.Alta;

            //action
            repositorio.Editar(tarefaAtualizada);

            //assert
            Tarefa tarefaEncontrada = repositorio.SelecionarPorNumero(novaTarefa.Numero);

            Assert.IsNotNull(tarefaEncontrada);
            Assert.AreEqual(tarefaAtualizada.Numero, tarefaEncontrada.Numero);
            Assert.AreEqual(tarefaAtualizada.Titulo, tarefaEncontrada.Titulo);
            Assert.AreEqual(tarefaAtualizada.Prioridade, tarefaEncontrada.Prioridade);
            Assert.AreEqual(tarefaAtualizada.DataCriacao.Date, tarefaEncontrada.DataCriacao.Date);
            Assert.AreEqual(tarefaAtualizada.DataConclusao, tarefaEncontrada.DataConclusao);
            Assert.AreEqual(tarefaAtualizada.PercentualConcluido, tarefaEncontrada.PercentualConcluido);
        }

        [TestMethod]
        public void Deve_atualizar_itens_na_tarefa()
        {
            //arrange
            Tarefa novaTarefa = new Tarefa("Corrigir provas");
            var repositorio = new RepositorioTarefaEmBancoDados();

            repositorio.Inserir(novaTarefa);

            var itens = new List<ItemTarefa>
            {
                new ItemTarefa("Item 01"),
                new ItemTarefa("Item 02"),
                new ItemTarefa("Item 03"),
                new ItemTarefa("Item 04")
            };

            repositorio.AdicionarItens(novaTarefa, itens);

            Tarefa tarefaAtualizada = repositorio.SelecionarPorNumero(novaTarefa.Numero);
            tarefaAtualizada.Titulo = "Preparar palestra";
            tarefaAtualizada.Prioridade = PrioridadeTarefaEnum.Alta;

            var itensConcluidos = new List<ItemTarefa>();
            itensConcluidos.Add(tarefaAtualizada.Itens[0]);
            itensConcluidos.Add(tarefaAtualizada.Itens[1]);

            var itensPendentes = new List<ItemTarefa>();
            itensPendentes.Add(tarefaAtualizada.Itens[2]);
            itensPendentes.Add(tarefaAtualizada.Itens[3]);

            //action
            repositorio.AtualizarItens(tarefaAtualizada, itensConcluidos, itensPendentes);

            //assert
            Tarefa tarefaEncontrada = repositorio.SelecionarPorNumero(novaTarefa.Numero);

            Assert.IsNotNull(tarefaEncontrada);
            Assert.AreEqual(tarefaAtualizada.Numero, tarefaEncontrada.Numero);
            Assert.AreEqual(tarefaAtualizada.Titulo, tarefaEncontrada.Titulo);
            Assert.AreEqual(tarefaAtualizada.Prioridade, tarefaEncontrada.Prioridade);
            Assert.AreEqual(tarefaAtualizada.DataCriacao.Date, tarefaEncontrada.DataCriacao.Date);
            Assert.AreEqual(tarefaAtualizada.DataConclusao, tarefaEncontrada.DataConclusao);
            Assert.AreEqual(tarefaAtualizada.PercentualConcluido, tarefaEncontrada.PercentualConcluido);
        }

        [TestMethod]
        public void Deve_excluir_tarefa_com_itens()
        {
            //arrange
            Tarefa novaTarefa = new Tarefa("Corrigir provas");

            var repositorio = new RepositorioTarefaEmBancoDados();

            repositorio.Inserir(novaTarefa);

            var itens = new List<ItemTarefa>
            {
                new ItemTarefa("Item 01"),
                new ItemTarefa("Item 02"),
                new ItemTarefa("Item 03"),
                new ItemTarefa("Item 04")
            };

            repositorio.AdicionarItens(novaTarefa, itens);

            //action
            repositorio.Excluir(novaTarefa);

            //assert
            Tarefa tarefaEncontrada = repositorio.SelecionarPorNumero(novaTarefa.Numero);

            Assert.IsNull(tarefaEncontrada);
        }

        [TestMethod]
        public void Deve_selecionar_uma_tarefa_e_seus_itens()
        {
            //arrange
            Tarefa novaTarefa = new Tarefa("Corrigir provas");

            var repositorio = new RepositorioTarefaEmBancoDados();

            repositorio.Inserir(novaTarefa);

            var itens = new List<ItemTarefa>
            {
                new ItemTarefa("Item 01"),
                new ItemTarefa("Item 02"),
                new ItemTarefa("Item 03"),
                new ItemTarefa("Item 04")
            };

            repositorio.AdicionarItens(novaTarefa, itens);

            //action
            Tarefa tarefaEncontrada = repositorio.SelecionarPorNumero(novaTarefa.Numero);

            //assert
            Assert.IsNotNull(tarefaEncontrada);
            Assert.AreEqual(novaTarefa.Numero, tarefaEncontrada.Numero);
            Assert.AreEqual(novaTarefa.Titulo, tarefaEncontrada.Titulo);
            Assert.AreEqual(novaTarefa.Prioridade, tarefaEncontrada.Prioridade);
            Assert.AreEqual(novaTarefa.DataCriacao.Date, tarefaEncontrada.DataCriacao.Date);
            Assert.AreEqual(novaTarefa.DataConclusao, tarefaEncontrada.DataConclusao);
            Assert.AreEqual(novaTarefa.PercentualConcluido, tarefaEncontrada.PercentualConcluido);

            Assert.AreEqual(4, tarefaEncontrada.Itens.Count);
        }

        [TestMethod]
        public void Deve_selecionar_todas_tarefas()
        {
            //arrange
            var repositorio = new RepositorioTarefaEmBancoDados();

            Tarefa t1 = new Tarefa("Preparar aula");            
            repositorio.Inserir(t1);

            Tarefa t2 = new Tarefa("Corrigir Provas");
            repositorio.Inserir(t2);

            Tarefa t3 = new Tarefa("Implementar Atividades");
            repositorio.Inserir(t3);

            //action
            var tarefas = repositorio.SelecionarTodos();

            //assert
            Assert.AreEqual(3, tarefas.Count);  

            Assert.AreEqual("Preparar aula", tarefas[0].Titulo);
            Assert.AreEqual("Corrigir Provas", tarefas[1].Titulo);
            Assert.AreEqual("Implementar Atividades", tarefas[2].Titulo);
        }

        [TestMethod]
        public void Deve_selecionar_apenas_tarefas_pendentes()
        {
            //arrange
            var repositorio = new RepositorioTarefaEmBancoDados();

            Tarefa t1 = new Tarefa("Preparar aula");
            repositorio.Inserir(t1);

            Tarefa t2 = new Tarefa("Corrigir Provas");
            repositorio.Inserir(t2);

            Tarefa t3 = new Tarefa("Implementar Atividades");
            repositorio.Inserir(t3);

            var itens = new List<ItemTarefa>
            {
                new ItemTarefa("Item 01"),
                new ItemTarefa("Item 02"),
                new ItemTarefa("Item 03"),
                new ItemTarefa("Item 04")
            };

            repositorio.AdicionarItens(t3, itens);

            var itensConcluidos = new List<ItemTarefa>();
            itensConcluidos.Add(t3.Itens[0]);
            itensConcluidos.Add(t3.Itens[1]);
            itensConcluidos.Add(t3.Itens[2]);
            itensConcluidos.Add(t3.Itens[3]);

            repositorio.AtualizarItens(t3, itensConcluidos, new List<ItemTarefa>());

            //action
            var tarefas = repositorio.SelecionarTodos(StatusTarefaEnum.Pendentes);

            //assert
            Assert.AreEqual(2, tarefas.Count);

            Assert.AreEqual("Preparar aula", tarefas[0].Titulo);
            Assert.AreEqual("Corrigir Provas", tarefas[1].Titulo);
        }

        [TestMethod]
        public void Deve_selecionar_apenas_tarefas_concluidas()
        {
            //arrange
            var repositorio = new RepositorioTarefaEmBancoDados();

            Tarefa t1 = new Tarefa("Preparar aula");
            repositorio.Inserir(t1);

            Tarefa t2 = new Tarefa("Corrigir Provas");
            repositorio.Inserir(t2);

            Tarefa t3 = new Tarefa("Implementar Atividades");
            repositorio.Inserir(t3);

            var itens = new List<ItemTarefa>
            {
                new ItemTarefa("Item 01"),
                new ItemTarefa("Item 02"),
                new ItemTarefa("Item 03"),
                new ItemTarefa("Item 04")
            };

            repositorio.AdicionarItens(t3, itens);

            var itensConcluidos = new List<ItemTarefa>();
            itensConcluidos.Add(t3.Itens[0]);
            itensConcluidos.Add(t3.Itens[1]);
            itensConcluidos.Add(t3.Itens[2]);
            itensConcluidos.Add(t3.Itens[3]);

            repositorio.AtualizarItens(t3, itensConcluidos, new List<ItemTarefa>());

            //action
            var tarefas = repositorio.SelecionarTodos(StatusTarefaEnum.Concluidas);

            //assert
            Assert.AreEqual(1, tarefas.Count);

            Assert.AreEqual("Implementar Atividades", tarefas[0].Titulo);
        }

    }
}