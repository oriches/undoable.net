namespace undoable_net
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;

    public class Undoable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private readonly Stack<Memento> undoStack;
        private readonly Stack<Memento> redoStack;

        public Undoable()
        {
            undoStack = new Stack<Memento>();
            redoStack = new Stack<Memento>();
        }

        public bool CanUndo
        {
            get { return undoStack.Count != 0; }
        }

        public bool CanRedo
        {
            get { return redoStack.Count != 0; }
        }

        public void Add(Action undoAction)
        {
            undoStack.Push(new Memento(undoAction));
            redoStack.Clear();

            RaisePropertyChanged(() => CanUndo);
            RaisePropertyChanged(() => CanRedo);
        }

        public void Add(Action undoAction, Action redoAction)
        {
            undoStack.Push(new Memento(undoAction, redoAction));
            redoStack.Clear();

            RaisePropertyChanged(() => CanUndo);
            RaisePropertyChanged(() => CanRedo);
        }
        
        public void Undo()
        {
            if (undoStack.Count == 0)
            {
                return;
            }

            var current = undoStack.Pop();
            current.Undo();

            if (current.Redo != null)
            {
                redoStack.Push(current);
            }

            RaisePropertyChanged(() => CanUndo);
            RaisePropertyChanged(() => CanRedo);
        }

        public void Redo()
        {
            if (redoStack.Count == 0)
            {
                return;
            }

            var current = redoStack.Pop();
            current.Redo();
            undoStack.Push(current);

            RaisePropertyChanged(() => CanUndo);
            RaisePropertyChanged(() => CanRedo);
        }
        
        public void Clear()
        {
            redoStack.Clear();
            undoStack.Clear();

            RaisePropertyChanged(() => CanUndo);
            RaisePropertyChanged(() => CanRedo);
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> expression)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(ExpressionHelper.Name(expression)));
        }
    }
}