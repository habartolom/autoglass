using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.DTOs.Products
{
	public class ProductPaginationDTO
	{
		[Required]
		[Range(minimum: 1, maximum: Int32.MaxValue, ErrorMessage = "El valor debe ser mayor a 0")]
		public int PageIndex { get; set; }

		[Required]
		[Range(minimum: 1, maximum: Int32.MaxValue, ErrorMessage = "El valor debe ser mayor a 0")]
		public int PageSize { get; set; }
	}
}
