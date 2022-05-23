using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Dominio.ModuloContato;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Infra.BancoDados.ModuloCompromisso
{
    public class RepositorioCompromissoEmBancoDados : IRepositorioCompromisso
    {
        private const string enderecoBanco =
              "Data Source=(LocalDB)\\MSSqlLocalDB;" +
              "Initial Catalog=eAgendaDb;" +
              "Integrated Security=True;" +
              "Pooling=False";

        private const string sqlInserir =
            @"INSERT INTO [TBCOMPROMISSO]
                (
                    [LOCAL],       
                    [DATA], 
                    [ASSUNTO],
                    [HORAINICIO],                    
                    [HORATERMINO],                                                           
                    [CONTATO_NUMERO],
                    [LINK]            
                )
            VALUES
                (
                    @LOCAL,
                    @DATA,
                    @ASSUNTO,
                    @HORAINICIO,
                    @HORATERMINO,
                    @CONTATO_NUMERO,
                    @LINK
                ); SELECT SCOPE_IDENTITY();";

        private const string sqlSelecionarTodos =
            @"SELECT 
                CP.[NUMERO],       
                CP.[DATA],
                CP.[ASSUNTO],
                CP.[LOCAL],             
                CP.[HORAINICIO],                    
                CP.[HORATERMINO],                                
                CP.[CONTATO_NUMERO],
                CP.[LINK],
                CT.[NOME],       
                CT.[EMAIL],             
                CT.[TELEFONE],                    
                CT.[CARGO], 
                CT.[EMPRESA] 
            FROM
                [TBCOMPROMISSO] AS CP LEFT JOIN 
                [TBCONTATO] AS CT
            ON
                CT.NUMERO = CP.CONTATO_NUMERO";

        public ValidationResult Inserir(Compromisso novoRegistro)
        {
            var validador = new ValidadorCompromisso();

            var resultadoValidacao = validador.Validate(novoRegistro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosCompromisso(novoRegistro, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            novoRegistro.Numero = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        private void ConfigurarParametrosCompromisso(Compromisso compromisso, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NUMERO", compromisso.Numero);
            comando.Parameters.AddWithValue("ASSUNTO", compromisso.Assunto);
            comando.Parameters.AddWithValue("LOCAL", string.IsNullOrEmpty(compromisso.Local) ? DBNull.Value : compromisso.Local);
            comando.Parameters.AddWithValue("LINK", string.IsNullOrEmpty(compromisso.Link) ? DBNull.Value : compromisso.Link);
            comando.Parameters.AddWithValue("DATA", compromisso.Data);
            comando.Parameters.AddWithValue("HORAINICIO", compromisso.HoraInicio.Ticks);
            comando.Parameters.AddWithValue("HORATERMINO", compromisso.HoraTermino.Ticks);

            comando.Parameters.AddWithValue("CONTATO_NUMERO", compromisso.Contato == null ? DBNull.Value : compromisso.Contato.Numero);
        }

        public ValidationResult Editar(Compromisso registro)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Excluir(Compromisso registro)
        {
            throw new NotImplementedException();
        }       

        public List<Compromisso> SelecionarCompromissosFuturos(DateTime dataInicial, DateTime dataFinal)
        {
            throw new NotImplementedException();
        }

        public List<Compromisso> SelecionarCompromissosPassados(DateTime dataDeHoje)
        {
            throw new NotImplementedException();
        }

        public Compromisso SelecionarPorNumero(int numero)
        {
            throw new NotImplementedException();
        }

        public List<Compromisso> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorCompromisso = comandoSelecao.ExecuteReader();

            List<Compromisso> compromissos = new List<Compromisso>();

            while (leitorCompromisso.Read())
            {
                Compromisso compromisso = ConverterParaCompromisso(leitorCompromisso);

                compromissos.Add(compromisso);
            }

            conexaoComBanco.Close();

            return compromissos;
        }

        private Compromisso ConverterParaCompromisso(SqlDataReader leitorCompromisso)
        {
            var numero = Convert.ToInt32(leitorCompromisso["NUMERO"]);
            var assunto = Convert.ToString(leitorCompromisso["ASSUNTO"]);

            string local = "", link = "";
            TipoLocalizacaoCompromissoEnum tipoLocalizacao = TipoLocalizacaoCompromissoEnum.Presencial;

            if (leitorCompromisso["LOCAL"] != DBNull.Value)
                local = Convert.ToString(leitorCompromisso["LOCAL"]);

            if (leitorCompromisso["LINK"] != DBNull.Value)
            {
                link = Convert.ToString(leitorCompromisso["LINK"]);
                tipoLocalizacao = TipoLocalizacaoCompromissoEnum.Remoto;
            }

            var data = Convert.ToDateTime(leitorCompromisso["DATA"]);
            var horaInicio = TimeSpan.FromTicks(Convert.ToInt64(leitorCompromisso["HORAINICIO"]));
            var horaTermino = TimeSpan.FromTicks(Convert.ToInt64(leitorCompromisso["HORATERMINO"]));

            Compromisso compromisso = new Compromisso();
            compromisso.Numero = numero;
            compromisso.Assunto = assunto;
            compromisso.Local = local;
            compromisso.Link = link;
            compromisso.Data = data;
            compromisso.HoraInicio = horaInicio;
            compromisso.HoraTermino = horaTermino;
            compromisso.TipoLocal = tipoLocalizacao;

            if (leitorCompromisso["CONTATO_NUMERO"] != DBNull.Value)
            {
                var numeroContato = Convert.ToInt32(leitorCompromisso["CONTATO_NUMERO"]);
                var email = Convert.ToString(leitorCompromisso["EMAIL"]);
                var nome = Convert.ToString(leitorCompromisso["NOME"]);
                var telefone = Convert.ToString(leitorCompromisso["TELEFONE"]);
                var empresa = Convert.ToString(leitorCompromisso["EMPRESA"]);
                var cargo = Convert.ToString(leitorCompromisso["CARGO"]);

                compromisso.Contato = new Contato
                {
                    Numero = numeroContato,
                    Nome = nome,
                    Telefone = telefone,
                    Email = email,
                    Cargo = cargo,
                    Empresa = empresa
                };
            }

            return compromisso;
        }
    }
}
