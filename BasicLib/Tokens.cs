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
        public static Dictionary<int, string> List;

        /// <summary>
        /// Tokens codes by description
        /// </summary>
        public static Dictionary<string, int> ReverseList;
        
        /// <summary>
        /// Constructor
        /// </summary>
        static Tokens()
        {
            List = new Dictionary<int, string>();
            List.Add((int)Token.End,    "END");
            List.Add((int)Token.For, "FOR");
            List.Add((int)Token.Next, "NEXT");
            List.Add((int)Token.Data, "DATA");
            List.Add((int)Token.InputSharp, "INPUT#");
            List.Add((int)Token.Input, "INPUT");
            List.Add((int)Token.Dim, "DIM");
            List.Add((int)Token.Read, "READ");
            List.Add((int)Token.Let, "LET");
            List.Add((int)Token.Goto, "GOTO");
            List.Add((int)Token.Run, "RUN");
            List.Add((int)Token.If, "IF");
            List.Add((int)Token.Restore, "RESTORE");
            List.Add((int)Token.Gosub, "GOSUB");
            List.Add((int)Token.Return, "RETURN");
            List.Add((int)Token.Rem, "REM");
            List.Add((int)Token.Stop, "STOP");
            List.Add((int)Token.On, "ON");
            List.Add((int)Token.Wait, "WAIT");
            List.Add((int)Token.Load, "LOAD");
            List.Add((int)Token.Save, "SAVE");
            List.Add((int)Token.Verify, "VERIFY");
            List.Add((int)Token.Def, "DEF");
            List.Add((int)Token.Poke, "POKE");
            List.Add((int)Token.PrintSharp, "PRINT#");
            List.Add((int)Token.Print, "PRINT");
            List.Add((int)Token.Cont, "CONT");
            List.Add((int)Token.List, "LIST");
            List.Add((int)Token.Clr, "CLR");
            List.Add((int)Token.Cmd, "CMD");
            List.Add((int)Token.Sys, "SYS");
            List.Add((int)Token.Open, "OPEN");
            List.Add((int)Token.Close, "CLOSE");
            List.Add((int)Token.Get, "GET");
            List.Add((int)Token.New, "NEW");
            List.Add((int)Token.Tab, "TAB(");
            List.Add((int)Token.To, "TO");
            List.Add((int)Token.Fn, "FN");
            List.Add((int)Token.Spc, "SPC(");
            List.Add((int)Token.Then, "THEN");
            List.Add((int)Token.Not, "NOT");
            List.Add((int)Token.Step, "STEP");
            List.Add((int)Token.Plus, "+");
            List.Add((int)Token.Minus, "−");
            List.Add((int)Token.Times, "*");
            List.Add((int)Token.Div, "/");
            List.Add((int)Token.Exponent, "^");
            List.Add((int)Token.And, "AND");
            List.Add((int)Token.Or, "OR");
            List.Add((int)Token.Major, ">");
            List.Add((int)Token.Equal, "=");
            List.Add((int)Token.Minor, "<");
            List.Add((int)Token.Sgn, "SGN");
            List.Add((int)Token.Int, "INT");
            List.Add((int)Token.Abs, "ABS");
            List.Add((int)Token.Usr, "USR");
            List.Add((int)Token.Fre, "FRE");
            List.Add((int)Token.Pos, "POS");
            List.Add((int)Token.Sqr, "SQR");
            List.Add((int)Token.Rnd, "RND");
            List.Add((int)Token.Log, "LOG");
            List.Add((int)Token.Exp, "EXP");
            List.Add((int)Token.Cos, "COS");
            List.Add((int)Token.Sin, "SIN");
            List.Add((int)Token.Tan, "TAN");
            List.Add((int)Token.Atn, "ATN");
            List.Add((int)Token.Peek, "PEEK");
            List.Add((int)Token.Len, "LEN");
            List.Add((int)Token.Str, "STR$");
            List.Add((int)Token.Val, "VAL");
            List.Add((int)Token.Asc, "ASC");
            List.Add((int)Token.Chr, "CHR$");
            List.Add((int)Token.Left, "LEFT$");
            List.Add((int)Token.Right, "RIGHT$");
            List.Add((int)Token.Mid, "MID$");
            List.Add((int)Token.Go, "GO");

            // Dizionario per le ricerche inverse
            ReverseList = new Dictionary<string, int>();
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
