
using tcg;

public interface IAttackable
{
    int Health { get; set; }
    int Defense { get; set; }
    bool IsAlive { get; }

}