using Dominio.Modelos;


namespace Dominio.Validacoes.Interface
{
    public interface IValidacaoProposta
    {
        public void ValidarProposta(TreinaProposta proposta);
        public void ValidarNumeroProposta(decimal numero);
        public void ValidarValor(decimal valorSolicitado, decimal valorFinanciado);


    }
}
