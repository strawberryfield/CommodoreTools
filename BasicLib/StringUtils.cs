/// @file
/// Single line of Basic PRG class.
/// This file is part of Casasoft Commodore Utilities
/// 
/// @author
/// copyright (c) 2019-2020 Roberto Ceccarelli - Casasoft  
/// http://strawberryfield.altervista.org 
/// 
/// @copyright
/// Casasoft Commodore Utilities is free software: 
/// you can redistribute it and/or modify it
/// under the terms of the GNU General Public License as published by
/// the Free Software Foundation, either version 3 of the License, or
/// (at your option) any later version.
/// 
/// You should have received a copy of the GNU General Public License
/// along with Casasoft Commodore Utilities.  
/// If not, see http://www.gnu.org/licenses/
/// 
/// @remark
/// Casasoft Commodore Utilities is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
/// See the GNU General Public License for more details.
/// 

using System;
using System.Runtime.CompilerServices;

namespace Casasoft.Commodore.Basic
{
    /// <summary>
    /// Common string functions
    /// </summary>
    public static class StringUtils
    {
        /// <summary>
        /// Left part of c# string without exceptions
        /// </summary>
        /// <param name="s"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string StringLeft(string s, int len)
        {
            return ((s.Length >= len) && (len > 0) ? s.Substring(0, len) : "");
        }

        /// <summary>
        /// Substring catching exceptions
        /// </summary>
        /// <param name="s"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static string StringMid(string s, int start)
        {
            return ((s.Length > start) && (start > 0) ? s.Substring(start) : "");
        }

        /// <summary>
        /// Tests if c is alpha
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlpha(char c)
        {
            return ('A' <= c && c <= 'Z') || ('a' <= c && c <= 'z');
        }

        /// <summary>
        /// Tests if c is alpha or digit
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlphaOrDigit(char c)
        {
            return IsAlpha(c) || IsDigit(c);
        }

        /// <summary>
        /// Tests if c is alpha or digit
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDigit(char c)
        {
            return ('0' <= c && c <= '9');
        }

        /// <summary>
        /// BASIC LEFT$
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string Left(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            maxLength = Math.Abs(maxLength);

            return (value.Length <= maxLength
                   ? value
                   : value.Substring(0, maxLength)
                   );
        }

        /// <summary>
        /// BASIC RIGHT$
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string Right(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            maxLength = Math.Abs(maxLength);

            return (value.Length <= maxLength
                   ? value
                   : value.Substring(value.Length - maxLength)
                   );
        }

        /// <summary>
        /// BASIC MID$
        /// </summary>
        /// <param name="value"></param>
        /// <param name="start"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string Mid(string value, int start, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            maxLength = Math.Abs(maxLength);
            start = Math.Abs(start);
            start = (start == 0 ? 1 : start);

            if(start > value.Length)
            {
                return "";
            }
            value = value.Substring(start - 1);

            return (value.Length <= maxLength
                   ? value
                   : value.Substring(0, maxLength)
                   );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static string Mid(string value, int start)
        {
            return Mid(value, start, value.Length);
        }


    }
}
