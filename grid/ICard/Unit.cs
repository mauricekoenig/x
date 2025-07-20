


using System.Collections.Generic;

namespace tcg
{
    public class Unit : BaseCard, ICanAttack, IAttackable, ICanMove
    {
        private Unit (int id, string name, int hp, int attack, int defense, int stepsPerTurn, int range, List<IKeyword> keywords, IMovementBehaviour movementBehaviour) : base (id)
        {
            Name = name;
            Health = hp;
            Attack = attack;
            Defense = defense;
            StepsPerTurn = stepsPerTurn;
            StepsLeft = stepsPerTurn;
            Range = range;

            Keywords = keywords;
            MoveBehaviour = movementBehaviour;
        }
        public static Unit New (int id, string name, int hp, int attack, int defense, int stepsPerTurn, int range, List<IKeyword> keywords, IMovementBehaviour movementBehaviour)
        {
            return new Unit(id, name, hp, attack, defense, stepsPerTurn, range, keywords, movementBehaviour);
        }

        public List<IKeyword> Keywords { get; }

        public GridPosition CurrentPosition { get; set; }
        public int Health { get; set; }
        public int Defense { get; set; }
        public int Attack { get; set; }
        public int StepsPerTurn { get; set; }
        public int StepsLeft { get; set; }
        public bool IsAlive => Health > 0;
        public int Range { get; set; }
        public IMovementBehaviour MoveBehaviour { get; set; }

        public bool CanAttack()
        {
            return true;
        }
        public bool CanMove()
        {
            return StepsPerTurn > 0;
        }
    }

}