using ProductCatalog.Application.DTOs.Account;

namespace ProductCatalog.Application.Interfaces
{
    public interface ITokenService
    {
        UserToken GenerateToken(string email);
    }
}
