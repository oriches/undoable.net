namespace undoable_net.specs
{
    using System;
    using Machine.Specifications;

    [Subject("Undoable")]
    public class when_there_is_nothing_to_redo
    {
        Establish context = () => undoable = new Undoable();

        Because of = () => canRedo = undoable.CanRedo;

        It should_be_false_for_can_redo = () => canRedo.ShouldBeFalse();

        It should_not_throw_an_exception = () => Exception.ShouldBeNull();

        static Exception Exception;
        static bool canRedo;
        static Undoable undoable;
    }
}