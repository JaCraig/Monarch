using BigBook;
using Monarch.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Monarch.Defaults
{
    /// <summary>
    /// Default console writer
    /// </summary>
    /// <seealso cref="Monarch.Interfaces.IConsoleWriter"/>
    /// <seealso cref="IConsoleWriter"/>
    public class DefaultConsoleWriter : IConsoleWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultConsoleWriter"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public DefaultConsoleWriter(IEnumerable<IOptions> options)
        {
            ConsoleWidth = Console.BufferWidth > 10 ? Console.BufferWidth : DefaultWidth;
            Separator = new string('-', ConsoleWidth);
            Options = options.FirstOrDefault(x => !(x is DefaultOptions)) ?? new DefaultOptions();
        }

        /// <summary>
        /// The default width
        /// </summary>
        private const int DefaultWidth = 120;

        /// <summary>
        /// Gets the width of the console.
        /// </summary>
        /// <value>The width of the console.</value>
        public int ConsoleWidth { get; }

        /// <summary>
        /// Gets the current indent.
        /// </summary>
        /// <value>The current indent.</value>
        public int CurrentIndent { get; private set; }

        /// <summary>
        /// Gets the separator.
        /// </summary>
        /// <value>The separator.</value>
        public string Separator { get; }

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <value>The options.</value>
        private IOptions Options { get; }

        /// <summary>
        /// Indents this instance.
        /// </summary>
        /// <returns>This</returns>
        public IConsoleWriter Indent()
        {
            CurrentIndent += Options.IndentAmount;
            if (CurrentIndent >= ConsoleWidth)
                Outdent();
            return this;
        }

        /// <summary>
        /// Outdents this instance.
        /// </summary>
        /// <returns>This</returns>
        public IConsoleWriter Outdent()
        {
            CurrentIndent -= Options.IndentAmount;
            if (CurrentIndent < 0)
                CurrentIndent = 0;
            return this;
        }

        /// <summary>
        /// Resets the color of the console.
        /// </summary>
        /// <returns>This.</returns>
        public IConsoleWriter ResetConsoleColor()
        {
            Console.ResetColor();
            return this;
        }

        /// <summary>
        /// Sets the color of the console.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>This.</returns>
        public IConsoleWriter SetConsoleColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            return this;
        }

        /// <summary>
        /// Writes the text and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        public IConsoleWriter Write(string value)
        {
            Console.Write(value);
            return this;
        }

        /// <summary>
        /// Writes the value and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        public IConsoleWriter Write(bool value)
        {
            Console.Write(value);
            return this;
        }

        /// <summary>
        /// Writes the value and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        public IConsoleWriter Write(char value)
        {
            Console.Write(value);
            return this;
        }

        /// <summary>
        /// Writes the value and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        public IConsoleWriter Write(char[] value)
        {
            Console.Write(value);
            return this;
        }

        /// <summary>
        /// Writes the value and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        public IConsoleWriter Write(decimal value)
        {
            Console.Write(value);
            return this;
        }

        /// <summary>
        /// Writes the value and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        public IConsoleWriter Write(double value)
        {
            Console.Write(value);
            return this;
        }

        /// <summary>
        /// Writes the value and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        public IConsoleWriter Write(float value)
        {
            Console.Write(value);
            return this;
        }

        /// <summary>
        /// Writes the value and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        public IConsoleWriter Write(int value)
        {
            Console.Write(value);
            return this;
        }

        /// <summary>
        /// Writes the value and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        public IConsoleWriter Write(long value)
        {
            Console.Write(value);
            return this;
        }

        /// <summary>
        /// Writes the value and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        public IConsoleWriter Write(object value)
        {
            Console.Write(value);
            return this;
        }

        /// <summary>
        /// Writes the value and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        public IConsoleWriter Write(uint value)
        {
            Console.Write(value);
            return this;
        }

        /// <summary>
        /// Writes the value and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        public IConsoleWriter Write(ulong value)
        {
            Console.Write(value);
            return this;
        }

        /// <summary>
        /// Writes a new line.
        /// </summary>
        /// <returns>This</returns>
        public IConsoleWriter WriteLine()
        {
            Console.WriteLine();
            return this;
        }

        /// <summary>
        /// Writes the text and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        public IConsoleWriter WriteLine(string value)
        {
            var Lines = BreakIntoLines(value);
            for (int i = 0; i < Lines.Length; i++)
            {
                Console.WriteLine(Lines[i]);
            }
            return this;
        }

        /// <summary>
        /// Writes the value and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        public IConsoleWriter WriteLine(bool value)
        {
            WriteIndent();
            Console.WriteLine(value);
            return this;
        }

        /// <summary>
        /// Writes the value and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        public IConsoleWriter WriteLine(char value)
        {
            WriteIndent();
            Console.WriteLine(value);
            return this;
        }

        /// <summary>
        /// Writes the value and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        public IConsoleWriter WriteLine(char[] value)
        {
            WriteIndent();
            Console.WriteLine(value);
            return this;
        }

        /// <summary>
        /// Writes the value and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        public IConsoleWriter WriteLine(decimal value)
        {
            WriteIndent();
            Console.WriteLine(value);
            return this;
        }

        /// <summary>
        /// Writes the value and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        public IConsoleWriter WriteLine(double value)
        {
            WriteIndent();
            Console.WriteLine(value);
            return this;
        }

        /// <summary>
        /// Writes the value and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        public IConsoleWriter WriteLine(float value)
        {
            WriteIndent();
            Console.WriteLine(value);
            return this;
        }

        /// <summary>
        /// Writes the value and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        public IConsoleWriter WriteLine(int value)
        {
            WriteIndent();
            Console.WriteLine(value);
            return this;
        }

        /// <summary>
        /// Writes the value and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        public IConsoleWriter WriteLine(long value)
        {
            WriteIndent();
            Console.WriteLine(value);
            return this;
        }

        /// <summary>
        /// Writes the value and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        public IConsoleWriter WriteLine(object value)
        {
            WriteIndent();
            Console.WriteLine(value);
            return this;
        }

        /// <summary>
        /// Writes the value and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        public IConsoleWriter WriteLine(uint value)
        {
            WriteIndent();
            Console.WriteLine(value);
            return this;
        }

        /// <summary>
        /// Writes the value and ends with a new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This</returns>
        public IConsoleWriter WriteLine(ulong value)
        {
            WriteIndent();
            Console.WriteLine(value);
            return this;
        }

        /// <summary>
        /// Writes the separator.
        /// </summary>
        /// <returns>This</returns>
        public IConsoleWriter WriteSeparator()
        {
            Console.WriteLine(Separator);
            return this;
        }

        /// <summary>
        /// Breaks the input into lines.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The resulting lines</returns>
        private string[] BreakIntoLines(string input)
        {
            if (string.IsNullOrEmpty(input)) return new string[0];
            var lines = new List<string>();
            while (input.Length > 0)
            {
                var Width = ConsoleWidth - CurrentIndent;
                Width = input.Length > Width ? Width : input.Length;

                lines.Add(new string(' ', CurrentIndent) + input.Left(Width));
                input = input.Remove(0, Width);
            }
            return lines.ToArray();
        }

        /// <summary>
        /// Writes the indent.
        /// </summary>
        private void WriteIndent()
        {
            Console.Write(new string(' ', CurrentIndent));
        }
    }
}