using System;

namespace Codelux.Cache
{
    public class CachedItem
    {
        private readonly ExpirationOptions _expirationOptions;

        public CachedItem(object value, ExpirationOptions expirationOptions)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
            _expirationOptions = expirationOptions ?? throw new ArgumentNullException(nameof(expirationOptions));
        }

        public object Value { get; set; }

        public DateTime? ExpiresAt => _expirationOptions.ExpiresAt;

        public bool HasExpired
        {
            get
            {
                if (!_expirationOptions.ExpiresAt.HasValue) return false;
                return _expirationOptions.ExpiresAt < DateTime.UtcNow;
            }
        }
    }
}
