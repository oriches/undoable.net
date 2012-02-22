namespace undoable_net.specs
{
    using System;
    using Machine.Specifications;
    using Models;

    [Subject("Undoable")]
    public class when_undoing_a_value_type_property_modification
    {
        private Establish context = () =>
        {
            undoable = new Undoable();
            widget = new Widget {Name = OriginalName};
        };

        private Because of = () =>
        {
            undoable.Add(() => { widget.Name = OriginalName; }, () => { widget.Name = NewName; });

            widget.Name = NewName;
            undoable.Undo();
        };

        private It should_undo_setting_the_name_on_a_widget = () => widget.Name.ShouldEqual(OriginalName);

        private static string OriginalName = "Original Name - " + Guid.NewGuid();
        private static string NewName = "New name - " + Guid.NewGuid();
        private static Widget widget;
        private static Undoable undoable;
    }
}