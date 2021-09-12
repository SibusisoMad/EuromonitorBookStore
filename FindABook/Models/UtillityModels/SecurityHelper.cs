using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace FindABook.Models.UtillityModels
{
    public class SecurityHelper
    {
        public static string GetUserId(HttpContext context)
        {
            var stream = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream) as JwtSecurityToken;
            string userId = Convert.ToString(jsonToken.Payload["jti"]);
            return userId;
        }
    }
}
