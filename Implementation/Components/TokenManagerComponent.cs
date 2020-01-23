using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Implementation.Components
{
    public class TokenManagerComponent : ITokenManagerComponent
    {
        //HMACSHA256 
        private static readonly string _secret = "XCAP05H6LoKvbRRa/QkqLNMI7cOHguaRyHzyg7n5qEkGjQmtBhz4SzYh4Fqwjyi3KJHlSXKPwVu2+bXr6CtpgQ==";

        public string GenerateToken(string username)
        {
            var key = Convert.FromBase64String(_secret);

            var securityKey = new SymmetricSecurityKey(key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),

                Expires = DateTime.UtcNow.AddMinutes(30),

                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtHandler = new JwtSecurityTokenHandler();

            JwtSecurityToken token = jwtHandler.CreateJwtSecurityToken(tokenDescriptor);

            //token.Payload["favouriteFood"] = "cheese";

            return jwtHandler.WriteToken(token);
        }

        public ClaimsPrincipal GetClaimsPrincipal(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);

            if (jwtToken is null)
                return null;

            byte[] key = Convert.FromBase64String(_secret);

            var parameters = new TokenValidationParameters()
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            SecurityToken securityToken;

            ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, parameters, out securityToken);

            return claimsPrincipal;
        }

        public string ValidateToken(string token)
        {
            string username = null;

            ClaimsPrincipal claimsPrincipal = GetClaimsPrincipal(token);

            if (claimsPrincipal is null)
                return null;

            ClaimsIdentity identity = null;

            identity = (ClaimsIdentity)claimsPrincipal.Identity;

            Claim usernameClaim = identity.FindFirst(ClaimTypes.Name);

            return username = usernameClaim.Value;
        }
    }
}
