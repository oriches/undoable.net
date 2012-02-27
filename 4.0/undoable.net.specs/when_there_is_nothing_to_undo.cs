namespace undoable_net.specs
{
    using System;
    using Machine.Specifications;

    [Subject("Undoable")]
    public class when_there_is_nothing_to_undo
    {
        Establish context = () => undoable = new Undoable();

        Because of = () => canUndo = undoable.CanUndo;

        It should_be_false_for_can_undo = () => canUndo.ShouldBeFalse();

        It should_not_throw_an_exception = () => Exception.ShouldBeNull();

        static Exception Exception;
        static bool canUndo;
        static Undoable undoable;
    }
}