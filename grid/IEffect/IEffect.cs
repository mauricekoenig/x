using System;
using System.Collections.Generic;
using System.Text;

namespace tcg
{
    public interface IEffect
    {
        IReadOnlyList<IEffectComponent> Components { get; }
        GameActionResult Execute ();
    }

    public class Effect : IEffect
    {
        public IReadOnlyList<IEffectComponent> Components { get; set; }
        public GameActionResult Execute()
        {
            return GameActionResult.Success;
        }
    }
}
