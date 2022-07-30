// source originally copied from : https://referencesource.microsoft.com/#mscorlib/system/random.cs,bb77e610694e64ca
// this modification adds the functionality of being able to clone the random class instance (as a deep copy)

using System;
using System.Diagnostics.Contracts;

namespace Common_Tools
{
    [Serializable]
    public class PRNG_Basic: I_PRNG
    {
        // private constant values
        private const int MBIG = Int32.MaxValue;
        private const int MSEED = 161803398;

        // State
        private int inext;
        private int inextp;
        private int[] SeedArray = new int[56];

        #region constructors

        // ticks - based initialization
        public PRNG_Basic()
          : this(Environment.TickCount)
        {

        }

        // seed - based initialization
        public PRNG_Basic(int Seed)
        {
            int ii;
            int mj, mk;

            //Initialize our Seed array.
            //This algorithm comes from Numerical Recipes in C (2nd Ed.)
            int subtraction = (Seed == Int32.MinValue) ? Int32.MaxValue : Math.Abs(Seed);
            mj = MSEED - subtraction;
            SeedArray[55] = mj;
            mk = 1;
            for (int i = 1; i < 55; i++)
            {  //Apparently the range [1..55] is special (Knuth) and so we're wasting the 0'th position.
                ii = (21 * i) % 55;
                SeedArray[ii] = mk;
                mk = mj - mk;
                if (mk < 0) mk += MBIG;
                mj = SeedArray[ii];
            }
            for (int k = 1; k < 5; k++)
            {
                for (int i = 1; i < 56; i++)
                {
                    SeedArray[i] -= SeedArray[1 + (i + 30) % 55];
                    if (SeedArray[i] < 0) SeedArray[i] += MBIG;
                }
            }
            inext = 0;
            inextp = 21;
        }

        // copy constructor: creates a deep copy
        public PRNG_Basic(PRNG_Basic random_to_copy)
        {
            this.inext = random_to_copy.inext;
            this.inextp = random_to_copy.inextp;
            this.SeedArray = new int[56];
            for (int i = 0; i < 56; i++)
                this.SeedArray[i] = random_to_copy.SeedArray[i];
        }

        #endregion

        #region private methods
        protected virtual double Sample()
        {
            return (InternalSample() * (1.0 / MBIG));
        }

        private int InternalSample()
        {
            int retVal;
            int locINext = inext;
            int locINextp = inextp;

            if (++locINext >= 56) locINext = 1;
            if (++locINextp >= 56) locINextp = 1;

            retVal = SeedArray[locINext] - SeedArray[locINextp];

            if (retVal == MBIG) retVal--;
            if (retVal < 0) retVal += MBIG;

            SeedArray[locINext] = retVal;

            inext = locINext;
            inextp = locINextp;

            return retVal;
        }

        private double GetSampleForLargeRange()
        {
            // The distribution of double value returned by Sample 
            // is not distributed well enough for a large range.
            // If we use Sample for a range [Int32.MinValue..Int32.MaxValue)
            // We will end up getting even numbers only.

            int result = InternalSample();
            // Note we can't use addition here. The distribution will be bad if we do that.
            bool negative = (InternalSample() % 2 == 0) ? true : false;  // decide the sign based on second sample
            if (negative)
            {
                result = -result;
            }
            double d = result;
            d += (Int32.MaxValue - 1); // get a number in range [0 .. 2 * Int32MaxValue - 1)
            d /= 2 * (uint)Int32.MaxValue - 1;
            return d;
        }
        #endregion

        #region public methods
        public int Next()
        {
            return InternalSample();
        }

        public int Next(int minValue, int maxValue)
        {
            if (minValue > maxValue) throw new Exception("minValue must be smaller than maxValue");
            Contract.EndContractBlock();

            long range = (long)maxValue - minValue;
            if (range <= (long)Int32.MaxValue) 
                return ((int)(Sample() * range) + minValue);
            else
                return (int)((long)(GetSampleForLargeRange() * range) + minValue);
        }

        public int Next(int maxValue)
        {
            if (maxValue < 0) throw new Exception("maxvalue must be positive");
            Contract.EndContractBlock();

            return (int)(Sample() * maxValue);
        }

        public double NextDouble()
        {
            return Sample();
        }

        public void NextBytes(byte[] buffer)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");
            Contract.EndContractBlock();

            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = (byte)(InternalSample() % (Byte.MaxValue + 1));
        }


        public object Clone()
        {
            return new PRNG_Basic(this);
        }
        #endregion
    }



}