using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Bela.Application.Utility
{
    public static class RandomGenerator
    {
        private static readonly ThreadLocal<Random> LocalRandom = new ThreadLocal<Random>(() =>
        {
            var seed = Interlocked.Increment(ref staticSeed) & 0x7FFFFFFF;
            return new Random(seed);
        });

        private static int staticSeed = Environment.TickCount;

        public static int Next(int min, int max) => LocalRandom.Value.Next(min, max);
    }
}
