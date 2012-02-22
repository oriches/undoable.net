namespace undoable_net.specs
{
    using System;
    using Machine.Specifications;

    [Subject("Undoable")]
    public class redoing_when_nothing_has_happend_should_do_nothing
    {
        Establish context = () => undoable = new Undoable();

        Because of = () => Exception = Catch.Exception(() => undoable.Redo());

        It should_not_throw_an_exception = () => Exception.ShouldBeNull();

        static Exception Exception;
        static Undoable undoable;
    }
}