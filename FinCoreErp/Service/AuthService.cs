using System.Threading.Tasks;
using FinCoreErp.Repository;

namespace FinCoreErp.Service
{
    public class AuthService : IAuthService
    {
        public Task Login() => Task.CompletedTask;
    }
}
