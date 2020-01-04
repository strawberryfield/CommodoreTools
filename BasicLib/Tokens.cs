/// @file
/// Commodore Basic tokens list class.
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
using System.Collections.Generic;

namespace Casasoft.Commodore.Basic
{
    /// <summary>
    /// Tokens lists
    /// </summary>
    public static class Tokens
    {
        /// <summary>
        /// Tokens descriptions by code
        /// </summary>
        public static Dictionary<byte, string> List;

        /// <summary>
        /// Tokens codes by description
        /// </summary>
        public static Dictionary<string, byte> ReverseList;
        
        /// <summary>
        /// Constructor
        /// </summary>
        static Tokens()
        {
            List = new Dictionary<byte, string>
            {
                { (byte)Token.End, "END" },
                { (byte)Token.For, "FOR" },
                { (byte)Token.Next, "NEXT" },
                { (byte)Token.Data, "DATA" },
                { (byte)Token.InputSharp, "INPUT#" },
                { (byte)Token.Input, "INPUT" },
                { (byte)Token.Dim, "DIM" },
                { (byte)Token.Read, "READ" },
                { (byte)Token.Let, "LET" },
                { (byte)Token.Goto, "GOTO" },
                { (byte)Token.Run, "RUN" },
                { (byte)Token.If, "IF" },
                { (byte)Token.Restore, "RESTORE" },
                { (byte)Token.Gosub, "GOSUB" },
                { (byte)Token.Return, "RETURN" },
                { (byte)Token.Rem, "REM" },
                { (byte)Token.Stop, "STOP" },
                { (byte)Token.On, "ON" },
                { (byte)Token.Wait, "WAIT" },
                { (byte)Token.Load, "LOAD" },
                { (byte)Token.Save, "SAVE" },
                { (byte)Token.Verify, "VERIFY" },
                { (byte)Token.Def, "DEF" },
                { (byte)Token.Poke, "POKE" },
                { (byte)Token.PrintSharp, "PRINT#" },
                { (byte)Token.Print, "PRINT" },
                { (byte)Token.Cont, "CONT" },
                { (byte)Token.List, "LIST" },
                { (byte)Token.Clr, "CLR" },
                { (byte)Token.Cmd, "CMD" },
                { (byte)Token.Sys, "SYS" },
                { (byte)Token.Open, "OPEN" },
                { (byte)Token.Close, "CLOSE" },
                { (byte)Token.Get, "GET" },
                { (byte)Token.New, "NEW" },
                { (byte)Token.Tab, "TAB(" },
                { (byte)Token.To, "TO" },
                { (byte)Token.Fn, "FN" },
                { (byte)Token.Spc, "SPC(" },
                { (byte)Token.Then, "THEN" },
                { (byte)Token.Not, "NOT" },
                { (byte)Token.Step, "STEP" },
                { (byte)Token.Plus, "+" },
                { (byte)Token.Minus, "−" },
                { (byte)Token.Times, "*" },
                { (byte)Token.Div, "/" },
                { (byte)Token.Exponent, "^" },
                { (byte)Token.And, "AND" },
                { (byte)Token.Or, "OR" },
                { (byte)Token.Major, ">" },
                { (byte)Token.Equal, "=" },
                { (byte)Token.Minor, "<" },
                { (byte)Token.Sgn, "SGN" },
                { (byte)Token.Int, "INT" },
                { (byte)Token.Abs, "ABS" },
                { (byte)Token.Usr, "USR" },
                { (byte)Token.Fre, "FRE" },
                { (byte)Token.Pos, "POS" },
                { (byte)Token.Sqr, "SQR" },
                { (byte)Token.Rnd, "RND" },
                { (byte)Token.Log, "LOG" },
                { (byte)Token.Exp, "EXP" },
                { (byte)Token.Cos, "COS" },
                { (byte)Token.Sin, "SIN" },
                { (byte)Token.Tan, "TAN" },
                { (byte)Token.Atn, "ATN" },
                { (byte)Token.Peek, "PEEK" },
                { (byte)Token.Len, "LEN" },
                { (byte)Token.Str, "STR$" },
                { (byte)Token.Val, "VAL" },
                { (byte)Token.Asc, "ASC" },
                { (byte)Token.Chr, "CHR$" },
                { (byte)Token.Left, "LEFT$" },
                { (byte)Token.Right, "RIGHT$" },
                { (byte)Token.Mid, "MID$" },
                { (byte)Token.Go, "GO" }
            };

            // Dizionario per le ricerche inverse
            ReverseList = new Dictionary<string, byte>();
            foreach(var token in List)
            {
                ReverseList.Add(token.Value, token.Key);
            }
            ReverseList.Add("?", 153);
        }

    }

    /// <summary>
    /// Symbolic tokens values
    /// </summary>
    public enum Token {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        End = 128,
        For,
        Next,
        Data,
        InputSharp,
        Input,
        Dim,
        Read,
        Let,
        Goto,
        Run,
        If,
        Restore,
        Gosub,
        Return,
        Rem,
        Stop,
        On,
        Wait,
        Load,
        Save,
        Verify,
        Def,
        Poke,
        PrintSharp,
        Print,
        Cont,
        List,
        Clr,
        Cmd,
        Sys,
        Open,
        Close,
        Get,
        New,
        Tab,
        To,
        Fn,
        Spc,
        Then,
        Not,
        Step,
        Plus,
        Minus,
        Times,
        Div,
        Exponent,
        And,
        Or,
        Major,
        Equal,
        Minor,
        Sgn,
        Int,
        Abs,
        Usr,
        Fre,
        Pos,
        Sqr,
        Rnd,
        Log,
        Exp,
        Cos,
        Sin,
        Tan,
        Atn,
        Peek,
        Len,
        Str,
        Val,
        Asc,
        Chr,
        Left,
        Right,
        Mid,
        Go
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }

}
