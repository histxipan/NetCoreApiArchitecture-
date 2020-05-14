using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApiNinjectStudio.Domain.Abstract;
using WebApiNinjectStudio.Domain.Concrete;
using WebApiNinjectStudio.Domain.Entities;

namespace WebApiNinjectStudio.Services
{
    public class AuthPolicyHandler : AuthorizationHandler<AuthPolicyRequirement>
    {
        private IApiUrlRepository apiUrlRepository;
        private IUserRepository userRepository;


        public AuthPolicyHandler(IApiUrlRepository _apiUrlrepository, IUserRepository _userRepository)
        {
            apiUrlRepository = _apiUrlrepository;
            userRepository = _userRepository;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthPolicyRequirement requirement)
        {
            //context.Fail();
            var isAuthenticated = context.User.Identity.IsAuthenticated;
            var questUrl = "/"+ (context.Resource as Microsoft.AspNetCore.Routing.RouteEndpoint).RoutePattern.RawText;
            var questType = (context.Resource as Microsoft.AspNetCore.Routing.RouteEndpoint).RoutePattern.RequiredValues.Values.First().ToString();
            if (isAuthenticated)
            {
                if ( context.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier) )
                {
                    int userID = int.Parse(
                        context.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value
                        );
                    ApiUrl apiUrl = apiUrlRepository.ApiUrls.FirstOrDefault(a => a.ApiUrlString.ToUpper() == questUrl.ToUpper() && a.ApiRequestMethod.ToUpper() == questType.ToUpper() );
                    User user = userRepository.Users.FirstOrDefault(u => u.UserID == userID);
                    if ( (apiUrl != null) && (user != null) )
                    {
                        int currentApiUrlID = apiUrl.ApiUrlID;
                        int[] allowApiUrlArray = (from s in user.Role.RolePermissionApiUrls
                                           select s.ApiUrlID).ToArray();

                        if ( Array.IndexOf(allowApiUrlArray, currentApiUrlID) != -1 )
                        {
                            context.Succeed(requirement);
                        }
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
