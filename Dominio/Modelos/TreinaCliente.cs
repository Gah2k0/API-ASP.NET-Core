using System;


namespace Dominio.Modelos
{
    public partial class TreinaCliente
    {
        public int IdTreinaCliente { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public DateTime DtNascimento { get; set; }
        public string Genero { get; set; }
        public decimal VlrSalario { get; set; }
        public string Logradouro { get; set; }
        public string NumeroResidencia { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public string UsuarioAtualizacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
