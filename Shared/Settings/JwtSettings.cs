

namespace Shared.Settings
{
    public class JwtSettings
    {
        public string? Key {get; set;}
        public string? Issuer {get; set;}
        public string? Audience {get; set;}
        public double ExpireMinutes {get; set;}
    }
}