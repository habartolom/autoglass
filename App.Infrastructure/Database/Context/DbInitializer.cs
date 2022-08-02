using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Database.Context
{
	public static class DbInitializer
	{
		public static void Initialize(DbContext context)
		{
			context.Database.EnsureCreated();
		}
	}
}
