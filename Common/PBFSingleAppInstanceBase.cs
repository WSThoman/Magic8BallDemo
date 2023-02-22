//---------------------------------------------------------------------------
//
// Copyright © 2017-2020 Paragon Bit Foundry.  All Rights Reserved.
//
//---------------------------------------------------------------------------
using System;
using System.Threading;

namespace PBFCommon
{
    public class PBFSingleAppInstanceBase : IDisposable
    {
        // Data members
        //
        private static Mutex mSAIMutex = null;
        
        // Properties
        //
        private bool IsDisposed { get { return mSAIMutex == null; } }

        // Constructor (singleton)
        //
        //-------------------------------------------------------------------
        private PBFSingleAppInstanceBase()
        {
        }

        // Class methods
        //
        //-------------------------------------------------------------------
        public static bool IsAppInstanceRunning( string theAppID )
        {
            mSAIMutex = new Mutex( true, theAppID, out bool saiResultCreatedNew );

            return ! saiResultCreatedNew;
        }

        // Dispose
        //
        #region Dispose

        //-------------------------------------------------------------------
        public void Dispose()
        {
            Dispose( true );
        }

        //-------------------------------------------------------------------
        protected virtual void Dispose( bool disposing )
        {
            if (IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                mSAIMutex.ReleaseMutex();

                mSAIMutex = null;
            }
        }
        
        #endregion Dispose

    } // class - PBFSingleAppInstanceBase

} // namespace
