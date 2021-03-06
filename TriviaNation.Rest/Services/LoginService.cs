﻿using TriviaNation.Core.Models;

namespace TriviaNation.Rest.Services
{
    public class LoginService
    {
	    public IUser Login(string email, string password)
	    {
		    using (var serv = new AdminService())
		    {
			    var user = serv.GetUserByEmail(email);

			    if (user != null && user.Password == password)
			    {
				    return user;
			    }

				return new StudentUser(null, null);
		    }
	    }
    }
}
