namespace undoable.net.test.viewmodels
{
    using System;
    using System.Linq.Expressions;
    using GalaSoft.MvvmLight;
    using models;
    using undoable_net;

    public class MainViewModel : ViewModelBase
    {
        readonly Widget widget;
        readonly Undoable undoable;

        public MainViewModel(Undoable undoable)
        {
            this.undoable = undoable;
            widget = new Widget();
        }

        public Undoable Undoable
        {
            get { return undoable; }
        }

        public string Identifier
        {
            get { return widget.Identifier; }
            set
            {
                SetPropertyWithUndoActions(widget.Identifier, value, str => { widget.Identifier = str; }, () => Identifier); 
            }
        }

        public string Name
        {
            get { return widget.Name; }
            set
            {
                SetPropertyWithUndoActions(widget.Name, value, str => { widget.Name = str; }, () => Name); 
            }
        }

        public string Description
        {
            get { return widget.Description; }
            set
            {
                SetPropertyWithUndoActions(widget.Description, value, str => { widget.Description = str; }, () => Description); 
            }
        }
        
        public void ExecuteUndo()
        {
            undoable.Undo();
        }

        public void ExecuteRedo()
        {
            undoable.Redo();
        }

        private void SetPropertyWithUndoActions<T>(T currentValue, T newValue, Action<T> setValue, Expression<Func<T>> expression)
        {
            if (Equals(currentValue, newValue))
            {
                return;
            }

            var cVal = currentValue;
            undoable.Add(() =>
                            {
                                setValue(cVal);
                                RaisePropertyChanged(ExpressionHelper.Name(expression));
                            },
                        () =>
                        {
                            setValue(newValue);
                            RaisePropertyChanged(ExpressionHelper.Name(expression));
                        });

            setValue(newValue);
            RaisePropertyChanged(ExpressionHelper.Name(expression));
        }
    }
}
