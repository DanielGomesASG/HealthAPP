using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebAPIs.Token
{
    // Criando a classe da chave do JWT
    public class JwtSecurityKey
    {
        public static SymmetricSecurityKey Create(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}
