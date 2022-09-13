using System;
using System.Collections.Generic;


namespace Dominio.Modelos
{
    public partial class TreinaConveniada
    {
        public TreinaConveniada()
        {
            TreinaProposta = new HashSet<TreinaProposta>();
        }

        public int IdTreinaConveniada { get; set; }
        public string Conveniada { get; set; }
        public string Descricao { get; set; }
        public string UsuarioAtualizacao { get; set; }
        public DateTime DataAtualizacao { get; set; }

        public virtual ICollection<TreinaProposta> TreinaProposta { get; set; }
    }
}
