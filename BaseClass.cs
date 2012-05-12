﻿using System;
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

        public override bool Equals(object obj)
        {
            return Equals(obj as BaseClass);
        }

        public virtual bool Equals(BaseClass other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            else if (Object.ReferenceEquals(other, this))
            {
                return true;
            }
            else if (this.GetType() != other.GetType())
            {
                return false;
            }
            else
            {
                return other.baseValue == this.baseValue;
            }
        }

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