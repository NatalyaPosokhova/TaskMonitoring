﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMonitoring.APIClients
{
	public class WebProxyException : Exception
	{
		public WebProxyException(Exception ex = null) : base(null, ex)
		{

		}
	}
}
