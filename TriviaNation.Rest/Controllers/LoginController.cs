using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TriviaNation.Core.Models;
using TriviaNation.Rest.Services;

namespace TriviaNation.Rest.Controllers
{
    [Produces("application/json")]
    [Route("[action]")]
    public class LoginController : Controller
    {
	    private readonly LoginService _LoginService;

	    public LoginController()
	    {
		    _LoginService = new LoginService();
	    }

	    [HttpGet("{email}/{password}")]
	    public IUser Login(string email, string password) => _LoginService.Login(email, password);
    }
}