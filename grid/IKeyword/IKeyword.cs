


namespace tcg
{
    public interface IKeyword
    {
        string Name { get; }
        void Apply (Unit source);
    }
    public abstract class BaseKeyword : IKeyword
    {
        protected string name;
        public string Name => name;

        public abstract void Apply(Unit source);
    }
    public sealed class Keyword_Flying : BaseKeyword
    {
        public Keyword_Flying() => name = "Flying";
        public override void Apply (Unit source)
        {
            
        }
    }
    public sealed class Keyword_Guardian : BaseKeyword
    {
        public Keyword_Guardian() => name = "Guardian";
        public override void Apply(Unit source)
        {

        }
    }
}