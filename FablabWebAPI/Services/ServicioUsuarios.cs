using FablabWebAPI.Entities;
using Microsoft.AspNetCore.Identity;

namespace FablabWebAPI.Services
{
    public class ServicioUsuarios : IServicioUsuarios
    {
        private readonly UserManager<Usuario> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ServicioUsuarios(UserManager<Usuario> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<Usuario?> ObtenerUsuario()
        {

            var emailClaim = httpContextAccessor.HttpContext!.User.Claims.Where(x => x.Type == "email").FirstOrDefault();


            if (emailClaim is null)
            {
                return null;
            }

            return await userManager.FindByEmailAsync(emailClaim.Value);
        }



    }
}
