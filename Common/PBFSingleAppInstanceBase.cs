//---------------------------------------------------------------------------
//
// Copyright © 2017-2024 Paragon Bit Foundry.  All Rights Reserved.
//
//---------------------------------------------------------------------------
using System;
using System.Threading;

namespace PBFCommon
{
    public class PBFSingleAppInstanceBase : IDisposable
    {
        #region Data members

        private static Mutex mSAIMutex = null;

        #endregion

        #region Properties

        private bool IsDisposed { get { return mSAIMutex == null; } }

        #endregion

        #region Constructor (singleton)

        //-------------------------------------------------------------------
        private PBFSingleAppInstanceBase()
        {
        }

        #endregion

        #region Class methods

        //-------------------------------------------------------------------
        public static bool IsAppInstanceRunning( string theAppID )
        {
            mSAIMutex = new Mutex( true, theAppID, out bool saiResultCreatedNew );

            return ! saiResultCreatedNew;
        }

        #endregion

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
        
        #endregion

    } // class - PBFSingleAppInstanceBase

} // namespace
