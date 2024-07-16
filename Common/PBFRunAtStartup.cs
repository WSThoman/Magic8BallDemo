//---------------------------------------------------------------------------
//
// Copyright © 2016-2024 Paragon Bit Foundry.  All Rights Reserved.
//
//---------------------------------------------------------------------------
using Microsoft.Win32;

using System.Reflection;

namespace PBFCommon
{
    public sealed class PBFRunAtStartup
    {
		// Constants
		//
        public const string RUN_AT_STARTUP_ROOT_KEY = "HKEY_CURRENT_USER";

        public const string RUN_AT_STARTUP_REG_KEY =
            "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

        public const string RUN_AT_STARTUP_FULL_KEY =
            RUN_AT_STARTUP_ROOT_KEY + "\\" + RUN_AT_STARTUP_REG_KEY;

        public const string RUN_AT_STARTUP_VERIFY_PREFIX = "Verify";

        // Constructors
        //
        //-------------------------------------------------------------------
        private PBFRunAtStartup()
        {
        }
        
        // Class methods
        //
        ///-------------------------------------------------------------------
        /// <summary>
        /// Verifies that the 'run at startup' key can be created. It attempts
        /// to create, retrieve, and delete a temporary key prefixed with
        /// '<seealso cref="RUN_AT_STARTUP_VERIFY_PREFIX"'/>
        /// </summary>
        /// <param name="theApplicationName"></param>
        /// <returns>
        /// true if the key can be created for this user, false if not
        /// </returns>
        ///-------------------------------------------------------------------
        public static bool CanRunAtStartup( string theApplicationName )
        {
            string verifyAppName = RUN_AT_STARTUP_VERIFY_PREFIX + theApplicationName;

            SetRunAtStartup( verifyAppName );

            bool retBool = GetRunAtStartup( verifyAppName );

            DeleteRunAtStartup( verifyAppName );

            return retBool;
        }

        //-------------------------------------------------------------------
        public static bool GetRunAtStartup( string theApplicationName )
        {
            string retStr =
                (string)Registry.GetValue( RUN_AT_STARTUP_FULL_KEY, theApplicationName,
                                           string.Empty );

            return ! string.IsNullOrEmpty( retStr );
        }
        
        //-------------------------------------------------------------------
        public static bool SetRunAtStartup( string theApplicationName )
        {
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey( RUN_AT_STARTUP_REG_KEY, true );

            if (rkApp != null)
            {
                try
                {
                    rkApp.SetValue( theApplicationName,
                                    Assembly.GetExecutingAssembly().Location );

                    return GetRunAtStartup( theApplicationName );
                }
                catch
                {
                }
            }

            return false;
        }

        //-------------------------------------------------------------------
        public static bool DeleteRunAtStartup( string theApplicationName )
        {
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey( RUN_AT_STARTUP_REG_KEY, true );

            if (rkApp != null)
            {
                try
                {
                    rkApp.DeleteValue( theApplicationName );
                }
                catch
                {
                }

                return GetRunAtStartup( theApplicationName );
            }

            return false;
        }

    } // class - PBFRunAtStartup

} // namespace
