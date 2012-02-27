namespace undoable_net.specs
{
    using System;
    using Machine.Specifications;
    using Models;

    [Subject("Undoable")]
    public class when_clearing_undos_and_redos
    {
        Establish context = () =>
        {
            OriginalName = "Original Name - " + Guid.NewGuid();
            NewName = "New Name - " + Guid.NewGuid();
            
            widget = new Widget { Name = OriginalName };
            undoable = new Undoable();
        };

        Because of = () =>
        {
            exception = Catch.Exception(() =>
            {
                undoable.Add(() =>{ throw new Exception("Shouldn't get here!"); },
                             () =>{ throw new Exception("Shouldn't get here!"); });

                widget.Name = NewName;

                undoable.Clear();
                undoable.Undo();
                undoable.Redo();
            });
        };

        It should_not_have_done_an_undo = () => widget.Name.ShouldEqual(NewName);

        It should_not_generated_an_exception = () => exception.ShouldBeNull();

        static string OriginalName;
        static string NewName;
        
        static Exception exception;
        static Widget widget;
        static Undoable undoable;
    }
}