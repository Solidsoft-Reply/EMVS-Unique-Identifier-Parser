// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OsxClipboard.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.
// </copyright>
// <summary>
// Copies text to the macOS pasteboard.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.ConsoleMvc.Platform;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// Copies text to the macOS pasteboard.
/// </summary>
public static class OsxClipboard {
    /// <summary>
    /// Data format used for text.
    /// </summary>
    private const string NsPasteboardTypeString = "public.utf8-plain-text";

    /// <summary>
    /// Copies text to the macOS clipboard.
    /// </summary>
    /// <param name="text">The text to be copied.</param>
    public static void SetText(string text) {
        var nsString = objc_getClass("NSString");
        IntPtr str = default;
        IntPtr dataType = default;

        try {
            str = objc_msgSend(objc_msgSend(nsString, sel_registerName("alloc")), sel_registerName("initWithUTF8String:"), text);
            dataType = objc_msgSend(objc_msgSend(nsString, sel_registerName("alloc")), sel_registerName("initWithUTF8String:"), NsPasteboardTypeString);

            var nsPasteboard = objc_getClass("NSPasteboard");
            var generalPasteboard = objc_msgSend(nsPasteboard, sel_registerName("generalPasteboard"));

            objc_msgSend(generalPasteboard, sel_registerName("clearContents"));
            objc_msgSend(generalPasteboard, sel_registerName("setString:forType:"), str, dataType);
        }
        finally {
            if (str != default) {
                objc_msgSend(str, sel_registerName("release"));
            }

            if (dataType != default) {
                objc_msgSend(dataType, sel_registerName("release"));
            }
        }
    }

    /// <summary>
    /// Returns the class definition of a specified class.
    /// </summary>
    /// <param name="className">The name of the class to look up.</param>
    /// <returns>
    /// The Class object for the named class, or nil if the class is not registered with
    /// the Objective-C runtime.
    /// </returns>
    [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit", CharSet = CharSet.Unicode)]
    // ReSharper disable once ArrangeTypeMemberModifiers
    private static extern IntPtr objc_getClass(string className);

    /// <summary>
    /// Sends a message with a simple return value to an instance of a class.
    /// </summary>
    /// <param name="receiver">
    /// A pointer that points to the instance of the class that is to receive the message.
    /// </param>
    /// <param name="selector">The selector of the method that handles the message.</param>
    /// <returns>The return value of the method.</returns>
    [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
    // ReSharper disable once ArrangeTypeMemberModifiers
    private static extern IntPtr objc_msgSend(IntPtr receiver, IntPtr selector);

    /// <summary>
    /// Sends a message with a simple return value to an instance of a class.
    /// </summary>
    /// <param name="receiver">
    /// A pointer that points to the instance of the class that is to receive the message.
    /// </param>
    /// <param name="selector">The selector of the method that handles the message.</param>
    /// <param name="arg1">The first arguments to the method.</param>
    /// <returns>The return value of the method.</returns>
    [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit", CharSet = CharSet.Unicode)]
    // ReSharper disable once ArrangeTypeMemberModifiers
    private static extern IntPtr objc_msgSend(IntPtr receiver, IntPtr selector, string arg1);

    /// <summary>
    /// Sends a message with a simple return value to an instance of a class.
    /// </summary>
    /// <param name="receiver">
    /// A pointer that points to the instance of the class that is to receive the message.
    /// </param>
    /// <param name="selector">The selector of the method that handles the message.</param>
    /// <param name="arg1">The first arguments to the method.</param>
    /// <param name="arg2">The second arguments to the method.</param>
    /// <returns>The return value of the method.</returns>
    [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
    // ReSharper disable once ArrangeTypeMemberModifiers
    private static extern IntPtr objc_msgSend(IntPtr receiver, IntPtr selector, IntPtr arg1, IntPtr arg2);

    /// <summary>
    /// Registers a method with the Objective-C runtime system, maps the method name to a selector,
    /// and returns the selector value.
    /// </summary>
    /// <param name="selectorName">
    /// A pointer to a C string. Pass the name of the method you wish to register.
    /// </param>
    /// <returns>A pointer of type SEL specifying the selector for the named method.</returns>
    [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit", CharSet = CharSet.Unicode)]
    // ReSharper disable once ArrangeTypeMemberModifiers
    private static extern IntPtr sel_registerName(string selectorName);
}