namespace undoable_net
{
    using System;

    public class Memento
    {
        public Action Undo { get; private set; }
        public Action Redo { get; private set; }

        public Memento(Action undo)
        {
            Undo = undo;
            Redo = null;
        }

        public Memento(Action undo, Action redo)
        {
            Undo = undo;
            Redo = redo;
        }
    }
}