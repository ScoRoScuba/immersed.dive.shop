using System;
using System.Dynamic;

namespace immersed.dive.shop.model
{
    public interface  IEntity
    {
        Guid Id { get; set; }

        DateTime DateCreated { get; set; }
        DateTime LastUpdated { get; set; }
        bool Live { get; set; }
    }
}
