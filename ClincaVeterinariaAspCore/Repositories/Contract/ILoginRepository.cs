using ClincaVeterinariaAspCore.Models;

namespace ClincaVeterinariaAspCore.Repositories.Contract
{
    public interface ILoginRepository
    {
        void TestarUsuario(Login user);
    }
}
