using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Mensagem
{
    public class Mensagem : IMensagem
    {
        private List<string> _erros;
        public Mensagem()
        {
            _erros = new List<string>();
        }
        public bool PossuiErros => _erros.Any();
        public void AdicionarErro(string erro)
        {
            _erros.Add(erro);
        }

        public List<string> RetornaErros()
        {
            return _erros;
        }
    }
}
