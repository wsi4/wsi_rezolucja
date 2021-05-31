using Resolution.Visitors;
using System;

namespace Resolution.Sentences
{
    public abstract class Sentence : IEquatable<Sentence>, ICloneable
    {
        public bool Negated { get; protected set; }
        public abstract void Accept(AbstractVisitor visitor);

        public void Negate()
        {
            Negated = !Negated;
        }
        public abstract object Clone();
        public abstract bool Equals(Sentence other);

        public override bool Equals(object obj)
        {
            return obj is Sentence && Equals(obj as Sentence);
        }

        public abstract bool Contains(string l);

        public abstract override int GetHashCode();
    }
}
