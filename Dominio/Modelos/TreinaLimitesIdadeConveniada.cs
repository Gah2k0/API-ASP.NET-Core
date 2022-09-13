using System;


namespace Dominio.Modelos
{
    public partial class TreinaLimitesIdadeConveniada
    {
        public int Id_Treina_Lim_Idade_Conveniada { get; set; }
        public string Conveniada { get; set; }
        public decimal Idade_Inicial { get; set; }
        public decimal Idade_Final { get; set; }
        public decimal Valor_Limite { get; set; }
        public decimal Percentual_MaximoAnalise { get; set; }
        public string Usuario_Atualizacao { get; set; }
        public DateTime Data_Atualizacao { get; set; }

        public virtual TreinaConveniada ConveniadaNavigation { get; set; }
    }
}
