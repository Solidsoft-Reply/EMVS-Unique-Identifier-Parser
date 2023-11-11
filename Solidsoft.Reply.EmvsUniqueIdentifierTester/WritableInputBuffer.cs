// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Solidsoft Reply Ltd.">
//   (c) 2018 Solidsoft Reply Ltd.
// </copyright>
// <license>
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </license>
// <summary>
// An input reader for console input based on a writable memory buffer.
// </summary>
// <remarks>
// This allows us to write content directly to
// the console input. bypassing the keyboard.  The class synchronises access to the memory buffer.  When writing to the 
// memory buffer (the base stream), use the Lock property to obtain the synchronisation lock object.
// </remarks>
// --------------------------------------------------------------------------------------------------------------------

namespace CozzTeatLok
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// An input reader for console input based on a writable memory buffer.
    /// </summary>
    public class WritableInputReader : TextReader
    {
        private object readerLock = new object();

        public WritableInputReader() : base()
        {

        }

        /// <summary>
        /// Gets a lock object for synchronising access to the base stream.
        /// </summary>
        /// <remarks>
        /// Don't use the base stream directly for synchronisation.  It has a weak identity.
        /// </remarks>
        public object Lock => this.readerLock;

        ///<inheritdoc />
        public override int Peek()
        {
            lock (readerLock)
            {
                return base.Peek();
            }
        }

        ///<inheritdoc />
        public override int Read()
        {
            lock (readerLock)
            {
                return base.Read();
            }
        }

        ///<inheritdoc />
        public override int Read(char[] buffer, int index, int count)
        {
            lock (readerLock)
            {
                return base.Read(buffer, index, count);
            }
        }

        ///<inheritdoc />
        public override int Read(Span<char> buffer)
        {
            lock (readerLock)
            {
                return base.Read(buffer);
            }
        }

        ///<inheritdoc />
        public override Task<int> ReadAsync(char[] buffer, int index, int count)
        {
            lock (readerLock)
            {
                return base.ReadAsync(buffer, index, count);
            }
        }

        ///<inheritdoc />
        public override ValueTask<int> ReadAsync(Memory<char> buffer, CancellationToken cancellationToken = new CancellationToken())
        {
            lock (readerLock)
            {
                return base.ReadAsync(buffer, cancellationToken);
            }
        }

        ///<inheritdoc />
        public override int ReadBlock(char[] buffer, int index, int count)
        {
            lock (readerLock)
            {
                return base.ReadBlock(buffer, index, count);
            }
        }

        ///<inheritdoc />
        public override int ReadBlock(Span<char> buffer)
        {
            lock (readerLock)
            {
                return base.ReadBlock(buffer);
            }
        }

        ///<inheritdoc />
        public override Task<int> ReadBlockAsync(char[] buffer, int index, int count)
        {
            lock (readerLock)
            {
                return base.ReadBlockAsync(buffer, index, count);
            }
        }

        ///<inheritdoc />
        public override ValueTask<int> ReadBlockAsync(Memory<char> buffer, CancellationToken cancellationToken = new CancellationToken())
        {
            lock (readerLock)
            {
                return base.ReadBlockAsync(buffer, cancellationToken);
            }
        }

        ///<inheritdoc />
        public override string? ReadLine()
        {
            lock (readerLock)
            {
                return base.ReadLine();
            }
        }

        ///<inheritdoc />
        public override Task<string?> ReadLineAsync()
        {
            lock (readerLock)
            {
                return base.ReadLineAsync();
            }
        }

        ///<inheritdoc />
        public override string ReadToEnd()
        {
            lock (readerLock)
            {
                return base.ReadToEnd();
            }
        }

        ///<inheritdoc />
        public override Task<string> ReadToEndAsync()
        {
            lock (readerLock)
            {
                return base.ReadToEndAsync();
            }
        }
    }
}