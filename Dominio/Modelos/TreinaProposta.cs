using System;


namespace Dominio.Modelos
{
    public class TreinaProposta 
    {
        public int Id_Treina_Proposta { get; set; }
        public decimal Proposta { get; set; }
        public string Cpf { get; set; }
        public string Conveniada { get; set; }
        public decimal Vlr_Solicitado { get; set; }
        public decimal? Prazo { get; set; }
        public decimal Vlr_Financiado { get; set; }
        public string Situacao { get; set; }
        public DateTime Dt_Situacao { get; set; }
        public string Usuario { get; set; }
        public string Usuario_Atualizacao { get; set; }
        public DateTime Data_Atualizacao { get; set; }


        public override bool Equals(object o)
        {
            if (o is TreinaProposta)
            {
                TreinaProposta prop = (TreinaProposta)o;
                if (this.Proposta == prop.Proposta 
                    && this.Cpf == prop.Cpf 
                    && this.Conveniada == prop.Conveniada
                    && this.Vlr_Solicitado == prop.Vlr_Solicitado 
                    && this.Prazo == prop.Prazo
                    && this.Vlr_Financiado == prop.Vlr_Financiado
                    && this.Situacao == prop.Situacao
                    && this.Dt_Situacao == prop.Dt_Situacao
                    && this.Usuario == prop.Usuario
                    && this.Usuario_Atualizacao == prop.Usuario_Atualizacao
                    && this.Data_Atualizacao == prop.Data_Atualizacao)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //public virtual TreinaConveniada ConveniadaNavigation { get; set; }
        //public virtual TreinaSituacao SituacaoNavigation { get; set; }
        //public virtual TreinaUsuario UsuarioNavigation { get; set; }
    }
}
