using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;


namespace Aplicacao.Dto
{
	public class UsuarioLogado
	{
		private readonly IHttpContextAccessor _acessor;

        public UsuarioLogado()
        {

        }

        public string Usuario { get; set; }
        public string Token { get; set; }
		
		public IEnumerable<Claim> GetClaimsIdentity()
		{
			return _acessor.HttpContext.User.Claims;
		}
    }
}