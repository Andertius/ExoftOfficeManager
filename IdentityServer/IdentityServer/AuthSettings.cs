using System.Text;

using Microsoft.IdentityModel.Tokens;

namespace IdentityServer
{
    public class AuthSettings
    {
        public string ISSUER;
        public string AUDIENCE;
        public int LIFETIME;
        public string KEY = "";

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
