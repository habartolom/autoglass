using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.DTOs.Products
{
	public class ProductCreationDTO
	{

		[Required]
		public string Description { get; set; }

		public bool IsActive { get; set; }

		public DateTime ManufacturingDate { get; set; }

		public DateTime ValidityDate { get; set; }

		public string ProviderCode { get; set; }

		public string ProviderDescription { get; set; }

		public string ProviderPhone { get; set; }
	}
}
