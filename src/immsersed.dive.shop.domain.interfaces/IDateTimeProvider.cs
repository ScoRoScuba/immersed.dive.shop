using System;

namespace immersed.dive.shop.domain.interfaces;

public interface IDateTimeProvider
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
    DateTime Epoch { get; }
}