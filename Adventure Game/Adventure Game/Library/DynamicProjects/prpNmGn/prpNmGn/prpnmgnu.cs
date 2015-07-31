using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace prpNmGn
{
    public class PrpNmGnU
    {
        /// <summary>
        /// inclusive
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        internal static Random rand = new Random();
        internal static int range(int min, int max)
        {
            
            return rand.Next(min, max + 1);
        }

        internal static string range(bool vow)
        {
            if(vow)
            {
                return PrpNmGnT.chartablev[range(0, 5)];
            }
            else
            {
                return PrpNmGnT.chartablec[range(0, 21)];
            }
        }
    }
}
