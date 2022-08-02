using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Enums
{
	public enum HttpCodes
	{
        Ok = 200,
        OkExist = 201,
        BadRequest = 400,
        NotFound = 404,
        ValidationError = 421,
        InternalServerError = 500,
        Unautorized = 401
    }
}
