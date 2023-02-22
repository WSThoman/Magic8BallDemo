//---------------------------------------------------------------------------
//
// Copyright © 2015-2020 Paragon Bit Foundry.  All Rights Reserved.
//
//---------------------------------------------------------------------------
using System;

namespace PBFCommon
{
    // IMPORTANT NOTE:
    //
    // The project -MUST- contain an 'app.manifest' Windows Manifest file for this to work
    // for Windows versions 8 and higher.
    //
    public sealed class PBFOSVersion
    {
        // Properties
        //
        public static OperatingSystem OS { get; private set; } = null;

        public static Version OSVersion { get; private set; } = null;

        public static double WindowsOSNumber { get; private set; } = 0.0;

        public static string WindowsOSNumberStr { get; private set; } = string.Empty;

        public static bool IsWindows7     { get { return WindowsOSNumber == 7.0; } }

        public static bool IsWindows8     { get { return WindowsOSNumber == 8.0; } }

        public static bool IsWindows8Dot1 { get { return WindowsOSNumber == 8.1; } }

        public static bool IsWindows10    { get { return WindowsOSNumber == 10.0; } }

        public static bool IsAtLeastWindows7     { get { return WindowsOSNumber >= 7.0; } }
        
        public static bool IsAtLeastWindows8     { get { return WindowsOSNumber >= 8.0; } }
        
        public static bool IsAtLeastWindows8Dot1 { get { return WindowsOSNumber >= 8.1; } }
        
        public static bool IsAtLeastWindows10    { get { return WindowsOSNumber >= 10.0; } }

        // Constructors
        //
        //-------------------------------------------------------------------
        static PBFOSVersion()
        {
            OS = Environment.OSVersion;

            OSVersion = OS.Version;

            // Set the Windows OS Number as a double
            //
            // Windows   7:  6.1
            // Windows   8:  6.2
            // Windows 8.1:  6.3
            // Windows  10: 10.0
            //
            if ((OSVersion.Major == 6) && (OSVersion.Minor == 1))
            {
                WindowsOSNumber = 7.0;
            }
            else
            if ((OSVersion.Major == 6) && (OSVersion.Minor == 2))
            {
                WindowsOSNumber = 8.0;
            }
            else
            if ((OSVersion.Major == 6) && (OSVersion.Minor == 3))
            {
                WindowsOSNumber = 8.1;
            }
            else
            if ((OSVersion.Major == 10) && (OSVersion.Minor == 0))
            {
                WindowsOSNumber = 10.0;
            }

            // Set the user-facing string representation of the version number
            //
            WindowsOSNumberStr = WindowsOSNumber.ToString();

        } // Constructor - static

    } // class - PBFOSVersion

} // namespace
