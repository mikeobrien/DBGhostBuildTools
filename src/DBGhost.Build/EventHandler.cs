using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBGhost.Build
{
    public class EventArgs<T> : System.EventArgs
    {
        private T _value;

        public EventArgs(T value)
        {
            this._value = value;
        }

        public T Value
        { get { return this._value; } }
    }
}

