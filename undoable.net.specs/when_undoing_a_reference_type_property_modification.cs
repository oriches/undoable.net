namespace undoable_net.specs
{
    using System.Linq;
    using Machine.Specifications;
    using Models;

    [Subject("Undoable")]
    public class when_undoing_a_reference_type_property_modification
    {
        private Establish context = () =>
        {
            undoable = new Undoable();
            parent = new Widget { Name = "Parent Widget" };
            child = new Widget { Name = "Child Widget" };
        };

        private Because of = () =>
        {
            undoable.Add(() => parent.RemoveChild(child), () => parent.AddChild(child));
            parent.AddChild(child);
            undoable.Undo();
        };

        private It parent_widget_should_not_cotain_child_widget = () => parent.Children.Contains(child).ShouldEqual(false);
        
        private It parent_widget_children_collection_should_be_empty = () => parent.Children.Count().ShouldEqual(0);

        private static Widget parent;
        private static Widget child;
        private static Undoable undoable;
    }
}