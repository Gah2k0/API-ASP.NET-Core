using Aplicacao.Dto.Cliente;


namespace Aplicacao.Dto.Proposta
{
    public class PropostaDto
    {
        public ClienteDto Cliente { get; set; }
        public decimal Proposta { get; set; }
        //public string Cpf { get; set; }
        public string Conveniada { get; set; }
        public decimal? Vlr_Solicitado { get; set; }
        public decimal? Prazo { get; set; }
        public decimal? Vlr_Financiado { get; set; }
        //public string Situacao { get; set; }
        //public DateTime Dt_Situacao { get; set; }
        //public string Usuario { get; set; }
        //public string Usuario_Atualizacao { get; set; }
        //public DateTime Data_Atualizacao { get; set; }

        
    }
}
