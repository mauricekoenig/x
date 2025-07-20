using System;
using System.Collections.Generic;
using System.Text;

namespace tcg
{
    public interface IEngine
    {
        GameEvents Events { get; }
        GameActionResult TryExecute (ICommand action, IPlayer requester);
        GameActionResult Init(EngineConfig engineConfig);
        GameActionResult Stop();
    }

}
