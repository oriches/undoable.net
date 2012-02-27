namespace undoable_net.specs
{
    using System;
    using Machine.Specifications;
    using Models;

    [Subject("Undoable")]
    public class when_undoing_a_value_type_property_modification
    {
        Establish context = () =>
        {
            undoable = new Undoable();
            widget = new Widget {Name = OriginalName};
        };

        Because of = () =>
        {
            undoable.Add(() => { widget.Name = OriginalName; }, () => { widget.Name = NewName; });

            widget.Name = NewName;
            undoable.Undo();
        };

        It should_undo_setting_the_name_on_a_widget = () => widget.Name.ShouldEqual(OriginalName);

        static string OriginalName = "Original Name - " + Guid.NewGuid();
        static string NewName = "New name - " + Guid.NewGuid();
        static Widget widget;
        static Undoable undoable;
    }
}