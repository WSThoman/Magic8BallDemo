//---------------------------------------------------------------------------
//
// Copyright © 2018-2020 Paragon Bit Foundry.  All Rights Reserved.
//
//---------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace PBFCommon
{
    public abstract class PBFRandomPrevBase
    {
        // Constants
        //
        public const int DEF_MIN = 1;
        public const int DEF_MAX = 10;

        // Properties
        //
        #region Properties

        public int Value { get; private set; } = DEF_MIN;

        public int PreviousValue { get; private set; } = 0;

        public int Min { get; private set; } = DEF_MIN;
       
        public int Max { get; private set; } = DEF_MAX;

        public int Range { get { return (Max - Min) + 1; } }

        public int RangeExcludeMax { get { return Max - Min; } }

        public List<int> PreviousList { get; private set; } = null;

        public int PreviousListSize { get; private set; } = 0;

        public bool HasPreviousList { get { return PreviousListSize > 0; } }

        #endregion Properties

        // Constructors
        //
        #region Constructors

        //-------------------------------------------------------------------
        public PBFRandomPrevBase( int theMinRange = DEF_MIN,
                                  int theMaxRange = DEF_MAX,
                                  int thePreviousListSize = 0 )
        {
            Min = theMinRange;
            Max = theMaxRange;

            Value         = Min;
            PreviousValue = Min;

            // Correct invalid values
            //
            if (Min < 0)
            {
                Min = DEF_MIN;
            }

            if (Max <= Min)
            {
                Max = Min;
            }
             
            PreviousListSize = thePreviousListSize;

            if (Min == Max)
            {
                PreviousListSize = 0;
            }

            if (HasPreviousList)
            {
                PreviousList = new List<int>();
            }

        } // Constructor - default

        #endregion Constructors

        // Abstract class methods
        //
        //-------------------------------------------------------------------
        #region Abstract class methods

        protected abstract int CallNext( int theMaxRange );

        #endregion Abstract class methods

        // Class methods
        //
        #region Class methods

        //-------------------------------------------------------------------
        public void Clear()
        {
            Min = DEF_MIN;
            Max = DEF_MAX;

            Value         = Min;
            PreviousValue = Min;

            if (HasPreviousList)
            {
                PreviousList.Clear();
            }
        }

        //-------------------------------------------------------------------
        public int Next( int theMaxRange )
        {
            // Note the cast to correctly call the 'int?' variant and prevent
            // an infinite loop
            //
            return Next( (int?)theMaxRange );
        }

        //-------------------------------------------------------------------
        public int Next( int? theMaxRange = null )
        {
            PreviousValue = Value;

            NextOnly( theMaxRange );

            if ( ! HasPreviousList)
            {
                return Value;
            }

            // Update the 'previous list'
            //
            TruncatePreviousList();

            AddFirstItem();

            return Value;
        }

        //-------------------------------------------------------------------
        public int NextOnly( int theMaxRange )
        {
            // Note the cast to correctly call the 'int?' variant and prevent
            // an infinite loop
            //
            return NextOnly( (int?)theMaxRange );
        }

        //-------------------------------------------------------------------
        public int NextOnly( int? theMaxRange = null )
        {
            if (theMaxRange == null)
            {
                Value = GetNextValue();
            }
            else
            {
               Value = CallNext( (int)theMaxRange );
            }

            return Value;
        }

        //-------------------------------------------------------------------
        private int GetNextValue()
        {
            return CallNext( Range ) + Min;
        }

        //-------------------------------------------------------------------
        public int NextButNotPrev()
        {
            return NextButNotPrev( null );
        }

        //-------------------------------------------------------------------
        public int NextButNotPrev( int? theMaxRange = null )
        {
            PreviousValue = Value;

            if (theMaxRange == null)
            {
                Value = GetNextValue();
            }
            else
            {
                Value = CallNext( (int)theMaxRange );
            }

            if (HasPreviousList)
            {
                while (PreviousList.Contains( Value ))
                {
                    if (theMaxRange == null)
                    {
                        Value = GetNextValue();
                    }
                    else
                    {
                        Value = CallNext( (int)theMaxRange );
                    }
                }
            }
            else
            {
                while (Value == PreviousValue)
                {
                    if (theMaxRange == null)
                    {
                        Value = GetNextValue();
                    }
                    else
                    {
                        Value = CallNext( (int)theMaxRange );
                    }
                }
            }

            // Update the 'previous list'
            //
            TruncatePreviousList();

            AddFirstItem();

            return Value;
        }

        //-------------------------------------------------------------------
        public int NextButNot( int notThisInt, int? theMaxRange = null )
        {
            PreviousValue = Value;

            if (theMaxRange == null)
            {
                Value = GetNextValue();
            }
            else
            {
                Value = CallNext( (int)theMaxRange );
            }

            while (Value == notThisInt)
            {
                if (theMaxRange == null)
                {
                    Value = GetNextValue();
                }
                else
                {
                    Value = CallNext( (int)theMaxRange );
                }
            }

            // Update the 'previous list'
            //
            TruncatePreviousList();

            AddFirstItem();

            return Value;
        }

        //-------------------------------------------------------------------
        private void TruncatePreviousList()
        {
            if (HasPreviousList)
            {
                while (PreviousList.Count >= PreviousListSize)
                {
                    DeleteLastItem();
                }
            }
        }

        //-------------------------------------------------------------------
        private void DeleteLastItem()
        {
            if (HasPreviousList)
            {
                int plCount = PreviousList.Count;

                if (plCount > 0)
                {
                    PreviousList.RemoveAt( plCount - 1 );
                }
            }
        }

        //-------------------------------------------------------------------
        private void AddFirstItem()
        {
            if (HasPreviousList)
            {
                PreviousList.Insert( 0, Value );
            }
        }

        #endregion Class methods

    } // class - PBFRandomPrevBase


    public sealed class PBFRandomPrevStandard : PBFRandomPrevBase
    {
        // Data members
        //
        private readonly Random mRandomStandard = null;

        // Constructors
        //
        #region Constructors

        //-------------------------------------------------------------------
        public PBFRandomPrevStandard( Random theRandomObjStandard = null,
                                      int theMinRange = DEF_MIN,
                                      int theMaxRange = DEF_MAX,
                                      int thePreviousListSize = 0 ) :
            base( theMinRange, theMaxRange, thePreviousListSize )
        {
            // Random
            //
            if (theRandomObjStandard != null)
            {
                mRandomStandard = theRandomObjStandard;
            }
            else
            {
                mRandomStandard = new Random();
            }
        }

        #endregion Constructors

        // Abstract method implementations
        //
        #region Abstract method implementations

        ///-------------------------------------------------------------------
        /// <summary>
        /// CallNext
        /// </summary>
        /// <param name="theMaxRange"></param>
        ///-------------------------------------------------------------------
        protected override int CallNext(int theMaxRange)
        {
            return mRandomStandard.Next( theMaxRange );
        }

        #endregion Abstract method implementations

    } // class - PBFRandomPrevStandard


    public sealed class PBFRandomPrevCrypto: PBFRandomPrevBase
    {
        // Data members
        //
        #region Data members

        private readonly RNGCryptoServiceProvider mRandomCrypto = null;

        private readonly byte[] mRandomUIntByteBuf = new byte[ sizeof(uint) ];

        private uint mGeneratedUInt = 0U;

        #endregion Data members

        // Constructors
        //
        #region Constructors

        //-------------------------------------------------------------------
        public PBFRandomPrevCrypto( RNGCryptoServiceProvider theRandomObjCrypto = null,
                                    int theMinRange = DEF_MIN,
                                    int theMaxRange = DEF_MAX,
                                    int thePreviousListSize = 0 ) :
            base( theMinRange, theMaxRange, thePreviousListSize )
        {
            // Cryptographically-secure random number generator
            //
            if (theRandomObjCrypto != null)
            {
                mRandomCrypto = theRandomObjCrypto;
            }
            else
            {
                mRandomCrypto = new RNGCryptoServiceProvider();
            }
        }

        #endregion Constructors

        // Abstract method implementations
        //
        #region Abstract method implementations

        ///-------------------------------------------------------------------
        /// <summary>
        /// CallNext
        /// </summary>
        /// <param name="theMaxRange"></param>
        ///-------------------------------------------------------------------
        protected override int CallNext(int theMaxRange)
        {
            long rangeDiff = (theMaxRange - Min) + 1;

            long upperLimit = uint.MaxValue / rangeDiff * rangeDiff;

            do
            {
                mGeneratedUInt = GenerateRandomUInt();

            } while (mGeneratedUInt >= upperLimit);

            int tmp = (int)( (Min + (mGeneratedUInt % rangeDiff)) - 1 );

            return tmp;
        }

        #endregion Abstract method implementations

        // Class methods
        //
        #region Class methods

        //-------------------------------------------------------------------
        private uint GenerateRandomUInt()
        {
            mRandomCrypto.GetBytes( mRandomUIntByteBuf );
    
            return BitConverter.ToUInt32( mRandomUIntByteBuf, 0 );
        }

        #endregion Class methods

    } // class - PBFRandomPrevCrypto


    // ORIG
    //
    [Obsolete("Use PBFRandomPrev instead")]
    public sealed class PBFRandomPrevORIG
    {
        // Constants
        //
        public const int DEF_MIN = 1;
        public const int DEF_MAX = 10;

        // Member vars
        //
        private readonly Random mRandom = null;

        // Properties
        //
        #region Properties

        public int Value         { get; private set; } = DEF_MIN;

        public int PreviousValue { get; private set; } = 0;

        public int Min { get; private set; } = DEF_MIN;
       
        public int Max { get; private set; } = DEF_MAX;

        public int Range { get { return (Max - Min) + 1; } }

        public List<int> PreviousList { get; private set; } = null;

        public int PreviousListSize { get; private set; } = 0;

        public bool HasPreviousList { get { return PreviousListSize > 0; } }

        #endregion Properties

        // Constructors
        //
        //-------------------------------------------------------------------
        public PBFRandomPrevORIG( Random theRandomObj = null,
                                  int theMinRange = 1,
                                  int theMaxRange = 10,
                                  int thePreviousListSize = 0 )
        {
            // Random
            //
            if (theRandomObj != null)
            {
                mRandom = theRandomObj;
            }
            else
            {
                mRandom = new Random();
            }

            Value         = DEF_MIN;
            PreviousValue = DEF_MIN;

            Min = theMinRange;
            Max = theMaxRange;

            // Correct invalid values
            //
            if (Min < 0)
            {
                Min = DEF_MIN;
            }

            if (Max <= Min)
            {
                Max = Min;
            }
             
            PreviousListSize = thePreviousListSize;

            if (Min == Max)
            {
                PreviousListSize = 0;
            }

            if (HasPreviousList)
            {
                PreviousList = new List<int>();
            }

        } // Constructor - default

        // Class methods
        //

        ///-------------------------------------------------------------------
        /// <summary>
        /// CallNext
        /// </summary>
        /// <param name="theMaxRange"></param>
        /// <returns></returns>
        /// <remarks>
        /// Abstract in base, specific in Standard and Cryptographic
        /// </remarks>
        ///-------------------------------------------------------------------
        private int CallNext( int theMaxRange )
        {
            return mRandom.Next( theMaxRange );
        }

        //-------------------------------------------------------------------
        public void Clear()
        {
            Value         = DEF_MIN;
            PreviousValue = DEF_MIN;
        
            Min = DEF_MIN;
            Max = DEF_MAX;

            if (HasPreviousList)
            {
                PreviousList.Clear();
            }
        }

        //-------------------------------------------------------------------
        public int Next( int theMaxRange )
        {
            return Next( (int?)theMaxRange );
        }

        //-------------------------------------------------------------------
        public int Next( int? theMaxRange = null )
        {
            PreviousValue = Value;
            
            if (theMaxRange == null)
            {
                Value = GetNextValue();
            }
            else
            {
               Value = CallNext( (int)theMaxRange );
            }

            if ( ! HasPreviousList)
            {
                return Value;
            }

            // Update the 'previous list'
            //
            TruncatePreviousList();

            AddFirstItem();

            return Value;
        }

        //-------------------------------------------------------------------
        private int GetNextValue()
        {
            return CallNext( Range ) + Min;
        }

        //-------------------------------------------------------------------
        public int NextButNotPrev()
        {
            return NextButNotPrev( null );
        }

        //-------------------------------------------------------------------
        public int NextButNotPrev( int? theMaxRange = null )
        {
            PreviousValue = Value;

            if (theMaxRange == null)
            {
                Value = GetNextValue();
            }
            else
            {
                Value = CallNext( (int)theMaxRange );
            }

            if (HasPreviousList)
            {
                while (PreviousList.Contains( Value ))
                {
                    if (theMaxRange == null)
                    {
                        Value = GetNextValue();
                    }
                    else
                    {
                        Value = CallNext( (int)theMaxRange );
                    }
                }
            }
            else
            {
                while (Value == PreviousValue)
                {
                    if (theMaxRange == null)
                    {
                        Value = GetNextValue();
                    }
                    else
                    {
                        Value = CallNext( (int)theMaxRange );
                    }
                }
            }

            // Update the 'previous list'
            //
            TruncatePreviousList();

            AddFirstItem();

            return Value;
        }

        //-------------------------------------------------------------------
        private void TruncatePreviousList()
        {
            if (HasPreviousList)
            {
                while (PreviousList.Count >= PreviousListSize)
                {
                    DeleteLastItem();
                }
            }
        }

        //-------------------------------------------------------------------
        private void DeleteLastItem()
        {
            if (HasPreviousList)
            {
                int plCount = PreviousList.Count;

                if (plCount > 0)
                {
                    PreviousList.RemoveAt( plCount - 1 );
                }
            }
        }

        //-------------------------------------------------------------------
        private void AddFirstItem()
        {
            if (HasPreviousList)
            {
                PreviousList.Insert( 0, Value );
            }
        }

    } // class - PBFRandomPrevORIG

} // namespace
