using Dominio.Mensagem;
using Dominio.Modelos;
using Dominio.Validacoes.Interface;
using System.Text.RegularExpressions;

namespace Dominio.Validacoes
{
    public class ValidacaoProposta : IValidacaoProposta
    {
        private readonly IMensagem _mensagem;

        public ValidacaoProposta(IMensagem mensagem)
        {
            _mensagem = mensagem;
        }

        public void ValidarProposta(TreinaProposta proposta)
        {
            ValidarNumeroProposta(proposta.Proposta);
            ValidarValor(proposta.Vlr_Solicitado, proposta.Vlr_Financiado);

        }

        public void ValidarNumeroProposta(decimal numero)
        {
            string propostaModelo = "[0-9]";
            if (numero < 0)
                _mensagem.AdicionarErro("Número da proposta deve ser maior do que 0");

        }

        public void ValidarValor(decimal valorSolicitado, decimal valorFinanciado)
        {
            if (valorFinanciado < 0 || valorSolicitado < 0)
                _mensagem.AdicionarErro("Valor financiado e valor solicitado da proposta devem ser maiores do que 0");
            
        }

    }
}
