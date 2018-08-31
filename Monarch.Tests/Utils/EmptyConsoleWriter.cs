using Monarch.Interfaces;
using System;

namespace Monarch.Tests.Utils
{
    public class EmptyConsoleWriter : IConsoleWriter
    {
        public IConsoleWriter Indent()
        {
            return this;
        }

        public IConsoleWriter Outdent()
        {
            return this;
        }

        public IConsoleWriter ResetConsoleColor()
        {
            return this;
        }

        public IConsoleWriter SetConsoleColor(ConsoleColor color)
        {
            return this;
        }

        public IConsoleWriter Write(bool value)
        {
            return this;
        }

        public IConsoleWriter Write(char value)
        {
            return this;
        }

        public IConsoleWriter Write(char[] value)
        {
            return this;
        }

        public IConsoleWriter Write(decimal value)
        {
            return this;
        }

        public IConsoleWriter Write(double value)
        {
            return this;
        }

        public IConsoleWriter Write(float value)
        {
            return this;
        }

        public IConsoleWriter Write(int value)
        {
            return this;
        }

        public IConsoleWriter Write(long value)
        {
            return this;
        }

        public IConsoleWriter Write(object value)
        {
            return this;
        }

        public IConsoleWriter Write(string value)
        {
            return this;
        }

        public IConsoleWriter Write(uint value)
        {
            return this;
        }

        public IConsoleWriter Write(ulong value)
        {
            return this;
        }

        public IConsoleWriter WriteLine()
        {
            return this;
        }

        public IConsoleWriter WriteLine(bool value)
        {
            return this;
        }

        public IConsoleWriter WriteLine(char value)
        {
            return this;
        }

        public IConsoleWriter WriteLine(char[] value)
        {
            return this;
        }

        public IConsoleWriter WriteLine(decimal value)
        {
            return this;
        }

        public IConsoleWriter WriteLine(double value)
        {
            return this;
        }

        public IConsoleWriter WriteLine(float value)
        {
            return this;
        }

        public IConsoleWriter WriteLine(int value)
        {
            return this;
        }

        public IConsoleWriter WriteLine(long value)
        {
            return this;
        }

        public IConsoleWriter WriteLine(object value)
        {
            return this;
        }

        public IConsoleWriter WriteLine(string value)
        {
            return this;
        }

        public IConsoleWriter WriteLine(uint value)
        {
            return this;
        }

        public IConsoleWriter WriteLine(ulong value)
        {
            return this;
        }

        public IConsoleWriter WriteSeparator()
        {
            return this;
        }
    }
}