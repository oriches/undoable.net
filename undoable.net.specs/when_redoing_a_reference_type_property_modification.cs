namespace undoable_net.specs
{
    using System.Linq;
    using Machine.Specifications;
    using Models;

    [Subject("Undoable")]
    public class when_redoing_a_reference_type_property_modification
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
            undoable.Undo();
            undoable.Redo();
        };

        It parent_widget_should_cotain_child_widget = () => parent.Children.Contains(child).ShouldEqual(true);

        It parent_widget_children_collection_should_not_be_empty = () => parent.Children.Count().ShouldNotEqual(0);

        static Widget parent;
        static Widget child;
        static Undoable undoable;
    }
}