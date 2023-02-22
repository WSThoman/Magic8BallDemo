//---------------------------------------------------------------------------
//
// Copyright © 2017-2020 Paragon Bit Foundry.  All Rights Reserved.
//
//---------------------------------------------------------------------------
using System.Runtime.InteropServices;

namespace PBFCommon
{
    public sealed class PBFNotifyIconSize
    {
        private sealed class NativeMethods
        {
            // extern's
            //
            [DllImport("user32.dll")]
            public static extern int GetSystemMetrics( int nIndex );
        }


        // Icons are square, so we determine the width using the 'X'
        // identifier 'SM_CXSMICON' and use it for both the width and height
        // of the icon.  The corresponding 'Y' value is SM_CYSMICON = 50.
        //
        private const int SM_CXSMICON = 49;

        // Properties
        //
        public static int IconSize { get; private set; } = 0;

        // Constructors
        //
        #region Constructors

        //-------------------------------------------------------------------
        static PBFNotifyIconSize()
        {
            GetIconSize();

        } // Constructor - static

        #endregion Constructors

        // Class methods
        //
        //-------------------------------------------------------------------
        private static void GetIconSize()
        {
            IconSize = NativeMethods.GetSystemMetrics( SM_CXSMICON );
        }

    } // class - PBFNotifyIconSize

} // namespace
