using System;

namespace EmmLabs.Remote.Core
{
    public class Input : IInput
    {
        #region Fields

        private int _number;
        private string _name;
        private int _volume;

        #endregion


        #region Properties

        public int Number
        {
            get { return _number; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        #endregion


        #region Constructors

        public Input(int number)
            : this(number, String.Empty, 0)
        {}

        public Input(int number, string name, int volume)
        {
            _number = number;
            _name = name;
            _volume = volume;
        }

        #endregion


        #region Public Methods

        public override string ToString()
        {
            return Number.ToString();
        }

        #endregion
    }
}