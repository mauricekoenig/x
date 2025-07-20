


namespace tcg
{
    public interface ICanAttack
    {
        int Attack { get; set; }
        int Range { get; set; }
        bool CanAttack();
    }
}