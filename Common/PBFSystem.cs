//---------------------------------------------------------------------------
//
// Copyright © 2016-2024 Paragon Bit Foundry.  All Rights Reserved.
//
//---------------------------------------------------------------------------
using Microsoft.Win32;

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace PBFCommon
{
    public sealed class PBFSystem
    {
        private sealed class NativeMethods
        {
            [DllImport("kernel32")]
            public extern static ulong GetTickCount64();

            [DllImport("user32.dll", SetLastError = true)]
            public extern static bool PostMessage(IntPtr hWnd, [MarshalAs(UnmanagedType.U4)] uint Msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto,SetLastError = true)]
            public extern static EXECUTION_STATE SetThreadExecutionState( EXECUTION_STATE esFlags );
        }


        #region Constants

        public const int DAYS_PER_WEEK      = 7;
        public const int DAYS_PER_FORTNIGHT = DAYS_PER_WEEK * 2;

        private const string NOTEPAD_EXE_FILENAME  = "notepad.exe";
        private const string EXPLORER_EXE_FILENAME = "explorer.exe";
        private const string OSK_EXE_FILENAME      = "osk.exe";

        private const string EXPLORER_SHELL_WND = "Shell_TrayWnd";

        private const int WM_USER = 0x0400;
        private const int EXPLORER_HIDDEN_OPTION_OFFSET = 436;

        [Flags]
        public enum EXECUTION_STATE : uint
        {
             ES_AWAYMODE_REQUIRED = 0x00000040,
             ES_CONTINUOUS        = 0x80000000,
             ES_DISPLAY_REQUIRED  = 0x00000002,
             ES_SYSTEM_REQUIRED   = 0x00000001

             // Legacy flag
             // ES_USER_PRESENT   = 0x00000004
        }

        #endregion

        #region Data members

        private static DateTime mSystemStartTime = DateTime.MinValue;

        #endregion

        #region Properties

        public static TimeSpan UpTime { get { return GetUpTime(); } }

        public static DateTime SystemStartTime { get { return GetSystemStartTime(); } }

        public static int ProcessCount { get { return GetProcessCount(); } }

        #endregion

        #region Constructors

        //-------------------------------------------------------------------
        private PBFSystem()
        {
        } // Constructor - default

        #endregion

        ///-------------------------------------------------------------------
        /// <summary>
        /// LaunchApplication
        /// </summary>
        /// <param name="theEXEName"></param>
        /// <param name="theCmdLineArgs"></param>
        /// <param name="startMinimized"></param>
        /// <returns>true on success, false on error</returns>
        ///-------------------------------------------------------------------
        public static bool LaunchApplication( string theEXEName, string theCmdLineArgs = "",
                                              bool startMinimized = false )
        {
            // Start the application
            //
            bool retBool = false;

#if NETCOREAPP
            try
            {
                Process.Start( new ProcessStartInfo("cmd", $"/c start {theEXEName}") { CreateNoWindow = true });

                retBool = true;
            }
            catch
            {
            }
#else
            ProcessStartInfo psiObj = new ProcessStartInfo
            {
                FileName = theEXEName
            };

            if (theCmdLineArgs.Length > 0)
            {
                psiObj.Arguments = theCmdLineArgs;
            }

            psiObj.WindowStyle =
                startMinimized ? ProcessWindowStyle.Minimized : ProcessWindowStyle.Normal;

            psiObj.CreateNoWindow = false;

            try
            {
                Process.Start( psiObj );

                retBool = true;
            }
            catch
            {
            }
#endif
            // Return result to caller
            //
            return retBool;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// LaunchNotepad
        /// </summary>
        /// <param name="theFileName"></param>
        /// <returns>true on success, false on error</returns>
        ///-------------------------------------------------------------------
        public static bool LaunchNotepad( string theFileName )
        {
            return LaunchApplication( NOTEPAD_EXE_FILENAME, theFileName, false );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// LaunchTaskbarSettings
        /// </summary>
        /// <returns>true on success, false on error</returns>
        ///-------------------------------------------------------------------
        public static bool LaunchTaskbarSettings()
        {
            return LaunchApplication( "ms-settings:taskbar" );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// LaunchDateAndTime
        /// </summary>
        /// <returns>true on success, false on error</returns>
        ///-------------------------------------------------------------------
        public static bool LaunchDateAndTime()
        {
            return LaunchApplication( "ms-settings:dateandtime" );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// LaunchExplorer
        /// </summary>
        /// <param name="thePathName"></param>
        /// <returns>true on success, false on error</returns>
        ///-------------------------------------------------------------------
        public static bool LaunchExplorer( string thePathName = "" )
        {
            return LaunchApplication( EXPLORER_EXE_FILENAME, thePathName, false );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// LaunchExplorerShell
        /// </summary>
        /// <param name="thePathName"></param>
        /// <returns>true on success, false on error</returns>
        ///-------------------------------------------------------------------
        public static bool LaunchExplorerShell( string thePathName = "" )
        {
            string explorerFullPathName =
                Environment.GetEnvironmentVariable("WINDIR") + Path.DirectorySeparatorChar + EXPLORER_EXE_FILENAME;

            Process esProcess = new Process();
            esProcess.StartInfo.FileName = explorerFullPathName;

            if (thePathName.Length > 0)
            {
                esProcess.StartInfo.Arguments = thePathName;
            }

            esProcess.StartInfo.UseShellExecute = true;
            
            bool esStartRes = false;
            try
            {
                esStartRes = esProcess.Start();
            }
            catch
            {
            }

            return esStartRes;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// ExplorerCloseInstances
        /// </summary>
        /// <returns>true on success, false on error</returns>
        ///-------------------------------------------------------------------
        public static bool ExplorerCloseInstances()
        {
            try
            {
                var fwPtr = NativeMethods.FindWindow( EXPLORER_SHELL_WND, null );

                NativeMethods.PostMessage( fwPtr, WM_USER + EXPLORER_HIDDEN_OPTION_OFFSET, (IntPtr)0, (IntPtr)0 );

                do
                {
                    fwPtr = NativeMethods.FindWindow( EXPLORER_SHELL_WND, null );

                    if (fwPtr.ToInt32() == 0)
                    {
                        break;
                    }

                    Thread.Sleep(1000);

                } while (true);

                return true;
            }
            catch
            {
            }

            return false;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// ExplorerKillInstances
        /// </summary>
        /// <returns>true on success, false on error</returns>
        ///-------------------------------------------------------------------
        public static bool ExplorerKillInstances( bool killJustTheFirstOne = false )
        {
            Process[] allProcesses = Process.GetProcesses();

            foreach (Process p in allProcesses)
            {
                try
                {
                    if (p.MainModule.FileName.ToLower().EndsWith( EXPLORER_EXE_FILENAME ))
                    {
                        p.Kill();

                        if (killJustTheFirstOne)
                        {
                            break;
                        }
                    }
                }
                catch
                {
                }
            }

            return false;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// RestartExplorer
        /// </summary>
        /// <returns>true on success, false on error</returns>
        ///-------------------------------------------------------------------
        public static bool RestartExplorer()
        {
            if ( ! ExplorerCloseInstances())
            {
                ExplorerKillInstances();
            }
                        
//Process.Start( EXPLORER_EXE_FILENAME );
//            ShellExecute(NULL, NULL, EXPLORER_EXE_FILENAME, NULL, NULL, SW_SHOW );  // not in C#
//            LaunchExplorer();

            LaunchExplorerShell();

            return false;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Keeps the session active by resetting the 'idle timeout', thus
        /// preventing the screensaver from activating.
        /// </summary>
        /// <remarks>
        /// This MUST be called periodically to reset the idle timeout.
        /// https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-setthreadexecutionstate
        /// </remarks>
        ///-------------------------------------------------------------------
        private static void CallKeepAwake( EXECUTION_STATE theFlags )
        {
            NativeMethods.SetThreadExecutionState( theFlags );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Keeps the session active for 'system' or back-end applications.
        /// </summary>
        ///-------------------------------------------------------------------
        public static void KeepAwakeForSystem()
        {
            CallKeepAwake( EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_SYSTEM_REQUIRED );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Keeps the session active for 'display' applications such video playback.
        /// </summary>
        ///-------------------------------------------------------------------
        public static void KeepAwakeForDisplay()
        {
            CallKeepAwake( EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_DISPLAY_REQUIRED );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Keeps the session active for both 'system' and 'display'
        /// </summary>
        ///-------------------------------------------------------------------
        public static void KeepAwakeForSysAndDisplay()
        {
            CallKeepAwake( EXECUTION_STATE.ES_CONTINUOUS |
                           EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_DISPLAY_REQUIRED );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Keeps the session active using 'away mode'.  Only used by media apps.
        /// </summary>
        ///-------------------------------------------------------------------
        public static void KeepAwakeForAwayMode()
        {
            CallKeepAwake( EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_AWAYMODE_REQUIRED );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns the session back to normal 'idle to sleep' mode by clearing
        /// all flags besides ES_CONTINUOUS
        /// </summary>
        ///-------------------------------------------------------------------
        public static void ClearKeepAwake()
        {
            CallKeepAwake( EXECUTION_STATE.ES_CONTINUOUS );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// LaunchControlPanel
        /// </summary>
        /// <param name="withParams"></param>
        /// <returns>'true' on success, 'false' on error</returns>
        /// <remarks>
        /// Use 'true' for Windows 7, or 'false' for Windows 10 and higher
        /// </remarks>
        ///-------------------------------------------------------------------
        public static bool LaunchControlPanel( bool withParams = false )
        {
            if (withParams)
            {
                return LaunchApplication( "control.exe /name Microsoft.NotificationAreaIcons" );
            }

            return LaunchApplication( "control.exe" );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// LaunchControlPanelKeyboard
        /// </summary>
        /// <returns>'true' on success, 'false' on error</returns>
        ///-------------------------------------------------------------------
        public static bool LaunchControlPanelKeyboard()
        {
            return LaunchApplication( "main.cpl", "keyboard" );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// LaunchOnScreenKeyboard
        /// </summary>
        ///-------------------------------------------------------------------
        public static void LaunchOnScreenKeyboard()
        {
            LaunchApplication( OSK_EXE_FILENAME );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// LaunchWebBrowser
        /// </summary>
        /// <param name="theURL"></param>
        ///-------------------------------------------------------------------
        public static bool LaunchWebBrowser( string theURL )
        {
            try
            {
#if NETCOREAPP
                Process.Start( new ProcessStartInfo("cmd", $"/c start {theURL}") { CreateNoWindow = true });
#else
                Process.Start( new ProcessStartInfo( theURL ) );
#endif
                return true;
            }
            catch
            {                
            }

            return false;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// LaunchGoogleSearch
        /// </summary>
        /// <param name="theSearchText"></param>
        ///-------------------------------------------------------------------
        public static void LaunchGoogleSearch( string theSearchText )
        {
            LaunchWebBrowser(
                "http://www.google.com/search?q=" + theSearchText );
        }

        //-------------------------------------------------------------------
        public static void StartTaskManager()
        {
            LaunchApplication( "taskmgr.exe" );
        }

        //-------------------------------------------------------------------
        public static void StartEventViewer()
        {
            LaunchApplication( "eventvwr.msc" );
        }

        //-------------------------------------------------------------------
        public static void StartResourceMonitor()
        {
            LaunchApplication( "resmon.exe" );
        }

        //-------------------------------------------------------------------
        public static void StartPerformanceMonitor()
        {
            LaunchApplication( "perfmon.exe" );
        }

        //-------------------------------------------------------------------
        public static void StartDeviceManager()
        {
            LaunchApplication( "devmgmt.msc" );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// OpenStartupFolder
        /// </summary>
        ///-------------------------------------------------------------------
        public static void OpenStartupFolder()
        {
            LaunchApplication( "shell:startup" );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Opens the current user's 'Desktop" folder, e.g 'C:\Users\johndoe\Desktop'
        /// </summary>
        ///-------------------------------------------------------------------
        public static void OpenDesktopFolderUser()
        {
            LaunchApplication(
                Environment.GetFolderPath( Environment.SpecialFolder.UserProfile ) + Path.DirectorySeparatorChar + "Desktop" );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Opens the 'Common Desktop' folder 'Desktop"
        /// </summary>
        ///-------------------------------------------------------------------
        public static void OpenDesktopFolderSystem()
        {
            LaunchApplication( "shell:desktop" );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Opens the 'User Profile' folder, e.g 'C:\Users\johndoe'
        /// </summary>
        ///-------------------------------------------------------------------
        public static void OpenUserProfileFolder()
        {
            LaunchApplication( Environment.GetFolderPath( Environment.SpecialFolder.UserProfile ) );
        }

//        // Activate an application window.
//        [DllImport("USER32.DLL")]
//        public static extern bool SetForegroundWindow(IntPtr hWnd);

//        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
//        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
//
//        private static uint WM_CLOSE = 0x10;

        ///-------------------------------------------------------------------
        /// <summary>
        /// CloseApplication
        /// </summary>
        /// <param name="theAppName"></param>
        /// <returns>true on success, false on error</returns>
        ///-------------------------------------------------------------------
        public static bool CloseApplication( string theAppName )
        {
            Process[] caProcessList;

            try
            {
                caProcessList = Process.GetProcessesByName(theAppName);
                               
                // WM_Click is 0x00F5
//        public const int WM_RBUTTONDOWN = 0x0204;
//        public const int WM_RBUTTONUP = 0x0205;
//
//                IntPtr csWinHandle = caProcessList[ 0 ].MainWindowHandle;
//                SetForegroundWindow(csWinHandle);
//                SendKeys.Send("X");

                if (caProcessList.Length > 0)
                {
                    caProcessList[ 0 ].Kill();

                    caProcessList[ 0 ].Close();

//                    caProcessList[ 0 ].CloseMainWindow();

//                    SendMessage(caProcessList[ 0 ].Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);

                    return true;
                }
            }
            finally
            {
            }
                       
            return false;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns the number of running system processes
        /// </summary>
        /// <returns>
        /// Returns >= 0 on success, -1 on error
        /// </returns>
        ///-------------------------------------------------------------------
        public static int GetProcessCount()
        {
            int processCount;

            try
            {
                Process[] gpcProcessList = Process.GetProcesses();

                processCount = gpcProcessList.Length;
            }
            finally
            {
            }

            return processCount;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns the system up time as a 'TimeSpan' object
        /// </summary>
        ///-------------------------------------------------------------------
        public static TimeSpan GetUpTime()
        {
            return TimeSpan.FromMilliseconds( NativeMethods.GetTickCount64() );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns the time in '[Ww] Dd Hh Mm [Ss]' string format
        /// </summary>
        ///-------------------------------------------------------------------
        public static string GetTimeStr( TimeSpan theTimeSpan, bool includeSeconds = false,
                                         bool includeWeeks = true, bool includeFortnights = true )
        {
            string retStr = string.Empty;

            int days = theTimeSpan.Days;

            if (includeFortnights)
            {
                int fortnights = days / DAYS_PER_FORTNIGHT;

                days -= (fortnights * DAYS_PER_FORTNIGHT);

                if (fortnights > 0)
                {
                    retStr += fortnights + "f";
                }
            }

            if (includeWeeks)
            {
                int weeks = days / DAYS_PER_WEEK;

                days %= DAYS_PER_WEEK;

                if (weeks > 0)
                {
                    retStr += (retStr.Length > 0) ? " " : string.Empty;
    
                    retStr += weeks + "w";
                }
            }

            if (days > 0)
            {
                retStr += (retStr.Length > 0) ? " " : string.Empty;

                retStr += days + "d";
            }

            if (theTimeSpan.Hours > 0)
            {
                retStr += (retStr.Length > 0) ? " " : string.Empty;

                retStr += theTimeSpan.Hours + "h";
            }

            if (theTimeSpan.Minutes > 0)
            {
                retStr += (retStr.Length > 0) ? " " : string.Empty;

                retStr += theTimeSpan.Minutes + "m";
            }

            if (includeSeconds)
            {
                if (theTimeSpan.Seconds > 0)
                {
                    retStr += (retStr.Length > 0) ? " " : string.Empty;

                    retStr += theTimeSpan.Seconds + "s";
                }
            }

            if (retStr.Length == 0)
            {
                retStr = "Just now";
            }

            return retStr;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns the system up time in 'H hour(s) M minute(s) [S second(s)]'
        /// string format if less than one day, or in '[Ff] [Ww] Dd Hh Mm [Ss]'
        /// string format for times greater than one day.
        /// </summary>
        ///-------------------------------------------------------------------
        public static string GetTimeStrExt( TimeSpan theTimeSpan, bool includeSeconds = false,
                                            bool includeWeeks = true, bool includeFortnights = true )
        {
            if (theTimeSpan.Days > 0)
            {
                return GetTimeStr( theTimeSpan, includeSeconds, includeWeeks, includeFortnights );
            }

            string retStr = string.Empty;

            if (theTimeSpan.Hours > 0)
            {
                retStr += theTimeSpan.Hours + " hour" + PBFString.Plural(theTimeSpan.Hours);

                if (theTimeSpan.Minutes > 0)
                {
                    retStr += (retStr.Length > 0) ? ", " : string.Empty;

                    retStr += theTimeSpan.Minutes + " min";
                }
            }
            else
            {
                if (theTimeSpan.Minutes > 0)
                {
                    retStr += (retStr.Length > 0) ? ", " : string.Empty;

                    retStr += theTimeSpan.Minutes + " minute" + PBFString.Plural(theTimeSpan.Minutes);
                }
            }

            if (includeSeconds)
            {
                if (theTimeSpan.Seconds > 0)
                {
                    retStr += (retStr.Length > 0) ? ", " : string.Empty;

                    retStr += theTimeSpan.Seconds + " sec";
                }
            }

            if (retStr.Length == 0)
            {
                retStr = "Just now";
            }

            return retStr;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns the system start time as a 'DateTime' object
        /// </summary>
        ///-------------------------------------------------------------------
        public static DateTime GetSystemStartTime()
        {
            if (mSystemStartTime == DateTime.MinValue)
            {
                try
                {
                    mSystemStartTime = DateTime.Now - GetUpTime();
                }
                catch
                {
                    mSystemStartTime = DateTime.MinValue;
                }
            }
            
            return mSystemStartTime;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Sleeps this process for the given number of milliseconds
        /// </summary>
        /// <param name="theSleepTimeInMSec"></param>
        ///-------------------------------------------------------------------
        public static void Sleep( int theSleepTimeInMSec )
        {
            Thread.Sleep( theSleepTimeInMSec );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Determines if 'theAppDisplayName' is installed on this Windows system
        /// </summary>
        /// <param name="theAppDisplayName">
        /// The application name as it appears within the Windows Start menu
        /// </param>
        /// <returns>
        /// true if the app is installed, false if not
        /// </returns>
        ///-------------------------------------------------------------------
        public static bool IsApplicationInstalled( string theAppDisplayName )
        {
            // Check 64-bit node first
            //
            if (IsAppInRegKey( theAppDisplayName,
                               @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall" ))
            {
                return true;
            }

            // Check 32-bit node if not found in 64-bit node
            //
            return IsAppInRegKey( theAppDisplayName,
                                  @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall" );
        }

        private static bool IsAppInRegKey( string theAppDisplayName, string theRegistryKey )
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey( theRegistryKey );

            if (key != null)
            {
                try
                {
                    foreach (RegistryKey subKey in
                                 key.GetSubKeyNames().Select( keyName => key.OpenSubKey( keyName ) ))
                    {
                        if (subKey.GetValue("DisplayName") is string curDisplayName &&
                            curDisplayName.Contains( theAppDisplayName ))
                        {
                            return true;
                        }
                    }
                }
                finally
                {
                    key.Close();
                }
            }

            return false;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns a string detailing the line of source code that called it.
        /// </summary>
        /// <param name="callingObjThis"></param>
        /// <param name="includeCallingFilePath"></param>
        /// <param name="callingMethod"></param>
        /// <param name="callingFilePath"></param>
        /// <param name="callingFileLineNumber"></param>
        /// <remarks>
        /// Pass the keyword 'this' for the first parameter 'callingObjThis'.
        /// </remarks>
        ///-------------------------------------------------------------------
        public static string SourceCodePosStr( object callingObjThis,
                                               bool includeCallingFilePath = false,
                                               [CallerMemberName] string callingMethod = "",
                                               [CallerFilePath] string callingFilePath = "",
                                               [CallerLineNumber] int callingFileLineNumber = 0 )
        {
            return ((callingObjThis != null) ? callingObjThis.GetType().Name + "." : string.Empty) +
                   callingMethod + "():" + callingFileLineNumber.ToString() +
                   (includeCallingFilePath ? " " + callingFilePath : string.Empty);
        }

    } // class - PBFSystem

} // namespace
