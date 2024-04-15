// --------------------------------------------------------------------------------------------------------------------
// <copyright file="POINT.cs" company="Solidsoft Reply Ltd.">
//   (c) 2020 Solidsoft Reply Ltd.
// </copyright>
// <summary>
// The POINT structure defines the x- and y-coordinates of a point.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.EmvsUniqueIdentifierTester.Platform.Windows;

/// <summary>
/// The POINT structure defines the x- and y-coordinates of a point.
/// </summary>
// ReSharper disable once InconsistentNaming
public struct Point {
#pragma warning disable S1104 // Fields should not have public accessibility
    public int X;
    public int Y;
#pragma warning restore S1104 // Fields should not have public accessibility
}