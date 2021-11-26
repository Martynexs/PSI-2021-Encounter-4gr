using EncounterAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizationService
{
    public class UserAuthorizationHandler :
        AuthorizationHandler<SameUserRequirement, UserModel>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
                                                       SameUserRequirement requirement, 
                                                       UserModel resource)
        {
            var username = context.User.Claims.Where(c => c.Type == ClaimTypes.Name).First().Value;
            if (username == resource.Username)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class SameUserRequirement : IAuthorizationRequirement { }
}
