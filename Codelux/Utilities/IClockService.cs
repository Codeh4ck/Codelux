using System;

namespace Codelux.Utilities
{
    public interface IClockService
    {
        DateTime Now(bool useLocal = false);
    }
}
