//---------------------------------------------------------------------------
//
// Copyright © 2018-2020 Paragon Bit Foundry.  All Rights Reserved.
//
//---------------------------------------------------------------------------
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PBFCommon
{
    public sealed class PBFKeyboard
    {
        private sealed class NativeMethods
        {
            // extern's
            //
            [DllImport("user32.dll")]
#pragma warning disable IDE1006 // Naming Styles
            public static extern void keybd_event( byte bVk, byte bScan, uint dwFlags, IntPtr dwExtraInfo );
#pragma warning restore IDE1006 // Naming Styles

            [DllImport("user32.dll")]
            public static extern ushort GetAsyncKeyState( int vKey );
        }
        
        // Constants
        //
        private const int KEYEVENTF_EXTENDEDKEY = 0x01;
        private const int KEYEVENTF_KEYUP       = 0x02;

        // Constructors
        //
        //-------------------------------------------------------------------
        static PBFKeyboard()
        {
        } // Constructor - static

        // Properties
        //
        #region Properties

        //-------------------------------------------------------------------
        public bool CapsLock
        {
            get { return IsCapsLockOn(); }
            set { SetCapsLock( value ); }
        }

        //-------------------------------------------------------------------
        public bool NumLock
        {
            get { return IsNumLockOn(); }
            set { SetNumLock( value ); }
        }

        //-------------------------------------------------------------------
        public bool ScrollLock
        {
            get { return IsScrollLockOn(); }
            set { SetScrollLock( value ); }
        }

        #endregion Properties

        // Class methods
        //
        ///-------------------------------------------------------------------
        /// <summary>
        /// PressExtendedKey
        /// </summary>
        /// <param name="theKeyCode"></param>
        /// <remarks>
        /// Note that two events are fired, including a 'key up' event.
        /// </remarks>
        ///-------------------------------------------------------------------
        private static void PressExtendedKey( Keys theKeyCode )
        {
            NativeMethods.keybd_event( (byte)theKeyCode, 0x45, KEYEVENTF_EXTENDEDKEY,                   (IntPtr)null );
            NativeMethods.keybd_event( (byte)theKeyCode, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (IntPtr)null );
        }

        // Caps Lock
        //
        #region Caps Lock

        ///-------------------------------------------------------------------
        /// <summary>
        /// IsCapsLockOn
        /// </summary>
        ///-------------------------------------------------------------------
        public static bool IsCapsLockOn()
        {
            return Control.IsKeyLocked( Keys.CapsLock );
        }
        
        ///-------------------------------------------------------------------
        /// <summary>
        /// IsCapsLockOff
        /// </summary>
        ///-------------------------------------------------------------------
        public static bool IsCapsLockOff()
        {
            return ! IsCapsLockOn();
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// SetCapsLock
        /// </summary>
        /// <param name="theNewCapsLockState"></param>
        ///-------------------------------------------------------------------
        public static void SetCapsLock( bool theNewCapsLockState = false )
        {
            if (theNewCapsLockState)
            {
                if ( ! IsCapsLockOn())
                { 
                    PressExtendedKey( Keys.CapsLock );
                }
            }
            else
            {
                if (IsCapsLockOn())
                { 
                    PressExtendedKey( Keys.CapsLock );
                }
            }
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// SetCapsLockOn
        /// </summary>
        ///-------------------------------------------------------------------
        public static void SetCapsLockOn()
        {
            SetCapsLock( true );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// SetCapsLockOff
        /// </summary>
        ///-------------------------------------------------------------------
        public static void SetCapsLockOff()
        {
            SetCapsLock( false );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// ToggleCapsLock
        /// </summary>
        ///-------------------------------------------------------------------
        public static void ToggleCapsLock()
        {
            PressExtendedKey( Keys.CapsLock );
        }

        #endregion Caps Lock

        // Num Lock
        //
        #region Num Lock

        ///-------------------------------------------------------------------
        /// <summary>
        /// IsNumLockOn
        /// </summary>
        ///-------------------------------------------------------------------
        public static bool IsNumLockOn()
        {
            return Control.IsKeyLocked( Keys.NumLock );
        }
        
        ///-------------------------------------------------------------------
        /// <summary>
        /// IsNumLockOff
        /// </summary>
        ///-------------------------------------------------------------------
        public static bool IsNumLockOff()
        {
            return ! IsNumLockOn();
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// SetNumLock
        /// </summary>
        /// <param name="theNewNumLockState"></param>
        ///-------------------------------------------------------------------
        public static void SetNumLock( bool theNewNumLockState = false )
        {
            if (theNewNumLockState)
            {
                if ( ! IsNumLockOn())
                { 
                    PressExtendedKey( Keys.NumLock );
                }
            }
            else
            {
                if (IsNumLockOn())
                { 
                    PressExtendedKey( Keys.NumLock );
                }
            }
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// SetNumLockOn
        /// </summary>
        ///-------------------------------------------------------------------
        public static void SetNumLockOn()
        {
            SetNumLock( true );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// SetNumLockOff
        /// </summary>
        ///-------------------------------------------------------------------
        public static void SetNumLockOff()
        {
            SetNumLock( false );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// ToggleNumLock
        /// </summary>
        ///-------------------------------------------------------------------
        public static void ToggleNumLock()
        {
            PressExtendedKey( Keys.NumLock );
        }

        #endregion Num Lock

        // Scroll Lock
        //
        #region Scroll Lock

        ///-------------------------------------------------------------------
        /// <summary>
        /// IsScrollLockOn
        /// </summary>
        ///-------------------------------------------------------------------
        public static bool IsScrollLockOn()
        {
            return Control.IsKeyLocked( Keys.Scroll );
        }
        
        ///-------------------------------------------------------------------
        /// <summary>
        /// IsScrollLockOff
        /// </summary>
        ///-------------------------------------------------------------------
        public static bool IsScrollLockOff()
        {
            return ! IsScrollLockOn();
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// SetScrollLock
        /// </summary>
        /// <param name="theNewScrollLockState"></param>
        ///-------------------------------------------------------------------
        public static void SetScrollLock( bool theNewScrollLockState = false )
        {
            if (theNewScrollLockState)
            {
                if ( ! IsScrollLockOn())
                { 
                    PressExtendedKey( Keys.Scroll );
                }
            }
            else
            {
                if (IsScrollLockOn())
                { 
                    PressExtendedKey( Keys.Scroll );
                }
            }
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// SetScrollLockOn
        /// </summary>
        ///-------------------------------------------------------------------
        public static void SetScrollLockOn()
        {
            SetScrollLock( true );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// SetScrollLockOff
        /// </summary>
        ///-------------------------------------------------------------------
        public static void SetScrollLockOff()
        {
            SetScrollLock( false );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// ToggleScrollLock
        /// </summary>
        ///-------------------------------------------------------------------
        public static void ToggleScrollLock()
        {
            PressExtendedKey( Keys.Scroll );
        }

        #endregion Scroll Lock

        // Keyboard Modifier Keys
        //
        //-------------------------------------------------------------------
        public static bool IsKeyDown(Keys vKey)
        {
            return (NativeMethods.GetAsyncKeyState( (int)vKey) & 0x8000 ) != 0;
        }

        // Shift
        //
        #region Shift

        //-------------------------------------------------------------------
        public static bool IsShiftKeyDown()
        {
            return Control.ModifierKeys.HasFlag( Keys.Shift );
        }

        //-------------------------------------------------------------------
        public static bool IsLeftShiftKeyDown()
        {
            return IsKeyDown( Keys.LShiftKey );
        }

        //-------------------------------------------------------------------
        public static bool IsRightShiftKeyDown()
        {
            return IsKeyDown( Keys.RShiftKey );
        }

        #endregion Shift

        // Ctrl
        //
        #region Ctrl

        //-------------------------------------------------------------------
        public static bool IsCtrlKeyDown()
        {
            return Control.ModifierKeys.HasFlag( Keys.Control );
        }

        //-------------------------------------------------------------------
        public static bool IsLeftCtrlKeyDown()
        {
            return IsKeyDown( Keys.LControlKey );
        }

        //-------------------------------------------------------------------
        public static bool IsRightCtrlKeyDown()
        {
            return IsKeyDown( Keys.RControlKey );
        }

        #endregion Ctrl

        // Alt
        //
        #region Alt

        //-------------------------------------------------------------------
        public static bool IsAltKeyDown()
        {
            return Control.ModifierKeys.HasFlag( Keys.Alt );
        }

        //-------------------------------------------------------------------
        public static bool IsLeftAltKeyDown()
        {
            return IsKeyDown( Keys.LMenu );
        }

        //-------------------------------------------------------------------
        public static bool IsRightAltKeyDown()
        {
            return IsKeyDown( Keys.RMenu );
        }

        #endregion Alt

        // Windows key
        //
        #region Windows key

        //-------------------------------------------------------------------
        public static bool IsWindowsKeyDown()
        {
            return IsLeftWindowsKeyDown() || IsRightWindowsKeyDown();
        }

        //-------------------------------------------------------------------
        public static bool IsLeftWindowsKeyDown()
        {
            return IsKeyDown( Keys.LWin );
        }

        //-------------------------------------------------------------------
        public static bool IsRightWindowsKeyDown()
        {
            return IsKeyDown( Keys.RWin );
        }

        #endregion Windows key

    } // class - PBFKeyboard

} // namespace
