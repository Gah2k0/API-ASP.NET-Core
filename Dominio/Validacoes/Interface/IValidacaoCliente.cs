using Dominio.Modelos;


namespace Dominio.Validacoes.Interface
{
    public interface IValidacaoCliente
    {
        public void ValidarCliente(TreinaCliente cliente);
        public void ValidarNome(string nome);
        public void ValidarCpf(string cpf);
        public void ValidarCep(string cep);
        public void ValidarGenero(string genero);
        public void ValidarSalario(decimal salario);



    }
}
