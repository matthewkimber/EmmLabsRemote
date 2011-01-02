using System;
using System.ComponentModel;
using System.IO.Ports;

namespace EmmLabs.Remote.Core
{
    public class SerialCommunicationChannelSettings
    {
        #region Fields

        private int _dataBits;

        #endregion


        #region Properties

        public string[] AvailablePortNames
        {
            get
            {
                return SerialPort.GetPortNames();
            }
        }

        public string PortName { get; set; }
        public int BaudRate { get; set; } // (def = 9600)

        public int DataBits
        {
            get
            {
                return _dataBits;
            }
            set 
            { 
                // 5 <= x <= 8
                if (value >= 5 && value <= 8)
                {
                    _dataBits = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("DataBits");
                }
            }
        }

        public StopBits StopBits { get; set; }
        public Parity Parity { get; set; }

        #endregion


        #region Constructors

        public SerialCommunicationChannelSettings()
            : this(9600, 8, StopBits.One, Parity.None)
        {}

        public SerialCommunicationChannelSettings(int baudRate, int dataBits, StopBits stopBits, Parity parity)
        {
            PortName = GetDefaultPort();
            BaudRate = baudRate;
            DataBits = dataBits;
            StopBits = stopBits;
            Parity = parity;
        }

        public SerialCommunicationChannelSettings(string portName, int baudRate, int dataBits, StopBits stopBits, Parity parity)
        {
            PortName = portName;
            BaudRate = baudRate;
            DataBits = dataBits;
            StopBits = stopBits;
            Parity = parity;
        }

        #endregion


        #region Private Methods

        private static string GetDefaultPort()
        {
            try
            {
                var ports = SerialPort.GetPortNames();

                return ports.Length > 0 ? ports[0] : String.Empty;
            }
            catch (Win32Exception ex)
            {
                throw new CommunicationChannelException("Unable to query the serial port names.", ex);
            }
        }

        #endregion
    }
}