using View.Input;
using View.Shared;

namespace View.Players
{
    class AIPlayer : Player
    {
        public AIPlayer(IView v, AIInput i)
            : base(v, i)
        {
            base.BoardUpdated += i.BoardUpdated;
        }
    }
}
