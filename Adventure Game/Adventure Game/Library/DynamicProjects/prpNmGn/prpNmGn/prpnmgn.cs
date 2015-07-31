using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace prpNmGn
{
    public class PrpNmGn
    {
        public static string GenerateNameFromTable()
        {
            int pairs = PrpNmGnU.range(2, 4);

            string output = "";

            bool pass = false;
            bool nolengthcheck = false;

            while (pass == false)
            {
                nolengthcheck = false;
                pass = true;
                output = "";
                for (int i = 0; i <= pairs; i++)
                {
                    output += generatePair();
                    for (int y = 0; y < output.Length - 1; y++)
                    {
                        if (output[y] == '*')
                            output = output.Insert(y, PrpNmGnT.chartablev[PrpNmGnU.range(0, 4)]);
                        output = output.Replace("*", "");

                    }
                }

                for (int i = 0; i < output.Length; i++)
                {
                    if(PrpNmGnT.chartablec.Any(x => x.Equals(output[i].ToString())))
                    {
                        try
                        {
                            bool subpass = false;
                            int offset = 1;
                            while (subpass == false)
                            {                     
                                if (output[i + offset] == '!')
                                    offset += 1;
                                else
                                    subpass = true;
                            }
                            if (PrpNmGnT.chartablec.Any(x => x.Equals(output[i + offset].ToString())))
                            {
                                pass = false;
                                break;
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    if (PrpNmGnT.chartablei.Any(x => output.Contains(x)))
                    {
                        pass = false;
                        break;
                    }

                    if(output[i] == '_')
                        if(i != 0 && i != output.Length-1)
                        {
                            pass = false;
                            break;
                        }

                    if(output[i] == '!')
                        if(i == 0 || i == output.Length-1)
                        {
                            pass = false;
                            break;
                        }

                    //see if there are 80% more vowels than cons in the word
                    if((double)PrpNmGnT.chartablec.Count(x => output.Contains(x)) * 0.8 < PrpNmGnT.chartablev.Count(x => output.Contains(x)))
                    {
                        pass = false;
                        break;
                    }                
                    
                    if(output[i] == ' ')
                    {
                        nolengthcheck = true;
                    }
                }

                output = output.Replace("_", "");
                output = output.Replace("-", "");
                output = output.Replace("!", "");

                if (pass == true && output.Length > 7 && nolengthcheck == false)
                {
                    pass = false;
                }
            }

            return output;
        }

        internal static string generatePair()
        {
            string pattern = PrpNmGnT.chartablep[PrpNmGnU.range(0, PrpNmGnT.chartablep.Length-1)];

            string output = "";

            for(int i = 0; i < pattern.Length; i++)
            {
                if (pattern[i] == 'c')
                    output += PrpNmGnT.chartablec[PrpNmGnU.range(0, PrpNmGnT.chartablec.Length-1)];
                else
                    output += PrpNmGnT.chartablev[PrpNmGnU.range(0, PrpNmGnT.chartablev.Length-1)];
            }

            return output;
        }
    }
}
