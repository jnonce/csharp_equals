using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace equals
{
    public class ClassA : BaseClass, IEquatable<ClassA>
    {
        private int valueA;

        public ClassA(int valueA, int baseValue)
            : base(baseValue)
        {
            this.valueA = valueA;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ClassA);
        }

        public override bool Equals(BaseClass other)
        {
            return Equals(other as ClassA);
        }

        public virtual bool Equals(ClassA other)
        {
            return base.Equals((BaseClass)other)
                && (valueA == other.valueA);
        }


        public override string ToString()
        {
            return String.Format("[{0},{1}]", base.ToString(), this.valueA);
        }
    }

}
