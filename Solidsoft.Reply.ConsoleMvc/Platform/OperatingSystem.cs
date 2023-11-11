// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperatingSystem.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.
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
// Represents the current operating system.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.ConsoleMvc.Platform;

using System.Runtime.InteropServices;

/// <summary>
/// Represents the current operating system.
/// </summary>
public static class OperatingSystem
{
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