﻿// <copyright file="SmtpStreamReader.cs" company="Rnwood.SmtpServer project contributors">
// Copyright (c) Rnwood.SmtpServer project contributors. All rights reserved.
// Licensed under the BSD license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace Rnwood.SmtpServer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>A stream writer which uses the correct \r\n line ending required for SMTP protocol.</summary>
    public class SmtpStreamReader : StreamReader
    {
        /// <summary>Initializes a new instance of the <see cref="SmtpStreamReader"/> class.</summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="encoding">The character encoding to use.</param>
        public SmtpStreamReader(Stream stream, Encoding encoding)
            : base(stream, encoding)
        {
        }
    }
}
