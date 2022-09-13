using Dominio.Mensagem;
using Dominio.Modelos;
using Dominio.Validacoes.Interface;
using System.Text.RegularExpressions;


namespace Dominio.Validacoes
{
    public class ValidacaoCliente : IValidacaoCliente
    {
        private readonly IMensagem _mensagem;

        public ValidacaoCliente(IMensagem mensagem)
        {
            _mensagem = mensagem;
        }

        public void ValidarCliente(TreinaCliente cliente)
        {
            ValidarCpf(cliente.Cpf);
            ValidarNome(cliente.Nome);
            ValidarGenero(cliente.Genero);
            ValidarSalario(cliente.VlrSalario);
            ValidarCep(cliente.Cep);
        }

        public void ValidarNome(string nome)
        {
            string nomeModelo = "[a-zA-Z]";
            if (!Regex.IsMatch(nome, nomeModelo))
                _mensagem.AdicionarErro("Nome Inválido");

            foreach (var c in nome)
            {
                if (c >= '0' && c <= '9')
                    _mensagem.AdicionarErro("Nome Inválido");
                
                if(Regex.IsMatch(nome, ("[@_!#$%^&*()<>?/|}{~:]")))
                    _mensagem.AdicionarErro("Nome Inválido");

            }

        }

        public void ValidarCpf(string cpf)
        {
            string cpfModelo = "^[0-9]{11}$";
            if (!Regex.IsMatch(cpf, cpfModelo))
                _mensagem.AdicionarErro("CPF Inválido");

        }

        public void ValidarCep(string cep)
        {
            string cepModelo = "[0-9]{8}";
            if (!Regex.IsMatch(cep, cepModelo))
                _mensagem.AdicionarErro("CEP Inválido");

        }

        public void ValidarGenero(string genero)
        {
            string generoModelo = "[MF]";
            if (!Regex.IsMatch(genero, generoModelo))
                _mensagem.AdicionarErro("Gênero Inválido");

        }

        public void ValidarSalario(decimal salario)
        {
            if (salario < 0)
                _mensagem.AdicionarErro("Salário não pode ser negativo");

        }


    }
}
