using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class ConsultaTreinaProposta
    {
        public long Proposta { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public double VlrFinanciado { get; set; }
        public int Prazo { get; set; }
        public string Situacao { get; set; }
    }
}
