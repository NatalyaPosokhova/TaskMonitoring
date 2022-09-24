﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMonitoring.Users.BL.Exceptions
{
	public class CannotCreateUserException : Exception
	{
		public CannotCreateUserException(string message, Exception ex = null) : base(message, ex)
		{

		}
	}
}
