using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace equals
{
    /// <summary>
    /// Base class is the simpliest type in our heirarchy.
    /// </summary>
    public class BaseClass : IEquatable<BaseClass>
    {
        private int baseValue;

        public BaseClass(int value)
        {
            baseValue = value;
        }

        // Send the call over to our IEquatable<BaseClass>.Equals implementation
        public override bool Equals(object obj)
        {
            return Equals(obj as BaseClass);
        }

        // Implements IEquatable<BaseClass>.Equals
        public virtual bool Equals(BaseClass other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                // this isn't null, other is null.
                return false;
            }
            else if (Object.ReferenceEquals(other, this))
            {
                // this and other refer to the same object.
                return true;
            }
            else if (this.GetType() != other.GetType())
            {
                // This and other have different types.
                return false;
            }
            else
            {
                // other is not null, not the same object as this, but it is the same type.
                // Compare all fields of this class to ensure we're equal.
                return other.baseValue == this.baseValue;
            }
        }

        // Implementing Equals logically requires implementing GetHashCode.
        public override int GetHashCode()
        {
            return this.baseValue;
        }

        public override string ToString()
        {
            return String.Format("[{0},{1}]", base.ToString(), this.baseValue);
        }
    }
}
