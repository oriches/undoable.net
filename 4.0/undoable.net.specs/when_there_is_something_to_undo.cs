namespace undoable_net.specs
{
    using System;
    using Machine.Specifications;
    using Models;

    [Subject("Undoable")]
    public class when_there_is_something_to_undo
    {
        Establish context = () =>
        {
            undoable = new Undoable();
            parent = new Widget { Name = "Parent Widget" };
            child = new Widget { Name = "Child Widget" };
        };

        Because of = () =>
        {
            undoable.Add(() => parent.RemoveChild(child), () => parent.AddChild(child));
            parent.AddChild(child);
        };

        It should_be_true_for_can_undo = () => undoable.CanUndo.ShouldBeTrue();

        It should_not_throw_an_exception = () => Exception.ShouldBeNull();

        static Exception Exception;
        static Widget parent;
        static Widget child;
        static Undoable undoable;
    }
}