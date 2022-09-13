

using System;

namespace Dominio.Modelos
{
    public class TreinaUsuario
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string UsuarioAtualizacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public DateTime ValidadeSenha { get; set; }
    }
}
