using System;
using System.Text.Json.Serialization;

namespace immersed.dive.shop.model;

public class Class : IEntity
{
    public Guid Id { get; set; }
    
    public DateTime DateCreated { get; set; }
    
    public DateTime LastUpdated { get; set; }
    
    public bool Live { get; set; }
}