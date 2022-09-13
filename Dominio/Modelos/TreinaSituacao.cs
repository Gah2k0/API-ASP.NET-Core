using System;
using System.Collections.Generic;


namespace Dominio.Modelos
{
    public partial class TreinaSituacao
    {
        public TreinaSituacao()
        {
            TreinaProposta = new HashSet<TreinaProposta>();
        }

        public int Id_Treina_Situacao { get; set; }
        public string Situacao { get; set; }
        public string Descricao { get; set; }
        public string Usuario_Atualizacao { get; set; }
        public DateTime Data_Atualizacao { get; set; }

        public virtual ICollection<TreinaProposta> TreinaProposta { get; set; }
    }
}
