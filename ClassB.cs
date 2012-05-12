using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace equals
{
    /// <summary>
    /// Extend the sample further by implementing Equals on a deeper derived type.
    /// </summary>
    public class ClassB : ClassA, IEquatable<ClassB>
    {
        private int valueB;

        public ClassB(int valueB, int valueA, int baseValue)
            : base(valueA, baseValue)
        {
            this.valueB = valueB;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ClassB);
        }

        public override bool Equals(BaseClass other)
        {
            return this.Equals(other as ClassB);
        }

        public override bool Equals(ClassA other)
        {
            return this.Equals(other as ClassB);
        }

        public virtual bool Equals(ClassB other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            return this.valueB == other.valueB;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ this.valueB;
        }

        public override string ToString()
        {
            return String.Format("[{0},{1}]", base.ToString(), this.valueB);
        }
    }
}
