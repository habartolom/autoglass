using App.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Classes
{
	public class HeaderContract
	{
		public HttpCodes Code { get; set; }
		public string Message { get; set; }
	}
}
