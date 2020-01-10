/// @file
/// Commodore 64 colors definition.
/// This file is part of Casasoft Commodore Utilities
/// 
/// @author
/// copyright (c) 2020 Roberto Ceccarelli - Casasoft  
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
using System.Drawing;
using System.Linq;
using System.Text;

namespace Casasoft.Commodore.Video
{
    /// <summary>
    /// Color names
    /// </summary>
    public enum C64Color
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Black, White, Red, Cyan, Purple, Green, Blue, Yellow,
        Orange, Brown, LightRed, DarkGray, Gray, LightGreen, LightBlue, LightGray
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
    
    /// <summary>
    /// Colors definitions
    /// </summary>
    public class ColorDefinition
    {
        /// <summary>
        /// Color name
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Char to select code in text
        /// </summary>
        public readonly char KeyCode;

        /// <summary>
        /// RGB values
        /// </summary>
        public readonly Color RGBColor;

        /// <summary>
        /// Color id for poke
        /// </summary>
        public readonly C64Color ColorId;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code"></param>
        /// <param name="keycode"></param>
        /// <param name="rgb"></param>
        public ColorDefinition(C64Color code, char keycode, Color rgb)
        {
            ColorId = code;
            Name = code.ToString();
            KeyCode = keycode;
            RGBColor = rgb;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code"></param>
        /// <param name="keycode"></param>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        public ColorDefinition(C64Color code, byte keycode, int red, int green, int blue) :
            this(code, (char)keycode, Color.FromArgb(red, green, blue))
        { }
    }

    /// <summary>
    /// Color definition list
    /// </summary>
    public class C64Colors
    {
        private static ColorDefinition[] Colors = 
        {
            new ColorDefinition(C64Color.Black, 144, 0, 0, 0),
            new ColorDefinition(C64Color.White, 5, 255, 255, 255),
            new ColorDefinition(C64Color.Red, 28, 136, 0, 0),
            new ColorDefinition(C64Color.Cyan, 159, 170, 255, 238),
            new ColorDefinition(C64Color.Purple, 156, 204, 68, 204),
            new ColorDefinition(C64Color.Green, 30, 0, 204, 85),
            new ColorDefinition(C64Color.Blue, 31, 0, 0, 170),
            new ColorDefinition(C64Color.Yellow, 158, 238, 238, 119),
            new ColorDefinition(C64Color.Orange, 129, 221, 136, 85),
            new ColorDefinition(C64Color.Brown, 149, 102, 68, 0),
            new ColorDefinition(C64Color.LightRed, 150, 255, 119, 119),
            new ColorDefinition(C64Color.DarkGray, 151, 51, 51, 51),
            new ColorDefinition(C64Color.Gray, 152, 119, 119, 119),
            new ColorDefinition(C64Color.LightGreen, 153, 170, 255, 102),
            new ColorDefinition(C64Color.LightBlue, 154, 0, 136, 255),
            new ColorDefinition(C64Color.LightGray, 155, 187, 187, 187)
        };

        /// <summary>
        /// Color definition by colors enum
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ColorDefinition this[C64Color index] => Colors[(int)index];

        /// <summary>
        /// Color definition by numeric id
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ColorDefinition this[int index] => Colors[index];

        /// <summary>
        /// Color definition by key code
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public ColorDefinition ByKeyCode(char c) {
            return Array.Find(Colors, x => x.KeyCode == c);
        }

        /// <summary>
        /// Colors list as array
        /// </summary>
        /// <returns></returns>
        public ColorDefinition[] ToArray() => Colors;
    }
}
