using System;

namespace Codelux.Cache
{
    public class ExpirationOptions
    {
        private readonly DateTime? _expiresAt;

        public bool Expires => _expiresAt.HasValue;
        public DateTime? ExpiresAt => _expiresAt;

        public ExpirationOptions(DateTime? expiresAt)
        {
            _expiresAt = expiresAt;
        }

        public static ExpirationOptions CreateWithNoExpiration()
        {
            return new(null);
        }

        public static ExpirationOptions CreateWithExpirationAt(DateTime expiresAt)
        {
            if (expiresAt.Kind != DateTimeKind.Utc) expiresAt = expiresAt.ToUniversalTime();
            return new(expiresAt);
        }

        public static ExpirationOptions CreateWithExpirationIn(TimeSpan timeSpan)
        {
            return new(DateTime.UtcNow.Add(timeSpan));
        }
    }
}
