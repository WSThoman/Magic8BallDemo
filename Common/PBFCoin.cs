//---------------------------------------------------------------------------
//
// Copyright © 2017-2020 Paragon Bit Foundry.  All Rights Reserved.
//
//---------------------------------------------------------------------------
using System;

namespace PBFCommon
{
    public class PBFCoin
    {
        // Constants
        //
        private readonly string[] mSides =
        {
            "Heads",
            "Tails"
        };

        public enum SideIndexes
        {
            Heads = 0,
            Tails = 1
        }

        // Member variables
        //
        private readonly Random mRandom = null;

        // Properties
        //
        #region Properties

        public string Side { get { return mSides[ (int)SideIndex ]; } }

        public SideIndexes SideIndex { get; private set; } = SideIndexes.Heads;

        public bool IsHeads { get { return SideIndex == SideIndexes.Heads; } }

        public bool IsTails { get { return ! IsHeads; } }

        #endregion Properties

        // Constructors
        //
        //-------------------------------------------------------------------
        public PBFCoin( Random theRandomObj = null )
        {
            if (theRandomObj != null)
            {
                mRandom = theRandomObj;
            }
            else
            {
                mRandom = new Random( DateTime.Now.GetHashCode() );
            }

            Flip();

        } // Constructor - default

        // Class methods
        //
        #region Class methods

        ///-------------------------------------------------------------------
        /// <summary>
        /// Selects a random side or 'flips' the coin.
        /// </summary>
        /// <returns>
        /// true if the coin flip is 'heads', false for 'tails'.
        /// </returns>
        /// <remarks>
        /// Sets 'mSideIndex' to a random number, which is later used by
        /// the read-only property 'Side' to return the corresponding string.
        /// </remarks>
        ///-------------------------------------------------------------------
        /// 
        public bool Flip()
        {
            SideIndex = (SideIndexes)mRandom.Next( mSides.Length );

            return IsHeads;
        }
                
        //-------------------------------------------------------------------
        public void SetHeads()
        {
            SideIndex = SideIndexes.Heads;
        }

        //-------------------------------------------------------------------
        public void SetTails()
        {
            SideIndex = SideIndexes.Tails;
        }

        #endregion Class methods

    } // class - PBFCoin

} // namespace
