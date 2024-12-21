using Core.Entities;
using LetsQuizCore.Entities;

namespace Core.Utilities.Security.JWT;


public interface ITokenHelper
{
    AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
}