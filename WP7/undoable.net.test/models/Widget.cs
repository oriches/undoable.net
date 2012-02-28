namespace undoable.net.test.models
{
    public class Widget : BaseModel
    {
        string identifier = string.Empty;
        string name = string.Empty;
        string description = string.Empty;

        public string Identifier
        {
            get { return identifier; }
            set
            {
                SetPropertyAndNotify(ref identifier, value, () => Identifier);
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

        public string Description
        {
            get { return description; }
            set
            {
                SetPropertyAndNotify(ref description, value, () => Description);
            }
        }
    }
}