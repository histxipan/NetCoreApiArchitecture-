using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

using WebApiNinjectStudio.Services;
using WebApiNinjectStudio.Dtos;
using WebApiNinjectStudio.Domain.Entities;
using WebApiNinjectStudio.Domain.Abstract;
using WebApiNinjectStudio.Domain.Concrete;

namespace WebApiNinjectStudio.Services
{
    public class AuthPolicyRequirement : IAuthorizationRequirement
    {

    }
}
