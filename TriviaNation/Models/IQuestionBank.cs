﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaNation.Models
{
	public interface IQuestionBank
	{
		string Name { get; set; }

		List<IQuestion> Questions { get; set; }

		string UniqueId { get; }
	}
}