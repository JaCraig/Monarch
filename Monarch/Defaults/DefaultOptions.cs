﻿/*
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

using Monarch.Interfaces;

namespace Monarch.Defaults
{
    /// <summary>
    /// Default options
    /// </summary>
    /// <seealso cref="IOptions"/>
    public class DefaultOptions : IOptions
    {
        /// <summary>
        /// Gets the command prefix.
        /// </summary>
        /// <value>The command prefix.</value>
        public string CommandPrefix { get; } = "";

        /// <summary>
        /// Gets the flag prefix.
        /// </summary>
        /// <value>The flag prefix.</value>
        public string FlagPrefix { get; } = "-";

        /// <summary>
        /// Gets the indent amount.
        /// </summary>
        /// <value>The indent amount.</value>
        public int IndentAmount { get; } = 4;
    }
}