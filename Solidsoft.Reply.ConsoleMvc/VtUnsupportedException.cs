// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VtUnsupportedException.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.
// </copyright>
// <summary>
// AN exception that indicates that VT sequences are not supported on the current platform.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.ConsoleMvc;

using System;

/// <summary>
/// AN exception that indicates that VT sequences are not supported on the current platform.
/// </summary>
public class VtUnsupportedException : SystemException {
    /// <summary>
    /// Initializes a new instance of the <see cref="VtUnsupportedException"/> class.
    /// </summary>
    public VtUnsupportedException() : base("VT sequence codes are not supported on this platform.") {
    }
}