using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Blog.Infrastructure.Security
{
    public record SignInConfigurations
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public SecurityKey Key { get; }
        public SigningCredentials Credentials { get; set; }

        public SignInConfigurations(string jwtKey)
        {
            Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            Credentials = new(Key, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}
