using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace equals
{
    /// <summary>
    /// Derived type from BaseClass to demonstrate how to handle Equals in a derived class.
    /// </summary>
    public class ClassA : BaseClass, IEquatable<ClassA>
    {
        private int valueA;

        public ClassA(int valueA, int baseValue)
            : base(baseValue)
        {
            this.valueA = valueA;
        }

        // Override Object.Equals
        public override bool Equals(object obj)
        {
            return Equals(obj as ClassA);
        }

        // Override IEquatable<BaseClass>.Equals
        public override bool Equals(BaseClass other)
        {
            return Equals(other as ClassA);
        }

        // Implement IEquatable<ClassA>.Equals
        public virtual bool Equals(ClassA other)
        {
            // base.Equals will return false if other is null, not of the same type, or has different BaseClass field data.
            // Only if base.Equals returns true will we continue to check our fields.
            return base.Equals((BaseClass)other)
                && (valueA == other.valueA);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ this.valueA;
        }

        public override string ToString()
        {
            return String.Format("[{0},{1}]", base.ToString(), this.valueA);
        }
    }

}
