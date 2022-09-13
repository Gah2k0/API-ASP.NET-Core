using Aplicacao.Dto.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BemPreparadosTestes.Mocks
{
    public static class UsuarioRepositorioInfraestruturaMock
    {
        public static Autenticado ObterMock() =>
            new()
            {
                Nome = "Paula"
            };

        public static IEnumerable<Autenticado> ObterListaMock() =>
            new List<Autenticado>() { ObterMock() };

    }
}
