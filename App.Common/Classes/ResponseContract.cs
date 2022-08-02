using App.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Classes
{
	public class ResponseContract<TContract>
	{
		public HeaderContract Header { get; set; }
		public TContract Data { get; set; }

		public ResponseContract()
		{
			Header = new HeaderContract();
			Header.Code = HttpCodes.Ok;
		}
	}
}
