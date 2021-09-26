using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhiultaUI
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string value)
        {
            if (value == null) return true;
            return string.IsNullOrEmpty(value.Trim());
        }

        public static bool IsNumeric(this string s)
        {
            double output;
            return double.TryParse(s, out output);
        }

    }
}
