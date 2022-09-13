using Dominio.Fila;
using Dominio.Interface.Provedor;
using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infraestrutura.Fila
{
    public class MassTransitProvedor : IFilaProvedor
    {

        private readonly IBusControl _busControl;

        public MassTransitProvedor(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public async Task GravarMensagemPropostaFila(MensagemPropostaFila mensagem)
        {
            var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            await _busControl.Publish(mensagem, cancellationToken.Token);
        }
    }
}
