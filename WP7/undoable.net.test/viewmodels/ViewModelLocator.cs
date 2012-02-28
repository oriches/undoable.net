namespace undoable.net.test.viewmodels
{
    using System;
    using test;

    public sealed class ViewModelLocator : IDisposable
    {
        private readonly BootStrapper bootStrapper;

        public ViewModelLocator()
        {
            if (bootStrapper == null)
            {
                bootStrapper = new BootStrapper();
            }
        }

        public MainViewModel MainViewModel
        {
            get { return bootStrapper.Container.Resolve<MainViewModel>(); }
        }

        public void Dispose()
        {
            bootStrapper.Dispose();
        }
    }
}