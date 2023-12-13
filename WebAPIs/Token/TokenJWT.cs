using System.IdentityModel.Tokens.Jwt;

namespace WebAPIs.Token
{
    // Criando a classe do Token de autenticação
    public class TokenJWT
    {
        // Token
        private JwtSecurityToken token;

        // Construtor que recebe o token e atribui a variável privada
        internal TokenJWT(JwtSecurityToken token)
        {
            this.token = token;
        }

        // Tempo de expiração
        public DateTime ValidTo => token.ValidTo;

        // Valor do tempo de expiração
        public string value => new JwtSecurityTokenHandler().WriteToken(token);
    }
}
