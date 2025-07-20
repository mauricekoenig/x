


using System;

namespace tcg
{
    public abstract class BaseCard : ICard
    {
        public int Id { get; }
        public Guid InstanceId {get;}
        public string Name { get; set; }
        public int Cost { get; set; }
        public CardType Type { get; set; }

        protected BaseCard (int id)
        {
            Id = id;
            InstanceId = Guid.NewGuid();
        }

        public override string ToString()
        {
            return $"InstanceId: {InstanceId}, Id: {Id}, Name: {Name}, Cost: {Cost}, Type: {Type}";
        }
    }
}