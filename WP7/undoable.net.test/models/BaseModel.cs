namespace undoable.net.test.models
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;

    public abstract class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public virtual bool HasBeenModified { get { return false; } }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> expression)
        {
            RaisePropertyChanged(ExpressionHelper.Name(expression));
        }

        protected void RaisePropertyChanged<T>(object sender, Expression<Func<T>> expression)
        {
            PropertyChanged(sender, new PropertyChangedEventArgs(ExpressionHelper.Name(expression)));
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void RaisePropertyChanged(object sender, string propertyName)
        {
            PropertyChanged(sender, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void SetPropertyAndNotify<T>(ref T currentValue, T newValue, Expression<Func<T>> propertyExpression)
        {
            SetPropertyAndNotify(ref currentValue, newValue, ExpressionHelper.Name(propertyExpression));
        }

        protected virtual void SetPropertyAndNotify<T>(ref T currentValue, T newValue, string propertyName)
        {
            if (Equals(currentValue, newValue))
            {
                return;
            }

            currentValue = newValue;
            RaisePropertyChanged(propertyName);
        }

        protected int CombineHashCodes(params object[] objects)
        {
            var hash = 0;

            foreach (var t in objects)
            {
                hash = (hash << 5) + hash;
                hash ^= GetEntryHash(t);
            }

            return hash;
        }

        private int GetEntryHash(object entry)
        {
            var entryHash = 0x61E04917; // slurped from .Net runtime internals...

            if (entry != null)
            {
                var subObjects = entry as object[];
                entryHash = subObjects != null ? this.CombineHashCodes(subObjects) : entry.GetHashCode();
            }

            return entryHash;
        }
    }
}
