﻿using Mecha.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Monarch.Tests.BaseClasses
{
    /// <summary>
    /// Test base class
    /// </summary>
    /// <typeparam name="TTestObject">The type of the test object.</typeparam>
    public abstract class TestBaseClass<TTestObject> : TestBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestBaseClass{TTestObject}"/> class.
        /// </summary>
        protected TestBaseClass()
        {
            ObjectType = null;
        }

        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        protected override Type? ObjectType { get; set; }

        /// <summary>
        /// Gets or sets the test object.
        /// </summary>
        /// <value>The test object.</value>
        protected TTestObject? TestObject { get; set; }

        /// <summary>
        /// Attempts to break the object.
        /// </summary>
        /// <returns>The async task.</returns>
        [Fact]
        public Task BreakObject()
        {
            return TestObject is null
                ? Task.CompletedTask
                : Mech.BreakAsync(TestObject, new Options
                {
                    MaxDuration = 1000,
                    ExceptionHandlers = new ExceptionHandler()
                    .IgnoreException<NotImplementedException>()
                    .IgnoreException<ArgumentOutOfRangeException>((_, __) => true)
                    .IgnoreException<ArgumentException>()
                    .IgnoreException<ObjectDisposedException>((_, __) => true)
                    .IgnoreException<Exception>(),
                    DiscoverInheritedMethods = false
                });
        }
    }

    /// <summary>
    /// Test base class
    /// </summary>
    public abstract class TestBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestBaseClass{TTestObject}"/> class.
        /// </summary>
        protected TestBaseClass()
        {
            lock (_LockObject)
            {
                _ = new ServiceCollection().AddCanisterModules();
                _ = Mech.Default;
            }
        }

        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        protected abstract Type? ObjectType { get; set; }

        /// <summary>
        /// The lock object
        /// </summary>
        private static readonly object _LockObject = new();

        /// <summary>
        /// Attempts to break the object.
        /// </summary>
        /// <returns>The async task.</returns>
        [Fact]
        public Task BreakType()
        {
            return ObjectType is null
                ? Task.CompletedTask
                : Mech.BreakAsync(ObjectType, new Options
                {
                    MaxDuration = 1000,
                    ExceptionHandlers = new ExceptionHandler()
                    .IgnoreException<NotImplementedException>()
                    .IgnoreException<ArgumentOutOfRangeException>((_, __) => true)
                    .IgnoreException<ArgumentException>((_, __) => true)
                    .IgnoreException<FormatException>((_, __) => true)
                    .IgnoreException<ObjectDisposedException>((_, __) => true)
                    .IgnoreException<EndOfStreamException>((_, __) => true)
                    .IgnoreException<OutOfMemoryException>((_, __) => true)
                });
        }
    }
}