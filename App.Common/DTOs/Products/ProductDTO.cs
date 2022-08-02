using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.DTOs.Products
{
	public class ProductDTO : ProductCreationDTO
	{
		public int Code { get; set; }
	}
}
