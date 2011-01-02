using System;
using System.Diagnostics;
using NUnit.Framework;

namespace EmmLabs.Remote.Core.Tests
{
    [TestFixture]
    public class PreamplifierTests
    {
        [Test]
        public void CanCreateNewPreamplifier()
        {
            var pre = new Preamplifier();

            Assert.NotNull(pre);
        }

        [Test]
        public void PreamplifierHasSixInputs()
        {
            const int numberOfInputs = 6;
            var pre = new Preamplifier();

            Assert.AreEqual(numberOfInputs, pre.Inputs.Count);

            foreach (var i in pre.Inputs)
            {
                Debug.WriteLine(String.Format("Input {0}", i.Number));
            }
        }

        [Test]
        public void DefaultSelectedInputIsNumberOne()
        {
            const int inputNumber = 1;
            var pre = new Preamplifier();

            Assert.AreEqual(inputNumber, pre.CurrentInput.Number);

            Debug.WriteLine(String.Format("Input {0}", pre.CurrentInput.Number));
        }

        [Test]
        public void DefaultVolumeForEachInputShouldBeZero()
        {
            const int expectedVolume = 0;
            var pre = new Preamplifier();

            foreach (var i in pre.Inputs)
            {
                Assert.AreEqual(expectedVolume, i.Volume);
                Debug.WriteLine(String.Format("Input {0} Volume = {1}", i.Number, i.Volume));
            }
        }

        [Test]
        public void ChangeVolumeOnCurrentInput()
        {
            const int expectedVolume = 100;
            var pre = new Preamplifier();

            pre.Volume = expectedVolume;

            Assert.AreEqual(expectedVolume, pre.Volume);
            Assert.AreEqual(expectedVolume, pre.CurrentInput.Volume);
        }

        [Test]
        public void CanSelectDifferentInput()
        {
            const int expectedInput = 2;
            var pre = new Preamplifier();

            Debug.WriteLine(String.Format("Default Input = #{0}", pre.CurrentInput.Number));

            pre.SelectInput(expectedInput);

            Assert.AreEqual(expectedInput, pre.CurrentInput.Number);
            Debug.WriteLine(String.Format("New Input = #{0}", pre.CurrentInput.Number));
        }

        [Test]
        [ExpectedException(typeof(InvalidInputException))]
        public void CannotSelectInputNumberZero()
        {
            const int inputZero = 0;
            var pre = new Preamplifier();

            pre.SelectInput(inputZero);
        }

        [Test]
        [ExpectedException(typeof(InvalidInputException))]
        public void CannotSelectNegativeInput()
        {
            const int negativeInput = -1;
            var pre = new Preamplifier();

            pre.SelectInput(negativeInput);
        }

        [Test]
        [ExpectedException(typeof(InvalidInputException))]
        public void CannotSelectInputGreaterThanSix()
        {
            const int greaterThanInput = 7;
            var pre = new Preamplifier();

            pre.SelectInput(greaterThanInput);
        }

        [Test]
        public void CanNameCurrentInput()
        {
            const string inputName = "SACD";
            var pre = new Preamplifier();

            pre.CurrentInput.Name = inputName;

            Assert.AreEqual(inputName, pre.CurrentInput.Name);
            Debug.WriteLine(String.Format("Current Input Name = \"{0}\"", pre.CurrentInput.Name));
        }

        [Test]
        public void CanMuteOnSystem()
        {
            var pre = new Preamplifier();

            pre.Mute(PreamplifierMuteState.On);

            Assert.AreEqual(true, pre.IsMuted);
        }

        [Test]
        public void CanMuteOffSystem()
        {
            var pre = new Preamplifier();

            pre.Mute(PreamplifierMuteState.On);
            pre.Mute(PreamplifierMuteState.Off);

            Assert.AreEqual(false, pre.IsMuted);
        }

        [Test]
        public void CanSoftMuteSystem()
        {
            var pre = new Preamplifier { Volume = 100 };

            pre.Mute(PreamplifierMuteState.Soft);

            Assert.AreEqual(true, pre.IsSoftMuted);
        }

        [Test]
        public void AutomaticMuteOffWhenVolumeChanged()
        {
            var pre = new Preamplifier();

            pre.Volume = 65;
            pre.Mute(PreamplifierMuteState.On);
            pre.Volume = 70;

            Assert.AreEqual(false, pre.IsMuted);
        }

        [Test]
        public void AutomaticSoftMuteOffWhenVolumeChanged()
        {
            var pre = new Preamplifier();

            pre.Volume = 65;
            pre.Mute(PreamplifierMuteState.Soft);
            pre.Volume = 75;

            Assert.AreEqual(false, pre.IsSoftMuted);
            Assert.AreEqual(false, pre.IsMuted);
        }
    }
}
