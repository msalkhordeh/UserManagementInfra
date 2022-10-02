using UM.DataAccess.Entity.Identity;

namespace UM.ServiceProvider.InternalService.Authentication
{
    public class AuthUser : IAuthUser
    {
        public User? User { get; set; } 
    }
}
