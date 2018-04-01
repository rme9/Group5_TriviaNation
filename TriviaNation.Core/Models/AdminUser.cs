﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaNation.Core.Models
{
	public class AdminUser : IUser
	{
		public string Name { get; set; }
		public string Email { get; set; }

		public AdminUser(string name, string email)
		{
			Name = name;
			Email = email;
		}
	}
}