using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading.Tasks;


namespace EmmLabs.Remote.Core
{
    public class SerialCommunicationChannel : ICommunicationChannel, IDisposable
    {
        public SerialPort Port { get; private set; }
        public SerialCommunicationChannelSettings Settings { get; private set; }

        public SerialCommunicationChannel()
            : this(new SerialCommunicationChannelSettings())
        {}

        public SerialCommunicationChannel(SerialCommunicationChannelSettings settings)
        {
            Settings = settings;
            Port = new SerialPort(Settings.PortName, 
                                  Settings.BaudRate, 
                                  Settings.Parity, 
                                  Settings.DataBits,
                                  Settings.StopBits);

            Port.Open();
        }

        public string Read()
        {
            throw new NotImplementedException();
        }

        public void Write(IMessage message)
        {
            var messages = new List<IMessage> { message };

            Parallel.ForEach(messages, msg =>
                                           {
                                               
                                               Port.Write(message.Value);

#if DEBUG
                                               Debug.WriteLine(String.Format("Message Written. ({0})", message.Value));
#endif
                                           });
        }

        public void Dispose()
        {
            if (Port.IsOpen)
            {
                Port.Close();
            }
        }
    }
}
