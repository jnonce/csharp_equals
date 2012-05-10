using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace equals
{
    public class BaseClass : IEquatable<BaseClass>
    {
        private int baseValue;

        public BaseClass(int value)
        {
            baseValue = value;
        }

        protected bool? RefEquals(object obj)
        {
            if (Object.ReferenceEquals(obj, null))
            {
                return false;
            }
            else if (Object.ReferenceEquals(obj, this))
            {
                return true;
            }
            else if (this.GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                return null;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as BaseClass);
        }

        public virtual bool Equals(BaseClass other)
        {
            bool? eq = RefEquals(other);
            if (eq.HasValue)
            {
                return eq.Value;
            }
            return other.baseValue == this.baseValue;
        }

        public override string ToString()
        {
            return String.Format("[{0},{1}]", base.ToString(), this.baseValue);
        }
    }
}
