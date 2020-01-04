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
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Casasoft.Commodore.Basic
{
    /// <summary>
    /// Single line of Basic PRG class.
    /// </summary>
    public class BasicRow
    {
        /// <summary>
        /// Pointer to next row
        /// </summary>
        public UInt16 NextRowAddress;

        /// <summary>
        /// Row number
        /// </summary>
        public UInt16 LineNumber;

        /// <summary>
        /// Tokenized row
        /// </summary>
        public List<byte> Code;

        /// <summary>
        /// Constructor
        /// </summary>
        public BasicRow()
        {
            NextRowAddress = 0;
            LineNumber = 0;
            Code = new List<byte>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Ptr"></param>
        /// <param name="Line"></param>
        /// <param name="rawdata"></param>
        public BasicRow(UInt16 Ptr, UInt16 Line, byte[] rawdata) : this()
        {
            NextRowAddress = Ptr;
            LineNumber = Line;
            Code.AddRange(rawdata);
        }

        #region tokenizer
        /// <summary>
        /// Constructor with tokenizer
        /// </summary>
        /// <param name="Source"></param>
        public BasicRow(string Source) : this()
        {
            string cmd = Source.Trim();

            // scan for initial line number
            Regex linenum = new Regex(@"^\d+");
            Match linenumVal = linenum.Match(cmd);
            if (linenumVal.Success)
            {
                cmd = cmd.Substring(linenumVal.Length).Trim();
                LineNumber = Convert.ToUInt16(linenumVal.Value);
            }

            // tokens search loop
            while (cmd.Length > 0)
            {
                byte tk;
                if ((tk = FindToken(cmd)) > 0)
                {
                    cmd = StringUtils.StringMid(cmd, Tokens.List[tk].Length);
                    Code.Add(tk); 
                }
                else if (!StringUtils.IsAlpha(cmd[0]))
                {
                    // special char handling
                    switch (cmd[0])
                    {
                        case '"':
                            // Found quotes: search for closing ones or end of line
                            Code.Add((byte)cmd[0]);
                            cmd = cmd.Substring(1);
                            int cq = cmd.IndexOf('"');
                            string quoted;
                            if (cq >= 0)
                            {
                                quoted = StringUtils.StringLeft(cmd, cq + 1);
                                cmd = StringUtils.StringMid(cmd, cq + 1);
                            }
                            else
                            {
                                quoted = cmd;
                                cmd = string.Empty;
                            }
                            for (int j = 0; j < quoted.Length; j++) Code.Add((byte)quoted[j]);
                            break;
                        case '?':
                        case '+':
                        case '*':
                        case '/':
                        case '^':
                        case '<':
                        case '=':
                        case '>':
                            // single char tokens
                            Code.Add(Tokens.ReverseList[cmd.Substring(0, 1)]);
                            cmd = cmd.Substring(1);
                            break;
                        case '-':
                            // I don't know why minus must be processed alone 
                            Code.Add((byte)Token.Minus);
                            cmd = cmd.Substring(1);
                            break;
                        default:
                            // skip over
                            Code.Add((byte)cmd[0]);
                            cmd = cmd.Substring(1);
                            break;
                    }
                }
                else
                {
                    // Next char could by an id
                    // skip over
                    Code.Add((byte)cmd[0]);
                    cmd = cmd.Substring(1);
                }

            }

        }

        /// <summary>
        /// Gets a token index from initial part of the string
        /// </summary>
        /// <param name="cmdline"></param>
        /// <returns>0 if no token found</returns>
        protected byte FindToken(string cmdline)
        {
            byte token = 0;
            cmdline = cmdline.ToUpper();
            foreach (var tk in Tokens.List)
            {
                if (cmdline.Length >= tk.Value.Length)
                {
                    if (cmdline.Substring(0, tk.Value.Length) == tk.Value)
                    {
                        token = tk.Key;
                        break;
                    }
                }
            }
            return token;
        }

        #endregion

        /// <summary>
        /// Formats row in plain text
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(LineNumber.ToString());
            for (int j = 0; j < Code.Count; j++)
            {
                byte c = Code[j];
                if (c < 128)
                {
                    sb.Append((char)c);
                }
                else
                {
                    sb.Append(Tokens.List[c]);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Length of the line in Commodore format
        /// </summary>
        public int RawLength
        {
            get => Code.Count + 5;
        }

        /// <summary>
        /// Convert row in Commodore Basic format
        /// </summary>
        /// <returns></returns>
        public byte[] ToRaw()
        {
            byte[] ret = new byte[RawLength];
            ret[0] = Convert.ToByte(NextRowAddress % 256);
            ret[1] = Convert.ToByte(NextRowAddress / 256);
            ret[2] = Convert.ToByte(LineNumber % 256);
            ret[3] = Convert.ToByte(LineNumber / 256);
            ret[RawLength - 1] = 0;
            Array.Copy(Code.ToArray(), 0, ret, 4, Code.Count);
            return ret;
        }
    }
}
