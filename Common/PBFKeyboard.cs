//---------------------------------------------------------------------------
//
// Copyright © 2018-2024 Paragon Bit Foundry.  All Rights Reserved.
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
            #region extern's

            [DllImport("user32.dll")]
            public static extern void keybd_event( byte bVk, byte bScan, uint dwFlags, IntPtr dwExtraInfo );

            [DllImport("user32.dll")]
            public static extern ushort GetAsyncKeyState( int vKey );

            #endregion
        }

        #region Constants

        private const uint KEYEVENTF_EXTENDEDKEY = 0x01;
        private const uint KEYEVENTF_KEYUP       = 0x02;

        #endregion

        #region Constructors

        //-------------------------------------------------------------------
        static PBFKeyboard()
        {
        } // Constructor - static

        #endregion

        #region Caps Lock

        //-------------------------------------------------------------------
        public static bool CapsLock
        {
            get { return GetCapsLock(); }
            set { SetCapsLock( value ); }
        }

        public static bool IsCapsLockOn { get { return CapsLock; } }

        public static bool IsCapsLockOff { get { return ! CapsLock; } }

        //-------------------------------------------------------------------
        private static bool GetCapsLock()
        {
            return Control.IsKeyLocked( Keys.CapsLock );
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
                if (IsCapsLockOff)
                { 
                    PressExtendedKey( Keys.CapsLock );
                }
            }
            else
            {
                if (IsCapsLockOn)
                { 
                    PressExtendedKey( Keys.CapsLock );
                }
            }
        }

        //-------------------------------------------------------------------
        public static void SetCapsLockOn()
        {
            SetCapsLock( true );
        }

        //-------------------------------------------------------------------
        public static void SetCapsLockOff()
        {
            SetCapsLock( false );
        }

        //-------------------------------------------------------------------
        public static void ToggleCapsLock()
        {
            PressExtendedKey( Keys.CapsLock );
        }

        #endregion

        #region Num Lock

        //-------------------------------------------------------------------
        public static bool NumLock
        {
            get { return GetNumLock(); }
            set { SetNumLock( value ); }
        }

        public static bool IsNumLockOn { get { return NumLock; } }

        public static bool IsNumLockOff { get { return ! NumLock; } }

        //-------------------------------------------------------------------
        private static bool GetNumLock()
        {
            return Control.IsKeyLocked( Keys.NumLock );
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
                if (IsNumLockOff)
                { 
                    PressExtendedKey( Keys.NumLock );
                }
            }
            else
            {
                if (IsNumLockOn)
                { 
                    PressExtendedKey( Keys.NumLock );
                }
            }
        }

        //-------------------------------------------------------------------
        public static void SetNumLockOn()
        {
            SetNumLock( true );
        }

        //-------------------------------------------------------------------
        public static void SetNumLockOff()
        {
            SetNumLock( false );
        }

        //-------------------------------------------------------------------
        public static void ToggleNumLock()
        {
            PressExtendedKey( Keys.NumLock );
        }

        #endregion

        #region Scroll Lock

        //-------------------------------------------------------------------
        public static bool ScrollLock
        {
            get { return GetScrollLock(); }
            set { SetScrollLock( value ); }
        }

        public static bool IsScrollLockOn { get { return ScrollLock; } }

        public static bool IsScrollLockOff { get { return ! ScrollLock; } }

        //-------------------------------------------------------------------
        private static bool GetScrollLock()
        {
            return Control.IsKeyLocked( Keys.Scroll );
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
                if (IsScrollLockOff)
                { 
                    PressExtendedKey( Keys.Scroll );
                }
            }
            else
            {
                if (IsScrollLockOn)
                { 
                    PressExtendedKey( Keys.Scroll );
                }
            }
        }

        //-------------------------------------------------------------------
        public static void SetScrollLockOn()
        {
            SetScrollLock( true );
        }

        //-------------------------------------------------------------------
        public static void SetScrollLockOff()
        {
            SetScrollLock( false );
        }

        //-------------------------------------------------------------------
        public static void ToggleScrollLock()
        {
            PressExtendedKey( Keys.Scroll );
        }

        #endregion

        // Keyboard Modifier Keys
        //
        #region Shift

        //-------------------------------------------------------------------
        public static bool IsShiftKeyDown
        {
            get { return Control.ModifierKeys.HasFlag( Keys.Shift ); }
        }

        public static bool IsLeftShiftKeyDown
        {
            get { return IsKeyDown( Keys.LShiftKey ); }
        }

        public static bool IsRightShiftKeyDown
        {
            get { return IsKeyDown( Keys.RShiftKey ); }
        }

        #endregion

        #region Ctrl

        //-------------------------------------------------------------------
        public static bool IsCtrlKeyDown
        {
            get { return Control.ModifierKeys.HasFlag( Keys.Control ); }
        }

        public static bool IsLeftCtrlKeyDown
        {
            get { return IsKeyDown( Keys.LControlKey ); }
        }

        public static bool IsRightCtrlKeyDown
        {
            get { return IsKeyDown( Keys.RControlKey ); }
        }

        #endregion

        #region Alt

        //-------------------------------------------------------------------
        public static bool IsAltKeyDown
        {
            get { return Control.ModifierKeys.HasFlag( Keys.Alt ); }
        }

        public static bool IsLeftAltKeyDown
        {
            get { return IsKeyDown( Keys.LMenu ); }
        }

        public static bool IsRightAltKeyDown
        {
            get { return IsKeyDown( Keys.RMenu ); }
        }

        #endregion

        #region Windows key

        //-------------------------------------------------------------------
        public static bool IsWindowsKeyDown
        {
            get { return IsLeftWindowsKeyDown || IsRightWindowsKeyDown; }
        }

        public static bool IsLeftWindowsKeyDown
        {
            get { return IsKeyDown( Keys.LWin ); }
        }

        public static bool IsRightWindowsKeyDown
        {
            get { return IsKeyDown( Keys.RWin ); }
        }

        #endregion

        #region Class methods

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

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns 'true' if <paramref name="vKey"/> key is currently down.
        /// </summary>
        /// <param name="vKey"></param>
        ///-------------------------------------------------------------------
        private static bool IsKeyDown(Keys vKey)
        {
            return (NativeMethods.GetAsyncKeyState( (int)vKey) & 0x8000) != 0;
        }

        #endregion

    } // class - PBFKeyboard

} // namespace
