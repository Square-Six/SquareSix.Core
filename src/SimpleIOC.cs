using System;
using SquareSix.Core.Interfaces;

namespace SquareSix.Core
{
    public class SimpleIOC
    {
        static SimpleIOC()
        {
        }

        static ISimpleIOC _simpleContainer;

        public static ISimpleIOC Container
        {
            get
            {
                if (_simpleContainer == null)
                {
                    _simpleContainer = new SimpleIOCContainer();
                }

                return _simpleContainer;
            }
        }
    }
}
