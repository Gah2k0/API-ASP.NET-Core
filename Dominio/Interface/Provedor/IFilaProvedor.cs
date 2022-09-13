using Dominio.Fila;
using System.Threading.Tasks;

namespace Dominio.Interface.Provedor
{
    public interface IFilaProvedor
    {
        Task GravarMensagemPropostaFila(MensagemPropostaFila mensagem);
    }
}
