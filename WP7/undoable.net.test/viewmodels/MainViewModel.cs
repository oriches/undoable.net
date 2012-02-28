namespace undoable.net.test.viewmodels
{
    using undoable_net;

    public class MainViewModel
    {
        readonly Undoable undoable;

        public MainViewModel(Undoable undoable)
        {
            this.undoable = undoable;
        }

        public bool IsCanUndoEnabled
        {
            get { return undoable.CanUndo; }
        }

        public bool IsCanRedoEnabled
        {
            get { return undoable.CanRedo; }
        }
    }
}
