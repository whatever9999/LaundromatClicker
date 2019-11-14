using System;
using UnityEngine;

//Format, Add, Subtract and Compare large numerical strings
namespace NumberSystem
{
    public class NumberHandler : MonoBehaviour
    {
        //NOTE: numbers are always rounded down ("4736000" is "4.73 million")
        public static string FormatNumber(string n, int decimalPlaces)
        {
            //If the number is 0, then it is 0
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

            //If the number has more than bignumber characters (7 is a million, 9 is a billion etc.) then cut the end 3 characters off and update the unit to be fit how big it is
            if (n.Length >= bignumber)
            {
                highnumber = true;

                while (n.Length >= bignumber)
                {
                    bignumber += 3;
                    unitIndex++;
                }
                bignumber /= 3;

                n = n.Substring(0, n.Length - bignumber);
                //If the number is infinite set the whole string to be 'Infinite'
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

            //If the number is a big number it will need a decimal point
            //Examples of format: '5.60 million, '4.34 octillion', '7484'
            if (highnumber & !infinite)
            {
                int decimalPoint = n.Length - 3 - (2 * unitIndex);
                n = n.Substring(0, decimalPoint) + "." + n.Substring(decimalPoint, decimalPlaces);
            }

            //Return the number as a string (e.g. 4.56 or 23728) and its unit (e.g. million or '' respectively)
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
            //For each character in the decrease string
            for (int i = 1; i < decrease.Length + 1; i++)
            {
                //Get last characters of the number string and decrease string
                string nSubstring = n.Substring(n.Length - i, 1);
                string dSubstring = decrease.Substring(decrease.Length - i, 1);

                //Parse the last characters into numbers
                int nInt = Int32.Parse(nSubstring);
                int dInt = Int32.Parse(dSubstring);

                //If the number character is less than the decrease
                if (nInt < dInt)
                {
                    //For each character to the left of the current character
                    for (int j = i + 1; j < n.Length + 1; j++)
                    {
                        //Get the next number along (to the left)
                        string nextNSubstring = n.Substring(n.Length - j, 1);
                        int nextInt = Int32.Parse(nextNSubstring);

                        //If it's not 0 then take 1 from it (a unit, 10th, 100th etc.)
                        //Then update the string to account for this change
                        //And add 10 to the number
                        if (nextInt != 0)
                        {
                            nextInt -= 1;
                            n = n.Substring(0, n.Length - j) + nextInt + n.Substring(n.Length - j + 1, j - 1);
                            nInt += 10;
                            break;
                        } else
                        {
                            //Set the next number along to be 9 (as we will go check the next one instead)
                            n = n.Substring(0, n.Length - j) + 9 + n.Substring(n.Length - j + 1, j - 1);
                        }
                    }
                } 

                //Take the decrease character away from the number character and update the string
                int resultInt = nInt - dInt;
                n = n.Substring(0, n.Length - i) + resultInt + n.Substring(n.Length - i + 1, i - 1);
            }

            return n;
        }

        //Returns true if a is bigger, null if the same size and false if b is bigger
        public static Nullable<bool> CompareNumbers(string a, string b)
        {
            Nullable<bool> aBigger = false;

            //If a is longer than b then it's bigger (the same the other way around)
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

                //The first number from the left to be bigger than its counterpart in the other string shows which string is bigger than the other 
                //e.g. 1100, 1000 - the second character from the left shows that the first is the bigger number
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
