namespace undoable_net.specs.Models
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class Widget : Model
{
    private int? id;
    private Widget parent;
    private string name;
    private readonly ObservableCollection<Widget> children;

    public Widget()
    {
        children = new ObservableCollection<Widget>();
    }

    public int? Id
    {
        get { return id; } 
        private set
        {
            SetPropertyAndNotify(ref id, value, () => Id);
        }
    }

    public string Name
    {
        get { return name; }
        set
        {
            SetPropertyAndNotify(ref name, value, () => Name);
        }
    }

    public Widget Parent
    {
        get { return parent; }
        private set
        {
            SetPropertyAndNotify(ref parent, value, () => Parent);
        }
    }

    public IEnumerable<Widget> Children
    {
        get { return children; }
    }

    public Widget AddChild(Widget child)
    {
        if (children.Contains(child))
        {
            return this;
        }

        child.Parent = this;
        children.Add(child);

        return this;
    }

    public Widget AddChild(IEnumerable<Widget> childs)
    {
        foreach (var child in childs)
        {
            AddChild(child);
        }

        return this;
    }

    public Widget RemoveChild(Widget child)
    {
        if (!children.Contains(child))
        {
            return this;
        }

        child.Parent = null;
        children.Remove(child);

        return this;
    }

    public Widget RemoveAllChildren()
    {
        foreach (var child in children.ToList())
        {
            child.Parent = null;
            children.Remove(child);
        }

        return this;
    }
}
}
