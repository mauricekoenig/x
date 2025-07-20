

using System;

namespace tcg
{
    public interface ICard
    {
        Guid InstanceId { get; }
        int Id { get;  }
        string Name { get; set; }
        int Cost { get; set; }
        CardType Type { get; set; } 
    }
}