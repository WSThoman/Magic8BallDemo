//---------------------------------------------------------------------------
//
// Copyright © 2015-2024 Paragon Bit Foundry.  All Rights Reserved.
//
//---------------------------------------------------------------------------
using System;

namespace PBFCommon
{
    public sealed class PBFString
    {
        #region Constructors

        //-------------------------------------------------------------------
        public PBFString()
        {
        } // Constructor - default

        #endregion

        #region Class methods

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns an "s" string if <paramref name="theInt"/> is not '1',
        /// else an empty string.
        /// </summary>
        /// <param name="theInt"></param>
        ///-------------------------------------------------------------------
        public static string Plural( int? theInt )
        {
            return ((theInt == null) || (theInt == 1)) ? string.Empty : "s";
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns the concatenation of:  (theInt) (theStr)[s]
        /// </summary>
        /// <param name="theInt"></param>
        /// <param name="theStr"></param>
        ///-------------------------------------------------------------------
        public static string PluralStr( int? theInt, string theStr )
        {
            return ((theInt == null) ? "0" : theInt.ToString()) + " " + theStr + Plural( theInt );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns 'true' if 'theInt' is not '1'.
        /// </summary>
        /// <param name="theInt"></param>
        ///-------------------------------------------------------------------
        public static bool IsPlural( int? theInt )
        {
            return ! ((theInt == null) || (theInt == 1));
        }

        //-------------------------------------------------------------------
        public static bool IsPalindrome( string theString, bool ignoreCase = false,
                                         bool countEmptyStrings = true,
                                         bool countOneCharStrings = true )
        {
            // Test Cases:
            //
//            bool b0  = PBFString.IsPalindrome( null );
//            bool b1  = PBFString.IsPalindrome( "" );
//            bool b2  = PBFString.IsPalindrome( "a" );
//            bool b3  = PBFString.IsPalindrome( "aa" );
//            bool b4  = PBFString.IsPalindrome( "ab" );
//            bool b5  = PBFString.IsPalindrome( "aba" );
//            bool b6  = PBFString.IsPalindrome( "aBba", false );
//            bool b7  = PBFString.IsPalindrome( "abcba" );
//            bool b8  = PBFString.IsPalindrome( 0 );
//            bool b9  = PBFString.IsPalindrome( 4772774 );
//            bool b10 = PBFString.IsPalindromeIC( "aBba" );
//            bool b11 = PBFString.IsPalindrome( new DateTime( 2021, 01, 20 ), true );

            // Verify parameters
            //
            if (theString == null)
            {
                return false;
            }

            if (theString.Length == 0)
            {
                return countEmptyStrings;
            }

            if (theString.Length == 1)
            {
                return countOneCharStrings;
            }

            // Compare characters starting at both ends, and
            // proceeding toward the middle character(s)
            //
            int strLen = theString.Length;

            int strLenHalf = strLen / 2;

            for (int c = 0; c < strLenHalf; c++)
            {
                if (ignoreCase)
                {
                    if (char.ToUpper( theString[ c ] ) !=
                        char.ToUpper( theString[ (strLen - 1) - c ] ))
                    {
                        return false;
                    }
                }
                else
                {
                    if (theString[ c ] != theString[ (strLen - 1) - c ])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        //-------------------------------------------------------------------
        public static bool IsPalindromeIC( string theString,
                                           bool countEmptyStrings = true,
                                           bool countOneCharStrings = true )
        {
            return IsPalindrome( theString, true,
                                 countEmptyStrings, countOneCharStrings );
        }

        //-------------------------------------------------------------------
        public static bool IsPalindrome( ulong wholeNumber )
        {
            return IsPalindrome( wholeNumber.ToString() );
        }

        //-------------------------------------------------------------------
        public static bool IsPalindrome( DateTime theDate,
                                         bool includeCentury = false )
        {
            // Jan 20, 2021 to the 29th generates '12021' to '12921'
            // Including the century (20) for Jan 20th still generates '1202021'
            //
            int century = theDate.Date.Year / 100;

            int year = theDate.Date.Year - (century * 100);

            string dateStr = theDate.Date.Month.ToString() +
                             theDate.Date.Day.ToString() +
                             (includeCentury ? century.ToString() : string.Empty) +
                             year.ToString();

            return IsPalindrome( dateStr );
        }

        #endregion

    } // class - PBFString

} // namespace
