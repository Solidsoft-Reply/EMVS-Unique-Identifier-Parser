// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinuxClipboard.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.
// </copyright>
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
public static class LinuxClipboard {
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
    public static void SetText(string text) {
        var tempFileName = Path.GetRandomFileName();
        File.WriteAllText(tempFileName, text);

        try {
            BashRunner.Run($"cat {tempFileName} | xclip");
        }
        finally {
            File.Delete(tempFileName);
        }
    }
}

/// <summary>
/// Runs bash command lines.
/// </summary>
public static class BashRunner {
    /// <summary>
    /// Runs a command line in a bash terminal process.
    /// </summary>
    /// <param name="commandLine">The command line</param>
    /// <returns>The output of the command.</returns>
    public static string Run(string commandLine) {
        var errorBuilder = new StringBuilder();
        var outputBuilder = new StringBuilder();
        var arguments = $"-c \"{commandLine}\"";

        using var process = new Process();
        process.StartInfo = new ProcessStartInfo {
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

        if (!process.WaitForExit(500)) {
            var timeoutError = $"""
                                Process timed out. Command line: bash {arguments}.
                                Output: {outputBuilder}
                                Error: {errorBuilder}
                                """;
#pragma warning disable S112 // General or reserved exceptions should never be thrown
            throw new Exception(timeoutError);
#pragma warning restore S112 // General or reserved exceptions should never be thrown
        }
        if (process.ExitCode == 0) {
            return outputBuilder.ToString();
        }

        var error = $"""
                     Could not execute process. Command line: bash {arguments}.
                     Output: {outputBuilder}
                     Error: {errorBuilder}
                     """;
#pragma warning disable S112 // General or reserved exceptions should never be thrown
        throw new Exception(error);
#pragma warning restore S112 // General or reserved exceptions should never be thrown
    }
}