using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Database.Entities
{
	[Table("Products")]
	public class ProductEntity
	{
		[Key]
		public int Code { get; set; }

		[Required]
		public string Description { get; set; }

		public bool Status { get; set; }

		public DateTime ManufacturingDate { get; set; }

		public DateTime ValidityDate { get; set; }

		public string ProviderCode { get; set; }

		public string ProviderDescription { get; set; }

		public string ProviderPhone { get; set; }
	}
}
