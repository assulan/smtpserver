// <copyright file="FileMessage.cs" company="Rnwood.SmtpServer project contributors">
// Copyright (c) Rnwood.SmtpServer project contributors. All rights reserved.
// Licensed under the BSD license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace Rnwood.SmtpServer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="FileMessage" />
    /// </summary>
    public class FileMessage : IMessage
    {
        private readonly FileInfo file;

        private readonly bool keepOnDispose;

        private readonly List<string> recipients = new List<string>();

        private bool disposedValue = false; // To detect redundant calls

        /// <summary>
        /// Initializes a new instance of the <see cref="FileMessage"/> class.
        /// </summary>
        /// <param name="file">The file<see cref="FileInfo"/></param>
        /// <param name="keepOnDispose">The keepOnDispose<see cref="bool"/></param>
        public FileMessage(FileInfo file, bool keepOnDispose)
        {
            this.file = file;
            this.keepOnDispose = keepOnDispose;
        }

        /// <summary>
        /// Gets the DeclaredMessageSize
        /// </summary>
        public long? DeclaredMessageSize { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether EightBitTransport
        /// </summary>
        public bool EightBitTransport { get; internal set; }

        /// <summary>
        /// Gets the From
        /// </summary>
        public string From { get; internal set; }

        /// <summary>
        /// Gets the ReceivedDate
        /// </summary>
        public DateTime ReceivedDate { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether SecureConnection
        /// </summary>
        public bool SecureConnection { get; internal set; }

        /// <summary>
        /// Gets the Session
        /// </summary>
        public ISession Session { get; internal set; }

        /// <summary>
        /// Gets the To
        /// </summary>
        public IReadOnlyCollection<string> Recipients => this.RecipientsList.AsReadOnly();

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <value>
        /// The file.
        /// </value>
        internal FileInfo File => this.file;

        /// <summary>
        /// Gets the recipients list.
        /// </summary>
        /// <value>
        /// The recipients list.
        /// </value>
        internal List<string> RecipientsList => this.recipients;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets a stream which returns the message data.
        /// </summary>
        /// <returns>
        /// A <see cref="Task{T}" /> representing the async operation
        /// </returns>
        public Task<Stream> GetData()
        {
            return Task.FromResult<Stream>(new FileStream(this.File.FullName, FileMode.Open, FileAccess.Read, FileShare.Delete | FileShare.Read));
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    if (!this.keepOnDispose && this.File.Exists)
                    {
                        this.File.Delete();
                    }
                }

                this.disposedValue = true;
            }
        }
    }
}
