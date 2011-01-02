using System;
using System.IO.Ports;
using EmmLabs.Remote.Core;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace Pre2
{
    public class SettingsViewModel : ViewModelBase
    {
        private SerialCommunicationChannelSettings _settings;

        public string[] AvailablePortNames
        {
            get
            {
                return _settings.AvailablePortNames;
            }
        }

        public string PortName
        {
            get
            {
                return _settings.PortName;
            }
            set
            {
                if (_settings.PortName != value)
                {
                    _settings.PortName = value;
                    RaisePropertyChanged("PortName");
                }
            }
        }

        public int BaudRate
        {
            get
            {
                return _settings.BaudRate;
            }
            set
            {
                if (_settings.BaudRate != value)
                {
                    _settings.BaudRate = value;
                    RaisePropertyChanged("BaudRate");
                }
            }
        }

        public int DataBits
        {
            get
            {
                return _settings.DataBits;
            }
            set
            {
                if (_settings.DataBits != value)
                {
                    try
                    {
                        _settings.DataBits = value;
                        RaisePropertyChanged("DataBits");
                    }
                    catch
                    {
                        Messenger.Default.Send("Oops, please enter a valid number for DataBits. (Between 5 and 8)");
                    }
                }
            }
        }

        public StopBits StopBits
        {
            get
            {
                return _settings.StopBits;
            }
            set
            {
                if (_settings.StopBits != value)
                {
                    _settings.StopBits = value;
                    RaisePropertyChanged("StopBits");
                }
            }
        }

        public Parity Parity
        {
            get
            {
                return _settings.Parity;
            }
            set
            {
                if (_settings.Parity != value)
                {
                    _settings.Parity = value;
                    RaisePropertyChanged("Parity");
                }
            }
        }
		
		public SerialCommunicationChannelSettings Settings
		{
			get
			{
				return _settings;
			}
			set
			{
				_settings = value;
			}
		}

        public SettingsViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
                _settings = new SerialCommunicationChannelSettings();
            }
            else
            {
                // Code runs "for real": Connect to service, etc...
                _settings = new SerialCommunicationChannelSettings();
            }
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean own resources if needed

        ////    base.Cleanup();
        ////}
    }
}