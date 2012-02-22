namespace undoable_net.specs
{
    using System;
    using Machine.Specifications;

    [Subject("Undoable")]
    public class redoing_when_nothing_has_happend_should_do_nothing
    {
        private Establish context = () => undoable = new Undoable();

        private Because of = () => Exception = Catch.Exception(() => undoable.Redo());

        private It should_not_throw_an_exception = () => Exception.ShouldBeNull();

        private static Exception Exception;
        private static Undoable undoable;
    }
}