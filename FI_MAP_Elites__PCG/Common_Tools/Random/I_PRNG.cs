using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Tools
{
    public interface I_PRNG : ICloneable
    {
        int Next();
        int Next(int minValue, int maxValue);
        int Next(int maxValue);
        double NextDouble();
        void NextBytes(byte[] buffer);
    }
}
