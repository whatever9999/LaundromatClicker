using System;
using UnityEngine;

namespace NumberSystem
{
    public class NumberHandler : MonoBehaviour
    {
        public static string FormatNumber(string n, int decimalPlaces)
        {
            if (n == "0")
            {
                return "0";
            }

            string[] units = { " million", " billion", " trillion", " quadrillion", " quintillion", " sextillion", " septillion", " octillion", " nonillion", " decillion", " undecillion", " duodecillion", " tredecillion", " quattuordecillion", " quindecillion", " sexdecillion", " septendecillion", " octodecillion", " novemdecillion", " vigintillion", " unvigintillion", " duovigintillion", " tresvigintillion", " quattuorvigintillion", " quinquavigintillion", " sesvigintillion", " septemvigintillion", " octovigintillion", " novemvigintillion", " trigintillion", " untrigintillion", " duotrigintillion", " duotrigintillion", " trestrigintillion", " quattuortrigintillion", " quinquatrigintillion", " sestrigintillion", " septentrigintillion", " octotrigintillion", " noventrigintillion", " quadragintillion" };

            bool highnumber = false;
            bool infinite = false;
            int bignumber = 7;
            int unitIndex = -1;
            string unit = "";

            if (n.Length >= bignumber)
            {
                highnumber = true;

                while (n.Length >= bignumber)
                {
                    bignumber += 3; unitIndex++;
                }
                bignumber /= 3;

                n = n.Substring(0, n.Length - bignumber);
                try
                {
                    unit = units[unitIndex];
                }
                catch
                {
                    infinite = true;
                    n = "Infinite";
                    unit = "";
                }
            }

            if (highnumber & !infinite)
            {
                n = n[0] + "." + n.Substring(1, decimalPlaces);
            }

            return n + unit;
        }

        public static string IncreaseNumber(string n, string increase)
        {
            // Make increase the larger string
            if (n.Length > increase.Length)
            {
                string temp = n;
                n = increase;
                increase = temp;
            }

            string s = "";

            int lengthN = n.Length, lengthIncrease = increase.Length;

            // Reverse both of strings 
            char[] ch = n.ToCharArray();
            Array.Reverse(ch);
            n = new string(ch);
            char[] ch1 = increase.ToCharArray();
            Array.Reverse(ch1);
            increase = new string(ch1);

            int carry = 0;
            for (int i = 0; i < lengthN; i++)
            {
                int sum = ((int)(n[i] - '0') +
                        (int)(increase[i] - '0') + carry);
                s += (char)(sum % 10 + '0');

                // Calculate carry
                carry = sum / 10;
            }

            // Add remaining digits of larger number  
            for (int i = lengthN; i < lengthIncrease; i++)
            {
                int sum = ((int)(increase[i] - '0') + carry);
                s += (char)(sum % 10 + '0');
                carry = sum / 10;
            }

            // Add remaining carry  
            if (carry > 0)
                s += (char)(carry + '0');

            // reverse resultant string 
            char[] ch2 = s.ToCharArray();
            Array.Reverse(ch2);
            s = new string(ch2);

            return s;
        }

        public static string DecreaseNumber(string n, string decrease)
        {
            for (int i = 1; i < decrease.Length + 1; i++)
            {
                string nSubstring = n.Substring(n.Length - i, 1);
                string dSubstring = decrease.Substring(decrease.Length - i, 1);

                int nInt = Int32.Parse(nSubstring);
                int dInt = Int32.Parse(dSubstring);

                if (nInt < dInt)
                {
                    for (int j = i + 1; j < n.Length + 1; j++)
                    {
                        string nextNSubstring = n.Substring(n.Length - j, 1);
                        int nextInt = Int32.Parse(nextNSubstring);

                        if (nextInt != 0)
                        {
                            nextInt -= 1;
                            n = n.Substring(0, n.Length - j) + nextInt + n.Substring(n.Length - j + 1, j - 1);
                            nInt += 10;
                            break;
                        } else
                        {
                            n = n.Substring(0, n.Length - j) + 9 + n.Substring(n.Length - j + 1, j - 1);
                            
                        }
                    }
                } 

                int resultInt = nInt - dInt;
                n = n.Substring(0, n.Length - i) + resultInt + n.Substring(n.Length - i + 1, i - 1);
            }

            return n;
        }

        public static Nullable<bool> CompareNumbers(string a, string b)
        {
            Nullable<bool> aBigger = false;

            if(a.Length > b.Length)
            {
                aBigger = true;
            } else if (a.Length < b.Length)
            {
                aBigger = false;
            } else
            {
                int lengthA = a.Length, lengthB = b.Length;

                // Turn into arrays and reverse
                char[] aChar = a.ToCharArray();
                Array.Reverse(aChar);
                char[] bChar = b.ToCharArray();
                Array.Reverse(bChar);

                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i] > b[i])
                    {
                        aBigger = true;
                        break;
                    }
                    else if (a[i] < b[i])
                    {
                        aBigger = false;
                        break;
                    }
                    else
                    {
                        aBigger = null;
                    }
                }
            }

            return aBigger;
        }
    }
}
