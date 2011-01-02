using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using Pre2.ViewModel;

namespace Pre2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RegisterMessages();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        private void RegisterMessages()
        {
            Messenger.Default.Register<ModalMessage<SettingsViewModel>>(
                this,
                msg =>
                {
                    var dialog = new SettingsView();
                    var d = (SettingsViewModel)dialog.DataContext;
                    d.Settings = msg.ViewModel.Settings;
                    var result = dialog.ShowDialog();
                    msg.Callback(result ?? false, msg.ViewModel);
                });
        }
    }
}
