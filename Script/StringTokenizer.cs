using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Script
{
    public class StringTokenizer
    {
        private char _ppc;
        public char PreviousProcessedCharacter { get { return _ppc; } }
        private string _pps;
        public string PreviousProcessedString { get { return _pps; } }
        private string _string;
        public string String { get { return _string; } set { _string = value; } }

        public StringTokenizer(string _string)
        {
            String = _string.Trim();
        }

        public string NextToken()
        {
            String = String.Trim();
            int blank = String.IndexOf(" ");
            int newline = String.IndexOf("\n");
            int returnline = String.IndexOf("\r");
            if (blank == -1) { blank = String.Length; }
            if (newline == -1) { newline = String.Length; }
            if (returnline == -1) { returnline = String.Length; }
            int min = Math.Min(blank, Math.Min(newline, returnline));
            if (min != -1)
            {
                string sub = String.Substring(0, min);
                _ppc = String[min];
                String = String.Substring(min).Trim();
                return sub;
            }
            return null;
        }

        public string NextToken(char delimiter)
        {
            String = String.Trim();
            int index = String.IndexOf(delimiter);
            if (index != -1)
            {
                if (index == 0)
                    return "";
                string sub = String.Substring(0, index);
                _ppc = String[index];
                String = String.Substring(index + 1);
                return sub;
            }
            return null;
        }

        public string NextToken(string delimiter)
        {
            String = String.Trim();
            int index = String.IndexOf(delimiter);
            if (index != -1)
            {
                if (index == 0)
                    return "";
                string sub = String.Substring(0, index);
                _pps = delimiter;
                String = String.Substring(index + delimiter.Length);
                return sub;
            }
            return null;
        }

        public string NextToken(params char[] delimiter)
        {
            String = String.Trim();
            int[] indexarr = new int[delimiter.Length];
            int min = -1;
            bool minus = true;
            for (int j = 0; j < delimiter.Length; j++)
            {
                indexarr[j] = String.IndexOf(delimiter[j]);
                if (min == -1)
                    min = indexarr[j];
                if (indexarr[j] != -1)
                {
                    minus = false;
                    if (indexarr[j] < min)
                    {
                        min = indexarr[j];
                    }
                }
            }
            if (!minus)
            {
                string sub = String.Substring(0, min);
                _ppc = String[min];
                String = String.Substring(min + 1);
                return sub;
            }
            return null;
        }

        public string NextToken(params string[] delimiter)
        {
            String = String.Trim();
            int[] indexarr = new int[delimiter.Length];
            int min = -1;
            int minj = -1;
            bool minus = true;
            for (int j = 0; j < delimiter.Length; j++)
            {
                indexarr[j] = String.IndexOf(delimiter[j]);
                if (min == -1)
                {
                    min = indexarr[j];
                    minj = j;
                }
                if (indexarr[j] != -1)
                {
                    minus = false;
                    if (indexarr[j] < min)
                    {
                        min = indexarr[j];
                        minj = j;
                    }
                }
            }
            if (!minus)
            {
                string sub = String.Substring(0, min);
                _pps = delimiter[minj];
                String = String.Substring(min + delimiter[minj].Length);
                return sub;
            }
            return null;
        }
    }
}
