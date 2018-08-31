/*
Copyright 2018 James Craig

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;

namespace Monarch.Interfaces
{
    /// <summary>
    /// Console writer
    /// </summary>
    public interface IConsoleWriter
    {
        /// <summary>
        /// Indents this instance.
        /// </summary>
        /// <returns>This</returns>
        IConsoleWriter Indent();

        /// <summary>
        /// Outdents this instance.
        /// </summary>
        /// <returns>This</returns>
        IConsoleWriter Outdent();

        /// <summary>
        /// Resets the color of the console.
        /// </summary>
        /// <returns>This</returns>
        IConsoleWriter ResetConsoleColor();

        /// <summary>
        /// Sets the color of the console.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>This</returns>
        IConsoleWriter SetConsoleColor(ConsoleColor color);

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        IConsoleWriter Write(bool value);

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        IConsoleWriter Write(char value);

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        IConsoleWriter Write(char[] value);

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        IConsoleWriter Write(decimal value);

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        IConsoleWriter Write(double value);

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        IConsoleWriter Write(float value);

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        IConsoleWriter Write(int value);

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        IConsoleWriter Write(long value);

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        IConsoleWriter Write(object value);

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        IConsoleWriter Write(string value);

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        IConsoleWriter Write(uint value);

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        IConsoleWriter Write(ulong value);

        /// <summary>
        /// Writes the text and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        IConsoleWriter WriteLine();

        /// <summary>
        /// Writes the text and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        IConsoleWriter WriteLine(bool value);

        /// <summary>
        /// Writes the text and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        IConsoleWriter WriteLine(char value);

        /// <summary>
        /// Writes the text and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        IConsoleWriter WriteLine(char[] value);

        /// <summary>
        /// Writes the text and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        IConsoleWriter WriteLine(decimal value);

        /// <summary>
        /// Writes the text and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        IConsoleWriter WriteLine(double value);

        /// <summary>
        /// Writes the text and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        IConsoleWriter WriteLine(float value);

        /// <summary>
        /// Writes the text and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        IConsoleWriter WriteLine(int value);

        /// <summary>
        /// Writes the text and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        IConsoleWriter WriteLine(long value);

        /// <summary>
        /// Writes the text and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        IConsoleWriter WriteLine(object value);

        /// <summary>
        /// Writes the text and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        IConsoleWriter WriteLine(string value);

        /// <summary>
        /// Writes the text and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        IConsoleWriter WriteLine(uint value);

        /// <summary>
        /// Writes the text and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        IConsoleWriter WriteLine(ulong value);

        /// <summary>
        /// Writes the separator.
        /// </summary>
        /// <returns>This</returns>
        IConsoleWriter WriteSeparator();
    }
}