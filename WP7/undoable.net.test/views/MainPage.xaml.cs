namespace undoable.net.test.views
{
    using System;
    using System.ComponentModel;
    using System.Reactive.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;
    using viewmodels;

    public partial class MainPage : PhoneApplicationPage
    {
        private readonly ApplicationBar appBar;
        private readonly ApplicationBarIconButton undoButton = new ApplicationBarIconButton();
        private readonly ApplicationBarIconButton redoButton = new ApplicationBarIconButton();

        public MainPage()
        {
            InitializeComponent();

            if (appBar == null)
            {
                appBar = new ApplicationBar { IsVisible = true };

                ApplicationBar = appBar;
                CreateUndoAppBarButton();
                CreateRedoAppBarButton();
            }

            var vm = ((MainViewModel)DataContext);

            Observable.FromEventPattern<PropertyChangedEventArgs>(vm, "PropertyChanged")
                       .ObserveOnDispatcher()
                       .Subscribe(args => HandleUndoAble());
        }

        void HandleUndoAble()
        {
            var vm = ((MainViewModel)DataContext);
            undoButton.IsEnabled = vm.Undoable.CanUndo;
            redoButton.IsEnabled = vm.Undoable.CanRedo;
        }
        
        private void CreateUndoAppBarButton()
        {
            undoButton.IconUri = new Uri("/icons/appbar.back.rest.png", UriKind.Relative);
            undoButton.IsEnabled = false;
            undoButton.Text = "undo";
            undoButton.Click += HandleUndoClicked;
            appBar.Buttons.Add(undoButton);
        }
        
        private void CreateRedoAppBarButton()
        {
            redoButton.IconUri = new Uri("/icons/appbar.next.rest.png", UriKind.Relative);
            redoButton.IsEnabled = false;
            redoButton.Text = "redo";
            redoButton.Click += HandleRedoClicked;
            appBar.Buttons.Add(redoButton);
        }

        void HandleUndoClicked(object sender, EventArgs e)
        {
            var vm = ((MainViewModel)DataContext);
            vm.ExecuteUndo();
        }

        void HandleRedoClicked(object sender, EventArgs e)
        {
            var vm = ((MainViewModel)DataContext);
            vm.ExecuteRedo();
        }

        private void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }
}