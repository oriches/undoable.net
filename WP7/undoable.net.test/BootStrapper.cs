namespace undoable.net.test
{
    using System;
    using Funq;
    using undoable_net;
    using viewmodels;

    public sealed class BootStrapper : IDisposable
    {
        public BootStrapper()
        {
            Container = new Container();

            ConfigureContainer();
        }

        public Container Container { get; private set; }

        public void Dispose()
        {
            if (Container != null)
            {
                Container.Dispose();
                Container = null;
            }
        }

        private void ConfigureContainer()
        {
            Container.Register(c => new Undoable());

            Container.Register(c => new MainViewModel(c.Resolve<Undoable>()));
        }
    }
}
