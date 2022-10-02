using UM.DataAccess.Entity.Identity;

namespace UM.ServiceProvider.InternalService.Authentication
{
    public interface IAuthUser
    {
        User? User { get; set; }
    }
}
