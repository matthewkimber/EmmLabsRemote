using System;
using System.Text;
using System.Text.RegularExpressions;

namespace EmmLabs.Remote.Core
{
    public class PreamplifierMessage : IMessage
    {
        #region Fields

        private readonly string _value;

        #endregion


        #region Properties

        public string Value
        {
            get { return _value; }
        }

        #endregion


        #region Constructor

        private PreamplifierMessage(string frame)
        {
            if (!IsValidFrame(frame))
            {
                throw new InvalidFrameFormatException(frame);
            }

            if (frame.Length != 9)
            {
                var stringBuilder = new StringBuilder();

                // If the preamble is not present.
                if (frame[0] != '*')
                {
                    stringBuilder.Append("*");
                }

                stringBuilder.Append(frame);

                // Check to see if there isn't an existing checksum present in the frame.
                if (frame.Length == 6)
                {
                    stringBuilder.Append(CalculateChecksum(frame)); // Calculate and append checksum.
                }
                else
                {
                    stringBuilder.Append(CalculateChecksum(frame.Substring(1, 6)));
                }

                frame = stringBuilder.ToString(); // Return fully qualified frame.
            }

            _value = frame;
        }

        #endregion


        #region Private Methods

        private static bool IsValidFrame(string frame)
        {
            // Valid Frame Structure
            // ---------------------------
            // |*|P1|P0|M3|M2|M1|M0|C1|C0|
            // ---------------------------
            //
            // Length: 9
            // Starts with: "*"
            // Can end with: "NA"

            const string validFramePattern = @"^\*?(BD|CD|DA|PR|SC)(DM|LP|MT|NL|SI|SV|VL|VU)([0-9a-fA-F][0-9a-fA-F])([0-9a-fA-F]|NA)?$";

            return Regex.IsMatch(frame, validFramePattern);

        }

        private static string CalculateChecksum(string frame)
        {
            var data = Encoding.ASCII.GetBytes(frame);
            var checksum = 0x0;

            foreach (var b in data)
            {
                checksum ^= b;
            }

            return checksum.ToString("X2");
        }

        #endregion


        #region Public Methods

        public static PreamplifierMessage CreateVolumeLevelMessage(int level)
        {
            if ((level >= 0) && (level <= 100))
            {
                return new PreamplifierMessage(String.Format("*PRVL{0}", level.ToString("X2")));
            }

            throw new ArgumentOutOfRangeException("level", "Please enter a volume level between 0 and 100.");
        }

        public static PreamplifierMessage CreateInputMessage(int input)
        {
            if (input >= 1 && input <= 6)
            {
                return new PreamplifierMessage(String.Format("*PRSI{0}", input.ToString("X2")));
            }

            throw new ArgumentOutOfRangeException("input", "Please enter an input between 1 and 6.");
        }

        public static PreamplifierMessage CreateMuteMessage(PreamplifierMuteState state)
        {
            switch (state)
            {
                case PreamplifierMuteState.Off:
                    return new PreamplifierMessage("*PRMT00");
                case PreamplifierMuteState.Soft:
                    return new PreamplifierMessage("*PRMT01");
                case PreamplifierMuteState.On:
                    return new PreamplifierMessage("*PRMT02");
                default:
                    throw new ArgumentOutOfRangeException("state");
            }
        }

        public static PreamplifierMessage CreateLoopMessage(PreamplifierLoopState state)
        {
            switch (state)
            {
                case PreamplifierLoopState.Off:
                    return new PreamplifierMessage("*PRLP00");
                case PreamplifierLoopState.On:
                    return new PreamplifierMessage("*PRLP01");
                default:
                    throw new ArgumentOutOfRangeException("state");
            }
        }

        public static PreamplifierMessage CreateDisplayDimMessage(PreamplifierDisplayDimState state)
        {
            switch (state)
            {
                case PreamplifierDisplayDimState.Full:
                    return new PreamplifierMessage("*PRDM00");
                case PreamplifierDisplayDimState.Reduced:
                    return new PreamplifierMessage("*PRDM01");
                case PreamplifierDisplayDimState.Off:
                    return new PreamplifierMessage("*PRDM02");
                default:
                    throw new ArgumentOutOfRangeException("state");
            }
        }

        public static PreamplifierMessage CreateVuMessage(PreamplifierVuState state)
        {
            switch (state)
            {
                case PreamplifierVuState.Steps:
                    return new PreamplifierMessage("*PRVU01");
                case PreamplifierVuState.Decibels:
                    return new PreamplifierMessage("*PRVU02");
                default:
                    throw new ArgumentOutOfRangeException("state");
            }
        }

        public static PreamplifierMessage CreateNullMessage(int number)
        {
            return new PreamplifierMessage(String.Format("*PRNL{0}", number.ToString("X2")));
        }

        public static PreamplifierMessage CreateSaveSettingsMessage()
        {
            return new PreamplifierMessage("*PRSV00");
        }

        #endregion
    }
}
