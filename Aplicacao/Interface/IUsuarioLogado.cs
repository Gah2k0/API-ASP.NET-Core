//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;

//namespace Aplicacao.Interface
//{
//    public class UsuarioLogado 
//    {
//		private readonly IHttpContextAccessor _accessor;

//		public UsuarioLogado(IHttpContextAccessor accessor)
//		{
//			_accessor = accessor;
//		}

//		public string Nome => GetClaimsIdentity().FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;

//		public IEnumerable<Claim> GetClaimsIdentity()
//		{
//			return _accessor.HttpContext.User.Claims;
//		}

//	}
//}
