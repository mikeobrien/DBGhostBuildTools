namespace DbGhost.Build
{
    public class EventArgs<T> : System.EventArgs
    {
        private readonly T _value;

        public EventArgs(T value)
        {
            _value = value;
        }

        public T Value
        { get { return this._value; } }
    }
}

