using System;
using System.Collections.Generic;
using EmmLabs.Remote.Core;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Pre2.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Preamplifier _pre2;

        public IInput CurrentInput
        {
            get
            {
                return _pre2.CurrentInput;
            }
            set
            {
                if (_pre2.CurrentInput != value)
                {
                    _pre2.CurrentInput = value;
                    RaisePropertyChanged("CurrentInput");
                    RaisePropertyChanged("Volume");
                }
            }
        }

        public IList<IInput> Inputs
        {
            get
            {
                return _pre2.Inputs;
            }
        }

        public int Volume
        {
            get
            {
                return _pre2.Volume;
            }
            set
            {
                if (_pre2.Volume != value)
                {
                    _pre2.Volume = value;
                    RaisePropertyChanged("Volume");
                }
            }
        }

        public bool IsMuted
        {
            get
            {
                return _pre2.IsMuted;
            }
            set
            {
                if (_pre2.IsMuted != value)
                {
                    _pre2.IsMuted = value;
                    RaisePropertyChanged("IsMuted");
                }
            }
        }

        public RelayCommand ShowSettingsDialogCommand { get; private set; }

        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                _pre2 = new Preamplifier();
            }
            else
            {
                _pre2 = new Preamplifier();
            }

            ShowSettingsDialogCommand = new RelayCommand(ShowSettingsDialog);

            //_pre2.Settings.PortName = "COM5";
        }

        private void ShowSettingsDialog()
        {
            var vm = new SettingsViewModel()
                         {
                             Settings = _pre2.Settings
                         };

            var message = new ModalMessage<SettingsViewModel>(vm, ShowSettingsDialogCallback);
            Messenger.Default.Send(message);
        }

        private void ShowSettingsDialogCallback(bool confirmed, SettingsViewModel returnedViewModel)
        {
            if (confirmed)
            {
                _pre2.Settings = returnedViewModel.Settings;
            }
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }

    public class ModalMessage<TViewModel>
    {
        public TViewModel ViewModel { get; private set; }
        public Action<bool, TViewModel> Callback { get; private set; }

        public ModalMessage(TViewModel vm, Action<bool, TViewModel> callback)
        {
            ViewModel = vm;
            Callback = callback;
        }
    }
}