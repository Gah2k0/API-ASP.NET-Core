using Dominio.Mensagem;
using System.Collections.Generic;


namespace Dominio.RetornoApi
{
    public class RetornoApi<T>
    {
        public T Retorno { get; set; }
        public IEnumerable<string> Erros { get; private set; }
        public bool TemErros { get; }

        public RetornoApi(T retorno, IMensagem mensagem)
        {
            Retorno = retorno;
            Erros = mensagem.RetornaErros();
            TemErros = mensagem.PossuiErros;
        }
    }
}
