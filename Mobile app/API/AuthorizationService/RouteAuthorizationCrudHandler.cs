using EncounterAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizationService
{
    public class RouteAuthorizationCrudHandler :
        AuthorizationHandler<OperationAuthorizationRequirement, RouteModel>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
                                                       OperationAuthorizationRequirement requirement, 
                                                       RouteModel resource)
        {
            var userId = long.Parse(context.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value);
            if(userId == resource.CreatorID)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
