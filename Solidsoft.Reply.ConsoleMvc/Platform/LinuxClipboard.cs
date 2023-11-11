// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinuxClipboard.cs" company="Solidsoft Reply Ltd.">
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
// Copies text to the Linux clipboard.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.ConsoleMvc.Platform;

using System;
using System.Text;
using System.Diagnostics;
using System.IO;

/// <summary>
/// Copies text to the Linux clipboard.
/// </summary>
public static class LinuxClipboard
{
    /// <summary>
    /// Copies text to the clipboard in a Linux graphical desktop environment
    /// using xclip.
    /// </summary>
    /// <param name="text">The text to be copied.</param>
    /// <remarks>
    /// xclip isn't standard kit with many Linux distributions. To see if it's installed
    /// on the computer, open a terminal window and type which xclip. If that command returns
    /// output like /usr/bin/xclip, then xclip is installed. Otherwise, install xclip.
    /// </remarks>
    public static void SetText(string text)
    {
        var tempFileName = Path.GetTempFileName();
        File.WriteAllText(tempFileName, text);

        try
        {
            BashRunner.Run($"cat {tempFileName} | xclip");
        }
        finally
        {
            File.Delete(tempFileName);
        }
    }
}

/// <summary>
/// Runs bash command lines.
/// </summary>
public static class BashRunner
{
    /// <summary>
    /// Runs a command line in a bash terminal process.
    /// </summary>
    /// <param name="commandLine">The command line</param>
    /// <returns>The output of the command.</returns>
    public static string Run(string commandLine)
    {
        var errorBuilder = new StringBuilder();
        var outputBuilder = new StringBuilder();
        var arguments = $"-c \"{commandLine}\"";

        using var process = new Process();
        process.StartInfo = new ProcessStartInfo
        {
            FileName = "bash",
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = false
        };
        process.Start();
        process.OutputDataReceived += (_, args) => { outputBuilder.AppendLine(args.Data); };
        process.BeginOutputReadLine();
        process.ErrorDataReceived += (_, args) => { errorBuilder.AppendLine(args.Data); };
        process.BeginErrorReadLine();

        if (!process.WaitForExit(500))
        {
            var timeoutError = $"""
                                Process timed out. Command line: bash {arguments}.
                                Output: {outputBuilder}
                                Error: {errorBuilder}
                                """;
            throw new Exception(timeoutError);
        }
        if (process.ExitCode == 0)
        {
            return outputBuilder.ToString();
        }

        var error = $"""
                     Could not execute process. Command line: bash {arguments}.
                     Output: {outputBuilder}
                     Error: {errorBuilder}
                     """;
        throw new Exception(error);
    }
}