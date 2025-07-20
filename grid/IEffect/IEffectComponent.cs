namespace tcg
{
    public interface IEffectComponent
    {
        public EffectTrigger Trigger { get; set; }
    }

    public class EffectComponent : IEffectComponent
    {
        public EffectTrigger Trigger { get; set; }
        public IEffectCondition Condition { get; set; }
        public IEffectAction Action { get; set; }
    }
}