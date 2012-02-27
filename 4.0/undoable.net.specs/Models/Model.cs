namespace undoable_net.specs.Models
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;

    public  abstract class Model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected virtual void SetPropertyAndNotify<T>(ref T currentValue, T newValue, Expression<Func<T>> propertyExpression)
        {
            if (Equals(currentValue, newValue))
            {
                return;
            }

            currentValue = newValue;
            RaisePropertyChanged(this, propertyExpression);
        }

        protected virtual void RaisePropertyChanged<T>(object sender, Expression<Func<T>> expression)
        {
            PropertyChanged(sender, new PropertyChangedEventArgs(ExpressionHelper.Name(expression)));
        }
    }
}