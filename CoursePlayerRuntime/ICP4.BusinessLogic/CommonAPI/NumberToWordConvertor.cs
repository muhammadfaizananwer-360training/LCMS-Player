using System;
using System.Collections.Generic;
using System.Text;
using ICP4.BusinessLogic.CacheManager;

namespace CommonAPI.Utility
{
    public class NumberToWordConvertor
    {
        public static string NumberToText(int number, bool IsRoman, string str_words, string str_words0, string str_words1, string str_words2, string str_words3)
        {
            if (number == 0) return "Zero";
            if (number == -2147483648) return "Minus Two Hundred and Fourteen Crore Seventy Four Lakh Eighty Three Thousand Six Hundred and Forty Eight";
            int[] num = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (number < 0)
            {
                sb.Append("Minus "); number = -number;
            }

            /*
            string[] words = { "", "First ", "Second ", "Third ", "Fourth ", "Fifth ", "Sixth ", "Seventh ", "Eight ", "Ninth " };
            string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
            string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
            string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };
            */
            string[] stringSeparators = new string[] { "," };
            string[] words = str_words.Split(stringSeparators, StringSplitOptions.None);
            string[] words0 = str_words0.Split(stringSeparators, StringSplitOptions.None);
            string[] words1 = str_words1.Split(stringSeparators, StringSplitOptions.None);
            string[] words2 = str_words2.Split(stringSeparators, StringSplitOptions.None);
            string[] words3 = str_words3.Split(stringSeparators, StringSplitOptions.None);

            num[0] = number % 1000; // units 
            num[1] = number / 1000;
            num[2] = number / 100000;
            num[1] = num[1] - 100 * num[2]; // thousands 
            num[3] = number / 10000000; // crores 
            num[2] = num[2] - 100 * num[3]; // lakhs 

            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i; break;
                }
            }

            for (int i = first; i >= 0; i--)
            {
                if (num[i] == 0)
                    continue;

                u = num[i] % 10; // ones 
                t = num[i] / 10;
                h = num[i] / 100; // hundreds 
                t = t - 10 * h; // tens 

                if (h > 0)
                    sb.Append(words0[h] + "Hundred ");

                if (u > 0 || t > 0)
                {
                    if (h > 0 && i == 0)
                        sb.Append("and ");

                    if (t == 0)
                    {
                        if (IsRoman == true)
                        {
                            sb.Append(words[u]);
                        }
                        else
                        {
                            sb.Append(words0[u]);
                        }
                    }
                    else if (t == 1)
                        sb.Append(words1[u]);

                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }

                if (i != 0)
                    sb.Append(words3[i - 1]);
            }

            return sb.ToString().TrimEnd();

        }

        public static string GetTimeString(int hours, int mins, int secs)
        {

            string timeStr = string.Empty;

            if (hours > 0)
                timeStr = timeStr + hours + " ";

            if (hours > 1)
                timeStr = timeStr + "hours ";
            else if (hours > 0)
                timeStr = timeStr + "hour ";

            if (mins > 0)
                timeStr = timeStr + mins + " ";

            if (mins > 1)
                timeStr = timeStr + "minutes ";
            else if (mins > 0)
                timeStr = timeStr + "minute ";

            if (secs > 0)
                timeStr = timeStr + secs + " ";

            if (secs > 1)
                timeStr = timeStr + "seconds";
            else if (secs > 0)
                timeStr = timeStr + "second";

            return timeStr.Trim();
        }

        public static string GetTimeInHourString(int hours, string hour_str, string hours_str)
        {
            string timeStr = string.Empty;

            if (hours > 0)
                timeStr = timeStr + hours + " ";

            if (hours > 1)
                timeStr = timeStr + hours_str + " ";
            else if (hours > 0)
                timeStr = timeStr + hour_str + " ";

            return timeStr.Trim();
        }

        public static string GetTimeInMinuteString(int hour, int mins, string hour_str, string hours_str, string min_str, string mins_str, string and_str)
        {
            string timeStr = string.Empty;

            if (hour > 0 && mins > 0)
                timeStr = timeStr + and_str + " ";

            if (mins > 0)
                timeStr = timeStr + mins + " ";

            if (mins > 1)
                timeStr = timeStr + mins_str + " ";
            else if (mins > 0)
                timeStr = timeStr + min_str + " ";

            if (hour <= 0 && mins <= 0)
            {
                timeStr = "0 " + min_str + " ";
            }

            return timeStr.Trim();
        }

    }
}
