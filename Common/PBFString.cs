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
        // Constructors
        //
        //-------------------------------------------------------------------
        public PBFString()
        {
        } // Constructor - default


        // Class methods
        //

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
        /// Returns an "s" string if <paramref name="theUInt"/> is not '1',
        /// else an empty string.
        /// </summary>
        /// <param name="theUInt"></param>
        ///-------------------------------------------------------------------
        public static string Plural( uint? theUInt )
        {
            return ((theUInt == null) || (theUInt == 1)) ? string.Empty : "s";
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns an "s" string if <paramref name="theLong"/> is not '1',
        /// else an empty string.
        /// </summary>
        /// <param name="theLong"></param>
        ///-------------------------------------------------------------------
        public static string Plural( long? theLong )
        {
            return ((theLong == null) || (theLong == 1)) ? string.Empty : "s";
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns an "s" string if 'theNumber' is not '1', else
        /// an empty string.
        /// </summary>
        /// <param name="theULong"></param>
        ///-------------------------------------------------------------------
        public static string Plural( ulong? theULong )
        {
            return ((theULong == null) || (theULong == 1)) ? string.Empty : "s";
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns an "s" string if 'theNumber' is not '1', else
        /// an empty string.
        /// </summary>
        /// <param name="theDoub"></param>
        ///-------------------------------------------------------------------
        public static string Plural( double? theDoub )
        {
            return ((theDoub == null) || (theDoub == 1.0)) ? string.Empty : "s";
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
        /// Returns the concatenation of:  (theUInt) (theStr)[s]
        /// </summary>
        /// <param name="theUInt"></param>
        /// <param name="theStr"></param>
        ///-------------------------------------------------------------------
        public static string PluralStr( uint? theUInt, string theStr )
        {
            return ((theUInt == null) ? "0" : theUInt.ToString()) + " " + theStr + Plural( theUInt );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns the concatenation of:  (theLong) (theStr)[s]
        /// </summary>
        /// <param name="theLong"></param>
        /// <param name="theStr"></param>
        ///-------------------------------------------------------------------
        public static string PluralStr( long? theLong, string theStr )
        {
            return ((theLong == null) ? "0" : theLong.ToString()) + " " + theStr + Plural( theLong );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns the concatenation of:  (theULong) (theStr)[s]
        /// </summary>
        /// <param name="theULong"></param>
        /// <param name="theStr"></param>
        ///-------------------------------------------------------------------
        public static string PluralStr( ulong? theULong, string theStr )
        {
            return ((theULong == null) ? "0" : theULong.ToString()) + " " + theStr + Plural( theULong );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns the concatenation of:  (theDoub) (theStr)[s]
        /// </summary>
        /// <param name="theDoub"></param>
        /// <param name="theStr"></param>
        ///-------------------------------------------------------------------
        public static string PluralStr( double? theDoub, string theStr )
        {
            return ((theDoub == null) ? "0" : theDoub.ToString()) + " " + theStr + Plural( theDoub );
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

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns 'true' if 'theUInt' is not '1'.
        /// </summary>
        /// <param name="theUInt"></param>
        ///-------------------------------------------------------------------
        public static bool IsPlural( uint? theUInt )
        {
            return ! ((theUInt == null) || (theUInt == 1));
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns 'true' if 'theLong' is not '1'.
        /// </summary>
        /// <param name="theLong"></param>
        ///-------------------------------------------------------------------
        public static bool IsPlural( long? theLong )
        {
            return ! ((theLong == null) || (theLong == 1));
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns 'true' if 'theULong' is not '1'.
        /// </summary>
        /// <param name="theULong"></param>
        ///-------------------------------------------------------------------
        public static bool IsPlural( ulong? theULong )
        {
            return ! ((theULong == null) || (theULong == 1));
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns 'true' if 'theDoub' is not '1'.
        /// </summary>
        /// <param name="theDoub"></param>
        ///-------------------------------------------------------------------
        public static bool IsPlural( double? theDoub )
        {
            return ! ((theDoub == null) || (theDoub == 1.0));
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns the first character of 'theString', or
        /// a null '\0' character if 'theString' is empty or null.
        /// </summary>
        /// <param name="theString"></param>
        ///-------------------------------------------------------------------
        public static char FirstChar( string theString )
        {
            if ((theString != null) && (theString.Length > 0))
            {
                return theString[ 0 ];
            }

            return '\0';
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns 'theString' with the first character capitalized, or
        /// an empty string if 'theString' is null or empty.
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="toLower"></param>
        ///-------------------------------------------------------------------
        public static string CapitalizeFirstChar( string theString, bool toLower = false )
        {
            if ((theString != null) && (theString.Length > 0))
            {                    
                return char.ToUpper( theString[ 0 ] ) +
                       (toLower ? theString.Substring( 1 ).ToLower() : theString.Substring( 1 ));
            }

            return string.Empty;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns the first word of 'theString', not including the
        /// word-ending space ' ' character.
        /// </summary>
        /// <param name="theString"></param>
        ///-------------------------------------------------------------------
        public static string FirstWord( string theString )
        {
            if (theString != null)
            {
                int endPos = 0;

                int theStringLen = theString.Length;

                if (theStringLen > 0)
                {
                    while (endPos < theStringLen)
                    {
                        if (theString[ endPos ] == ' ')
                        {
                            return theString.Substring( 0, endPos );
                        }

                        endPos++;
                    }             
                }
            }

            return theString;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Removes the first character of 'theString', or 'theString' if
        /// 'theString' is empty or null.
        /// </summary>
        /// <param name="theString"></param>
        ///-------------------------------------------------------------------
        public static string RemoveFirstChar( string theString )
        {
            if (theString != null)
            {
                int theStringLen = theString.Length;

                if (theStringLen == 1)
                {
                    return string.Empty;
                }
                else
                if (theStringLen >= 2)
                {
                    return theString.Substring( 1 );
                }
            }

            return theString;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Removes the first word of 'theString', including the word-ending
        /// space ' ' character.
        /// </summary>
        /// <param name="theString"></param>
        ///-------------------------------------------------------------------
        public static string RemoveFirstWord( string theString )
        {
            if (theString != null)
            {
                int endPos = 0;

                int theStringLen = theString.Length;

                if (theStringLen > 0)
                {
                    while (endPos < theStringLen)
                    {
                        if (theString[ endPos ] == ' ')
                        {
                            break;
                        }

                        endPos++;
                    }
             
                    if (endPos < (theStringLen - 1))
                    {
                        return theString.Substring( endPos + 1 );
                    }
                }
            }

            return theString;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Removes the first occurrence of 'theRemoveString' in 'theString'.
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="theRemoveString"></param>
        ///-------------------------------------------------------------------
        public static string RemoveSubstring( string theString, string theRemoveString )
        {
            int ioRemoveString = theString.IndexOf( theRemoveString );

            if (ioRemoveString >= 0)
            {
                return theString.Remove( ioRemoveString, theRemoveString.Length );
            }

            return theString;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Removes the first occurrence of 'theRemoveString' in 'theString'.
        /// </summary>
        /// <param name="theString"></param>
        ///-------------------------------------------------------------------
        public static string StripCRLF( string theString )
        {
            return theString.Replace( "\r\n", "" );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns the last character of 'theString', or
        /// a null '\0' character if 'theString' is empty or null.
        /// </summary>
        /// <param name="theString"></param>
        ///-------------------------------------------------------------------
        public static char LastChar( string theString )
        {
            try
            {
                int theStringLen = theString.Length;

                if (theStringLen > 0)
                {
                    return theString[ theStringLen - 1 ];
                }
            }
            catch
            {   
            }

            return '\0';
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Removes the last character of 'theString', or 'theString' if
        /// 'theString' is empty or null.
        /// </summary>
        /// <param name="theString"></param>
        ///-------------------------------------------------------------------
        public static string RemoveLastChar( string theString )
        {
            if (theString != null)
            {
                int theStringLen = theString.Length;

                if (theStringLen == 1)
                {
                    return string.Empty;
                }
                else
                if (theStringLen >= 2)
                {
                    return theString.Substring( 0, (theStringLen - 1) );
                }
            }

            return theString;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// EndsWith
        /// </summary>
        ///-------------------------------------------------------------------
        public static bool EndsWith( string theString, string theEndsWithString )
        {
            int theStringLength         = theString.Length;
            int theEndsWithStringLength = theEndsWithString.Length;

            if (theEndsWithStringLength <= theString.Length)
            {
                if (theString.Substring( (theStringLength - theEndsWithStringLength) ) == theEndsWithString)
                {
                    return true;
                }
            }

            return false;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns the first character of 'theString', or
        /// a null '\0' character if 'theString' is empty or null.
        /// </summary>
        /// <param name="theString"></param>
        ///-------------------------------------------------------------------
        public static int PosOfNthChar( string theString, char theChar, int n )
        {
            try
            {
                int theStringLen = theString.Length;

                if (theStringLen > 0)
                {
                    int pos = 0;
                    int nCount = 0;

                    foreach (char c in theString)
                    {
                        if (theString[ pos ] == theChar)
                        {
                            nCount++;

                            if (nCount == n)
                            {
                                return pos;
                            }
                        }

                        pos++;
                    }
                }
            }
            catch
            {   
            }

            return 0;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Return the first 'theMaxLength' chars of 'theString', or
        /// 'theString' is the length is less than 'theMaxLength'.
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="theMaxLength"></param>
        ///-------------------------------------------------------------------
        public static string Trunc( string theString, int theMaxLength )
        {
            if (theString.Length > theMaxLength)
            {
                return theString.Substring( 0, theMaxLength );
            }

            return theString;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns a new string with a length less than or equal to 'theMaxLength'.
        /// If 'theString' cannot fit within 'theMaxLength', then it is
        /// truncated and '..." is appended to it to a length of exactly 'theMaxLength'.
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="theMaxLength"></param>
        ///-------------------------------------------------------------------
        public static string Ellipsis( string theString, int theMaxLength )
        {
            if (string.IsNullOrEmpty(theString))
            {
                return string.Empty;
            }

            const string ELLIPSIS_STR = "...";

            int minEllipsisStrLen = 1 + ELLIPSIS_STR.Length;

            if ( (theString.Length < minEllipsisStrLen) ||
                 (theMaxLength < minEllipsisStrLen) )
            {
                return theString;
            }

            if (theString.Length > theMaxLength)
            {
                return theString.Substring( 0, (theMaxLength - ELLIPSIS_STR.Length) ) +
                       ELLIPSIS_STR;
            }

            return theString;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// IsVowel
        /// </summary>
        /// <param name="theChar"></param>
        /// <param name="includeY"></param>
        /// <param name="includeW"></param>
        /// <remarks>See IsConsonant(), similar but not inverse method</remarks>
        ///-------------------------------------------------------------------
        public static bool IsVowel( char theChar, bool includeY = true, bool includeW = false )
        {
            const string vowels = "aeiou";

            char charLower = char.ToLower( theChar );

            return (vowels.IndexOf( charLower ) >= 0) ||
                   (includeY && (charLower == 'y')) ||
                   (includeW && (charLower == 'w'));
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// IsConsonant
        /// </summary>
        /// <param name="theChar"></param>
        /// <param name="includeY"></param>
        /// <param name="includeW"></param>
        /// <remarks>See IsVowel(), similar but not inverse method</remarks>
        ///-------------------------------------------------------------------
        public static bool IsConsonant( char theChar, bool includeY = false, bool includeW = true )
        {
            const string consonants = "bcdfghjklmnpqrstvxz";

            char charLower = char.ToLower( theChar );

            return (consonants.IndexOf( charLower ) >= 0) ||
                   (includeY && (charLower == 'y')) ||
                   (includeW && (charLower == 'w'));
        }

        //-------------------------------------------------------------------
        public static string Pad( string theString, char thePadChar, int theWidth )
        {
            string padStr = theString;

            while (padStr.Length < theWidth)
            {
                padStr = thePadChar + padStr;
            }

            return padStr;
        }

        //-------------------------------------------------------------------
        public static string LZPad( string theString, int theWidth )
        {
            return Pad( theString, '0', theWidth );
        }

        //-------------------------------------------------------------------
        public static string LSPad( string theString, int theWidth )
        {
            return Pad( theString, ' ', theWidth );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Removes any/all trailing '0' characters from 'theDoubleString'.
        /// If, after removing a trailing '0', a decimal point '.' character
        /// remains, it is removed as well.
        /// </summary>
        /// <param name="theDoubleString"></param>
        /// <returns>
        /// Returns 'theDoubleString' with no trailing zeroes
        /// </returns>
        ///-------------------------------------------------------------------
        public static string StripTrailingZeroes( string theDoubleString )
        {
            string retStr = theDoubleString;

            while (retStr.Length > 1)
            {
                if (LastChar( retStr ) == '0')
                {
                    retStr = RemoveLastChar( retStr );
                    
                    if (LastChar( retStr ) == '.')
                    {
                        retStr = RemoveLastChar( retStr );
                    
                        return retStr;
                    }
                    
                    continue;
                }

                break;
            }

            return retStr;
        }

        //-------------------------------------------------------------------
        public static string StripAllButDigits( string theString )
        {
            string retStr = string.Empty;

            foreach (char c in theString)
            {
                if (char.IsDigit( c ))
                {
                    retStr += c;
                }
            }

            return retStr;
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

    } // class - PBFString

} // namespace
