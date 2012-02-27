namespace undoable_net.specs
{
    using System.Linq;
    using Machine.Specifications;
    using Models;

    [Subject("Undoable")]
    public class when_a_redo_is_not_specified_it_should_only_do_an_undo
    {
        Establish context = () =>
        {
            undoable = new Undoable();
            parent = new Widget { Name = "Parent Widget" };
            child = new Widget { Name = "Child Widget" };
        };

        Because of = () =>
                                 {
                                     undoable.Add(() => parent.RemoveChild(child));
                                     parent.AddChild(child);
                                     undoable.Undo();
                                     undoable.Redo();
                                 };

        It parent_widget_should_not_cotain_child_widget = () => parent.Children.Contains(child).ShouldEqual(false);

        It parent_widget_children_collection_should_be_empty = () => parent.Children.Count().ShouldEqual(0);

        static Widget parent;
        static Widget child;
        static Undoable undoable;
    }
}