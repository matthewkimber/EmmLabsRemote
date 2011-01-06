using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace EmmLabs.Remote.Core
{
    public class Preamplifier : IPreamplifier
    {
        #region Fields

        private readonly SerialCommunicationChannel _commChannel;
        private IInput _currentInput;
        private bool _isMuted;
        private bool _isSoftMuted;

        #endregion


        #region Properties

        public string Class
        {
            get
            {
                return "PR";
            }
        }

        public IInput CurrentInput
        {
            get
            {
                return _currentInput;
            }
            set
            {
                if (_currentInput != value)
                {
                    SendCommand(PreamplifierMessage.CreateInputMessage(value.Number));
                    _currentInput = value;
                }
            }
        }

        public bool IsMuted
        {
            get
            {
                return _isMuted;
            }
            set
            {
                if (value)
                {
                    _isMuted = true;
                    Mute(PreamplifierMuteState.On);
                }
                else
                {
                    _isMuted = false;
                    Mute(PreamplifierMuteState.Off);
                }
            }
        }

        public bool IsSoftMuted
        {
            get
            {
                return _isSoftMuted;
            }
        }

        public SerialCommunicationChannelSettings Settings { get; set; }

        public int Volume
        {
            get
            {
                return CurrentInput.Volume;
            }
            set
            {
                if (CurrentInput.Volume != value)
                {
                    CurrentInput.Volume = value;
                    var msg = PreamplifierMessage.CreateVolumeLevelMessage(CurrentInput.Volume);
                    Debug.WriteLine(String.Format("Value = {0}, Message = {1}", value, msg.Value));
                    SendCommand(msg);

                    if (IsMuted)
                    {
                        Mute(PreamplifierMuteState.Off);
                    }

                    if (IsSoftMuted)
                    {
                        Mute(PreamplifierMuteState.Off);
                    }
                }
            }
        }

        public IList<IInput> Inputs { get; private set; }

        #endregion


        #region Constructors

        public Preamplifier()
        {
            Inputs = new List<IInput>();

            for (var inputCount = 0; inputCount < 6; inputCount++)
            {
                Inputs.Add(new Input(inputCount + 1));
            }

            _isMuted = false;
            _currentInput = Inputs[0];
            _commChannel = new SerialCommunicationChannel();
        }

        #endregion


        #region Public Methods

        public void FadeTo(int level)
        {
            if (Volume > level)
            {
                for (var step = Volume; step >= level; step--)
                {
                    Volume = step;
                    Thread.Sleep(10);
                }
            }
            else
            {
                for (var step = Volume; step <= level; step++)
                {
                    Volume = step;
                    Thread.Sleep(10);
                }
            }
        }

        public void Mute(PreamplifierMuteState state)
        {
            switch (state)
            {
                case PreamplifierMuteState.Off:
                    MuteOff();
                    break;
                case PreamplifierMuteState.On:
                    MuteOn();
                    break;
                case PreamplifierMuteState.Soft:
                    MuteSoft();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("state");
            }
        }

        public void Loop(PreamplifierLoopState state)
        {
            switch (state)
            {
                case PreamplifierLoopState.Off:
                    LoopOff();
                    break;
                case PreamplifierLoopState.On:
                    LoopOn();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("state");
            }

        }

        public void SelectInput(int inputNumber)
        {
            foreach (var i in Inputs)
            {
                if (i.Number == inputNumber)
                {
                    _currentInput = i;
                    return;
                }
            }

            throw new InvalidInputException(inputNumber);
        }

        public void SendCommand(IMessage message)
        {
            _commChannel.Write(message);
        }

        #endregion


        #region Private Methods

        private void MuteOn()
        {
            SendCommand(PreamplifierMessage.CreateMuteMessage(PreamplifierMuteState.On));
            _isMuted = true;
        }

        private void MuteOff()
        {
                SendCommand(PreamplifierMessage.CreateMuteMessage(PreamplifierMuteState.Off));
                _isMuted = false;
        }

        private void MuteSoft()
        {
                if (!IsMuted && !IsSoftMuted)
                {
                    SendCommand(PreamplifierMessage.CreateMuteMessage(PreamplifierMuteState.Soft));
                    _isSoftMuted = true;
                }
        }

        private void LoopOn()
        {}

        private void LoopOff()
        {}

        #endregion
    }
}
