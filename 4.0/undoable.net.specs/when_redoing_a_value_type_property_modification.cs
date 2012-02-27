namespace undoable_net.specs
{
    using System;
    using Machine.Specifications;
    using Models;

    [Subject("Undoable")]
    public class when_redoing_a_value_type_property_modification
    {
        Establish context = () =>
        {
            widget = new Widget { Name = OriginalName };
            undoable = new Undoable();
        };

        Because of = () =>
        {
            undoable.Add(() => { widget.Name = OriginalName; }, () => { widget.Name = NewName; });
            
            widget.Name = NewName;

            undoable.Undo();
            undoable.Redo();
        };

        It should_redo_setting_the_name_on_a_widget = () => widget.Name.ShouldEqual(NewName);

        static string OriginalName = "Original Name - " + Guid.NewGuid();
        static string NewName = "New Name - " + Guid.NewGuid();
        static Widget widget;
        static Undoable undoable;
    }
}
