using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace eAgenda.Dominio.ModuloTarefa.Tests
{
    [TestClass]
    public class TarefaTest
    {
        [TestMethod]
        public void Deve_adicionar_itens_na_tarefa()
        {
            //arrange
            var t = new Tarefa();

            var item01 = new ItemTarefa("Item 01");
            var item02 = new ItemTarefa("Item 02");
            var item03 = new ItemTarefa("Item 03");
            var item04 = new ItemTarefa("Item 04");

            //action
            t.AdicionarItem(item01);
            t.AdicionarItem(item02);
            t.AdicionarItem(item03);
            t.AdicionarItem(item04);

            //assert
            Assert.AreEqual(4, t.Itens.Count);
        }

        [TestMethod]
        public void Nao_deve_adicionar_itens_duplicados_na_tarefa()
        {
            //arrange
            var t = new Tarefa();

            var item01 = new ItemTarefa("Item 01");
            var item02 = new ItemTarefa("Item 01");
            var item03 = new ItemTarefa("Item 01");
            var item04 = new ItemTarefa("Item 04");

            //action
            t.AdicionarItem(item01);
            t.AdicionarItem(item02);
            t.AdicionarItem(item03);
            t.AdicionarItem(item04);

            //assert
            Assert.AreEqual(2, t.Itens.Count);
        }

        [TestMethod]
        public void Deve_concluir_itens_da_tarefa()
        {
            //arrange
            var t = new Tarefa();

            var item01 = new ItemTarefa("Item 01");
            var item02 = new ItemTarefa("Item 02");
            var item03 = new ItemTarefa("Item 03");
            var item04 = new ItemTarefa("Item 04");

            t.AdicionarItem(item01);
            t.AdicionarItem(item02);
            t.AdicionarItem(item03);
            t.AdicionarItem(item04);

            //action
            t.ConcluirItem(item01);
            t.ConcluirItem(item02);
            t.ConcluirItem(item03);
            t.ConcluirItem(item04);

            //assert
            Assert.AreEqual(true, item01.Concluido);
            Assert.AreEqual(true, item02.Concluido);
            Assert.AreEqual(true, item03.Concluido);
            Assert.AreEqual(true, item04.Concluido);

            Assert.AreEqual(DateTime.Now.Date, t.DataConclusao);
        }

        [TestMethod]
        public void Deve_calcular_percentual_concluido_da_tarefa()
        {
            //arrange
            var t = new Tarefa();

            var item01 = new ItemTarefa("Item 01");
            var item02 = new ItemTarefa("Item 02");
            var item03 = new ItemTarefa("Item 03");
            var item04 = new ItemTarefa("Item 04");

            t.AdicionarItem(item01);
            t.AdicionarItem(item02);
            t.AdicionarItem(item03);
            t.AdicionarItem(item04);

            t.ConcluirItem(item01);
            t.ConcluirItem(item02);
            t.ConcluirItem(item03);

            //action
            t.CalcularPercentualConcluido();

            //assert
            Assert.AreEqual(75, t.PercentualConcluido);
        }

        [TestMethod]
        public void Deve_marcar_item_da_tarefa_como_pendente()
        {
            //arrange
            var t = new Tarefa();

            var item01 = new ItemTarefa("Item 01");           

            t.AdicionarItem(item01);
            t.ConcluirItem(item01);

            //action
            t.MarcarPendente(item01);

            //assert
            Assert.AreEqual(false, item01.Concluido);
        }

        [TestMethod]
        public void Nao_deve_mostrar_data_conclusao_da_tarefa()
        {
            //arrange
            var t = new Tarefa();
            t.Numero = 1;
            t.Titulo = "Tarefa 01";
            t.Prioridade = PrioridadeTarefaEnum.Alta;

            var item01 = new ItemTarefa("Item 01");
            var item02 = new ItemTarefa("Item 02");           

            t.AdicionarItem(item01);
            t.AdicionarItem(item02);

            t.ConcluirItem(item01);

            //action
            var resultado = t.ToString();

            //assert
            var resultadoEsperado =
                "Número: 1, Título: Tarefa 01, Percentual: 0, Prioridade: Alta";

            Assert.AreEqual(resultadoEsperado, resultado);
        }

        [TestMethod]
        public void Deve_mostrar_data_conclusao_da_tarefa()
        {
            //arrange
            var t = new Tarefa();
            t.Numero = 1;
            t.Titulo = "Tarefa 01";
            t.Prioridade = PrioridadeTarefaEnum.Alta;

            var item01 = new ItemTarefa("Item 01");
            var item02 = new ItemTarefa("Item 02");

            t.AdicionarItem(item01);
            t.AdicionarItem(item02);

            t.ConcluirItem(item01);
            t.ConcluirItem(item02);

            //action
            var resultado = t.ToString();

            //assert
            var resultadoEsperado =
                $"Número: 1, Título: Tarefa 01, Percentual: 0, Prioridade: Alta, Concluída: {DateTime.Now.ToShortDateString()}";

            Assert.AreEqual(resultadoEsperado, resultado);
        }
    }
}
