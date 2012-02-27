namespace undoable_net
{
    using System;
    using System.Collections.Generic;

    public class Undoable
    {
        private readonly Stack<Memento> undoStack;
        private readonly Stack<Memento> redoStack;

        public Undoable()
        {
            undoStack = new Stack<Memento>();
            redoStack = new Stack<Memento>();
        }

        public void Add(Action undoAction)
        {
            undoStack.Push(new Memento(undoAction));
            redoStack.Clear();
        }

        public void Add(Action undoAction, Action redoAction)
        {
            undoStack.Push(new Memento(undoAction, redoAction));
            redoStack.Clear();
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
        }
        
        public void Clear()
        {
            redoStack.Clear();
            undoStack.Clear();
        }
    }
}