// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperatingSystem.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.
// </copyright>
// <summary>
// Represents the current operating system.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.ConsoleMvc.Platform;

using System.Runtime.InteropServices;

/// <summary>
/// Represents the current operating system.
/// </summary>
public static class OperatingSystem {
    /// <summary>
    /// Gets a value indicating whether current operating system is Windows.
    /// </summary>
    public static bool IsWindows =>
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    /// <summary>
    /// Gets a value indicating whether current operating system is macOS.
    /// </summary>
    public static bool IsMacOs =>
        RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

    /// <summary>
    /// Gets a value indicating whether current operating system is Linux.
    /// </summary>
    public static bool IsLinux =>
        RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
}