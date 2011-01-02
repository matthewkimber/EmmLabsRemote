using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace EmmLabs.Remote.Core.Tests
{
    [TestFixture]
    public class PreamplifierMessageTest
    {
        #region Volume Volume Message Tests

        [Test]
        public void CanCreateVolumeLevelMessage()
        {
            const int level = 10;
            var frame = String.Format("PRVL{0}", level.ToString("X2"));
            var expected = String.Format("*{0}{1}", frame, CalculateChecksum(frame));
            var msg = PreamplifierMessage.CreateVolumeLevelMessage(level);

            Assert.AreEqual(String.Format(expected, level.ToString("X2")), msg.Value);

            Debug.WriteLine(String.Format("Expected = {0}", expected));
            Debug.WriteLine(String.Format("Actual = {0}", msg.Value));
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WillFailWhenVolumeLevelIsNegative()
        {
            const int level = -1;
            var msg = PreamplifierMessage.CreateVolumeLevelMessage(level);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WillFailWhenVolumeLevelIsOverOneHundred()
        {
            const int level = 101;
            var msg = PreamplifierMessage.CreateVolumeLevelMessage(level);
        }

        #endregion


        #region Input Message Tests

        [Test]
        public void CanCreateInputMessage()
        {
            const int input = 1;
            var frame = String.Format("PRSI01");
            var expected = String.Format("*{0}{1}", frame, CalculateChecksum(frame));
            var msg = PreamplifierMessage.CreateInputMessage(input);

            Assert.AreEqual(expected, msg.Value);

            Debug.WriteLine(String.Format("Expected = {0}", expected));
            Debug.WriteLine(String.Format("Actual = {0}", msg.Value));
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WillFailWhenInputIsBelowAllowableRange()
        {
            const int input = 0;
            var msg = PreamplifierMessage.CreateInputMessage(input);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WillFailWhenInputIsAboveAllowableRange()
        {
            const int input = 7;
            var message = PreamplifierMessage.CreateInputMessage(input);
        }

        #endregion


        #region Mute Message Tests

        [Test]
        public void CanCreateMuteOnMessage()
        {
            var frame = String.Format("PRMT02");
            var expected = String.Format("*{0}{1}", frame, CalculateChecksum(frame));
            var msg = PreamplifierMessage.CreateMuteMessage(PreamplifierMuteState.On);

            Assert.AreEqual(expected, msg.Value);

            Debug.WriteLine(String.Format("Expected = {0}", expected));
            Debug.WriteLine(String.Format("Actual = {0}", msg.Value));
        }

        [Test]
        public void CanCreateSoftMuteMessage()
        {
            var frame = String.Format("PRMT01");
            var expected = String.Format("*{0}{1}", frame, CalculateChecksum(frame));
            var msg = PreamplifierMessage.CreateMuteMessage(PreamplifierMuteState.Soft);

            Assert.AreEqual(expected, msg.Value);

            Debug.WriteLine(String.Format("Expected = {0}", expected));
            Debug.WriteLine(String.Format("Actual = {0}", msg.Value));
        }

        [Test]
        public void CanCreateMuteOffMessage()
        {
            var frame = String.Format("PRMT00");
            var expected = String.Format("*{0}{1}", frame, CalculateChecksum(frame));
            var msg = PreamplifierMessage.CreateMuteMessage(PreamplifierMuteState.Off);

            Assert.AreEqual(expected, msg.Value);

            Debug.WriteLine(String.Format("Expected = {0}", expected));
            Debug.WriteLine(String.Format("Actual = {0}", msg.Value));
        }

        #endregion


        #region Loop Message Tests

        [Test]
        public void CanCreateLoopOnMessage()
        {
            var frame = String.Format("PRLP01");
            var expected = String.Format("*{0}{1}", frame, CalculateChecksum(frame));
            var msg = PreamplifierMessage.CreateLoopMessage(PreamplifierLoopState.On);

            Assert.AreEqual(expected, msg.Value);

            Debug.WriteLine(String.Format("Expected = {0}", expected));
            Debug.WriteLine(String.Format("Actual = {0}", msg.Value));
        }

        [Test]
        public void CanCreateLoopOffMessage()
        {
            var frame = String.Format("PRLP00");
            var expected = String.Format("*{0}{1}", frame, CalculateChecksum(frame));
            var msg = PreamplifierMessage.CreateLoopMessage(PreamplifierLoopState.Off);

            Assert.AreEqual(expected, msg.Value);

            Debug.WriteLine(String.Format("Expected = {0}", expected));
            Debug.WriteLine(String.Format("Actual = {0}", msg.Value));
        }

        #endregion


        #region Display Dim Message Tests

        [Test]
        public void CanCreateDisplayDimFullBrightnessMessage()
        {
            var frame = String.Format("PRDM00");
            var expected = String.Format("*{0}{1}", frame, CalculateChecksum(frame));
            var msg = PreamplifierMessage.CreateDisplayDimMessage(PreamplifierDisplayDimState.Full);

            Assert.AreEqual(expected, msg.Value);

            Debug.WriteLine(String.Format("Expected = {0}", expected));
            Debug.WriteLine(String.Format("Actual = {0}", msg.Value));
        }

        [Test]
        public void CanCreateDisplayDimReducedBrightnessMessage()
        {
            var frame = String.Format("PRDM01");
            var expected = String.Format("*{0}{1}", frame, CalculateChecksum(frame));
            var msg = PreamplifierMessage.CreateDisplayDimMessage(PreamplifierDisplayDimState.Reduced);

            Assert.AreEqual(expected, msg.Value);

            Debug.WriteLine(String.Format("Expected = {0}", expected));
            Debug.WriteLine(String.Format("Actual = {0}", msg.Value));
        }

        [Test]
        public void CanCreateDisplayDimBacklightOffMessage()
        {
            var frame = String.Format("PRDM02");
            var expected = String.Format("*{0}{1}", frame, CalculateChecksum(frame));
            var msg = PreamplifierMessage.CreateDisplayDimMessage(PreamplifierDisplayDimState.Off);

            Assert.AreEqual(expected, msg.Value);

            Debug.WriteLine(String.Format("Expected = {0}", expected));
            Debug.WriteLine(String.Format("Actual = {0}", msg.Value));
        }

        #endregion


        #region Volume Display Units Message Tests

        [Test]
        public void CanCreateVuStepsMessage()
        {
            var frame = String.Format("PRVU01");
            var expected = String.Format("*{0}{1}", frame, CalculateChecksum(frame));
            var msg = PreamplifierMessage.CreateVuMessage(PreamplifierVuState.Steps);

            Assert.AreEqual(expected, msg.Value);

            Debug.WriteLine(String.Format("Expected = {0}", expected));
            Debug.WriteLine(String.Format("Actual = {0}", msg.Value));
        }

        [Test]
        public void CanCreateVuDecibelsMessage()
        {
            var frame = String.Format("PRVU02");
            var expected = String.Format("*{0}{1}", frame, CalculateChecksum(frame));
            var msg = PreamplifierMessage.CreateVuMessage(PreamplifierVuState.Decibels);

            Assert.AreEqual(expected, msg.Value);

            Debug.WriteLine(String.Format("Expected = {0}", expected));
            Debug.WriteLine(String.Format("Actual = {0}", msg.Value));
        }

        #endregion


        #region Save Settings Message Tests

        [Test]
        public void CanCreateSaveSettingsMessage()
        {
            var frame = String.Format("PRSV00");
            var expected = String.Format("*{0}{1}", frame, CalculateChecksum(frame));
            var msg = PreamplifierMessage.CreateSaveSettingsMessage();

            Assert.AreEqual(expected, msg.Value);
            Debug.WriteLine(String.Format("Expected = {0}", expected));
            Debug.WriteLine(String.Format("Actual = {0}", msg.Value));
        }

        #endregion


        #region Null Command Message Tests

        [Test]
        public void CanCreateNullMessage()
        {
            const int number = 0;
            var frame = String.Format("PRNL{0}", number.ToString("X2"));
            var expected = String.Format("*{0}{1}", frame, CalculateChecksum(frame));
            var msg = PreamplifierMessage.CreateNullMessage(number);

            Assert.AreEqual(expected, msg.Value);
            Debug.WriteLine(String.Format("Expected = {0}", expected));
            Debug.WriteLine(String.Format("Actual = {0}", msg.Value));
        }

        #endregion


        #region Helper Methods

        private static string CalculateChecksum(string frame)
        {
            var data = Encoding.ASCII.GetBytes(frame);
            var checksum = data.Aggregate(0x0, (current, b) => current ^ b);

            return checksum.ToString("X2");
        }

        #endregion
    }
}
