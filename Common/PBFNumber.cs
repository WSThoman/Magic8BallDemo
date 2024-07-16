//---------------------------------------------------------------------------
//
// Copyright © 2015-2024 Paragon Bit Foundry.  All Rights Reserved.
//
//---------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace PBFCommon
{
    public static class PBFNumber
    {
        // Constants
        //
        public const string SYM_SIZE          = "N";    // Population size
        public const string SYM_MEAN          = "μ";    // Population mean
        public const string SYM_MEDIAN        = "M";    // Population median
        public const string SYM_MODE          = "Mo";   // Population mode
        public const string SYM_SAMP_MEMBERS  = "n";    // Sample size
        public const string SYM_SAMP_MEAN     = "x̄";    // Sample mean, 'x bar'
        public const string SYM_SAMP_MEDIAN   = "x̃";    // Sample median, 'x tilde'
        public const string SYM_SAMP_STD_DEV  = "s";    // Sample standard deviation
        public const string SYM_SAMP_VARIANCE = "s²";   // Sample variance
        public const string SYM_PROPORTION    = "p";    // Population proportion
        public const string SYM_SAMP_PROP     = "p̂";    // Sample proportion
        public const string SYM_STD_DEV       = "σ";    // Standard deviation
        public const string SYM_DISTRIBUTION  = "X~";   // Distribution
        public const string SYM_VARIANCE      = "σ²";   // Variance
        public const string SYM_SUM           = "∑";    // Summation
        public const string SYM_DOUBLE_SUM    = "∑∑";   // Double summation
        public const string SYM_PI            = "π";    // Pi

        public static readonly uint[] FibSeeds =
            new uint[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89 };
        
        // Class methods
        //
        #region Class methods

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns an 'sbyte' represented by 'theSByteString', or 'theDefaultSByte'
        /// if the string is empty or not-a-number (NaN).
        /// </summary>
        /// <param name="theSByteString"></param>
        /// <param name="theDefaultSByte"></param>
        ///-------------------------------------------------------------------
        [SuppressMessage("Design", "CA1031:Do not catch general exception types")]
        public static sbyte StrToSByteDef( string theSByteString, sbyte theDefaultSByte = 0 )
        {
            sbyte retSByte = theDefaultSByte;

            if ( ! string.IsNullOrEmpty( theSByteString ))
            {
                try
                {
                    retSByte = sbyte.Parse( theSByteString );
                }
                catch (ArgumentException) {}
                catch (FormatException) {}
                catch (OverflowException)
                {
                }
            }

            return retSByte;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns a 'byte' represented by 'theByteString', or 'theDefaultByte'
        /// if the string is empty or not-a-number (NaN).
        /// </summary>
        /// <param name="theByteString"></param>
        /// <param name="theDefaultByte"></param>
        ///-------------------------------------------------------------------
        [SuppressMessage("Design", "CA1031:Do not catch general exception types")]
        public static byte StrToByteDef( string theByteString, byte theDefaultByte = 0 )
        {
            byte retByte = theDefaultByte;

            if ( ! string.IsNullOrEmpty( theByteString ))
            {
                try
                {
                    retByte = byte.Parse( theByteString );
                }
                catch (ArgumentNullException) {}
                catch (FormatException) {}
                catch (OverflowException)
                {
                }
            }

            return retByte;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns a 'short' represented by 'theShortString', or 'theDefaultShort'
        /// if the string is empty or not-a-number (NaN).
        /// </summary>
        /// <param name="theShortString"></param>
        /// <param name="theDefaultShort"></param>
        ///-------------------------------------------------------------------
        [SuppressMessage("Design", "CA1031:Do not catch general exception types")]
        public static short StrToShortDef( string theShortString, short theDefaultShort = 0 )
        {
            short retShort = theDefaultShort;

            if ( ! string.IsNullOrEmpty( theShortString ))
            {
                try
                {
                    retShort = short.Parse( theShortString );
                }
                catch (ArgumentNullException) {}
                catch (FormatException) {}
                catch (OverflowException)
                {
                }
            }

            return retShort;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns a 'ushort' represented by 'theUShortString', or 'theDefaultUShort'
        /// if the string is empty or not-a-number (NaN).
        /// </summary>
        /// <param name="theUShortString"></param>
        /// <param name="theDefaultUShort"></param>
        ///-------------------------------------------------------------------
        [SuppressMessage("Design", "CA1031:Do not catch general exception types")]
        public static ushort StrToUShortDef( string theUShortString, ushort theDefaultUShort = 0 )
        {
            ushort retUShort = theDefaultUShort;

            if ( ! string.IsNullOrEmpty( theUShortString ))
            {
                try
                {
                    retUShort = ushort.Parse( theUShortString );
                }
                catch (ArgumentNullException) {}
                catch (FormatException) {}
                catch (OverflowException)
                {
                }
            }

            return retUShort;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns an 'int' represented by 'theIntString', or 'theDefaultInt'
        /// if the string is empty or not-a-number (NaN).
        /// </summary>
        /// <param name="theIntString"></param>
        /// <param name="theDefaultInt"></param>
        ///-------------------------------------------------------------------
        [SuppressMessage("Design", "CA1031:Do not catch general exception types")]
        public static int StrToIntDef( string theIntString, int theDefaultInt = 0 )
        {
            int retInt = theDefaultInt;

            if ( ! string.IsNullOrEmpty( theIntString ))
            {
                try
                {
                    retInt = int.Parse( theIntString );
                }
                catch (ArgumentNullException) {}
                catch (FormatException) {}
                catch (OverflowException)
                {
                }
            }

            return retInt;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns a 'uint' represented by 'theUIntString', or 'theDefaultUInt'
        /// if the string is empty or not-a-number (NaN).
        /// </summary>
        /// <param name="theUIntString"></param>
        /// <param name="theDefaultUInt"></param>
        ///-------------------------------------------------------------------
        [SuppressMessage("Design", "CA1031:Do not catch general exception types")]
        public static uint StrToUIntDef( string theUIntString, uint theDefaultUInt = 0U )
        {
            uint retUInt = theDefaultUInt;

            if ( ! string.IsNullOrEmpty( theUIntString ))
            {
                try
                {
                    retUInt = uint.Parse( theUIntString );
                }
                catch (ArgumentNullException) {}
                catch (FormatException) {}
                catch (OverflowException)
                {
                }
            }

            return retUInt;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns a 'long' represented by 'theLongString', or
        /// 'theDefaultLong' if the string is empty or not-a-number (NaN).
        /// </summary>
        /// <param name="theLongString"></param>
        /// <param name="theDefaultLong"></param>
        ///-------------------------------------------------------------------
        [SuppressMessage("Design", "CA1031:Do not catch general exception types")]
        public static long StrToLongDef( string theLongString, long theDefaultLong = 0L )
        {
            long retLong = theDefaultLong;

            if ( ! string.IsNullOrEmpty( theLongString ))
            {
                try
                {
                    retLong = long.Parse( theLongString );
                }
                catch (ArgumentNullException) {}
                catch (FormatException) {}
                catch (OverflowException)
                {
                }
            }

            return retLong;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns a 'ulong' represented by 'theULongString', or
        /// 'theDefaultULong' if the string is empty or not-a-number (NaN).
        /// </summary>
        /// <param name="theULongString"></param>
        /// <param name="theDefaultULong"></param>
        ///-------------------------------------------------------------------
        [SuppressMessage("Design", "CA1031:Do not catch general exception types")]
        public static ulong StrToULongDef( string theULongString, ulong theDefaultULong = 0UL )
        {
            ulong retULong = theDefaultULong;

            if ( ! string.IsNullOrEmpty( theULongString ))
            {
                try
                {
                    retULong = ulong.Parse( theULongString );
                }
                catch (ArgumentNullException) {}
                catch (FormatException) {}
                catch (OverflowException)
                {
                }
            }

            return retULong;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns a 'double' represented by 'theDoubleString', or
        /// 'theDefaultLong' if the string is empty or not-a-number (NaN).
        /// </summary>
        /// <param name="theDoubleString"></param>
        /// <param name="theDefaultDouble"></param>
        ///-------------------------------------------------------------------
        [SuppressMessage("Design", "CA1031:Do not catch general exception types")]
        public static double StrToDoubleDef( string theDoubleString,
                                             double theDefaultDouble = 0.0 )
        {
            double retDoub = theDefaultDouble;

            if ( ! string.IsNullOrEmpty( theDoubleString ))
            {
                try
                {
                    retDoub = double.Parse( theDoubleString );
                }
                catch (ArgumentNullException) {}
                catch (FormatException) {}
                catch (OverflowException)
                {
                }
            }

            return retDoub;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns a string representation of 'theLong' with
        /// thousand separators.
        /// </summary>
        /// <param name="theLong"></param>
        ///-------------------------------------------------------------------
        public static string ToStringThousandSep( long theLong )
        {
            return theLong.ToString( "#,#" );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns a string representation of 'theDouble' with
        /// 'theDecimalPlaces' number of decimal places and with thousand
        /// separator characters.  Watch for commas, though, since other
        /// cultures use non-comma separators.
        /// </summary>
        /// <param name="theDouble"></param>
        /// <param name="theDecimalPlaces"></param>
        ///-------------------------------------------------------------------
        public static string ToThousandSep( double? theDouble, int theDecimalPlaces = 0 )
        {
            return (theDouble != null) ?
                    ( ((int)theDouble == 0) ? "0" : string.Empty) +
                    ((double)theDouble).ToString( "#,#." + new string( '#', theDecimalPlaces ) ) :
                    string.Empty;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns a string representation of 'theDouble' with
        /// 'theDecimalPlaces' number of decimal places.
        /// </summary>
        /// <param name="theDouble"></param>
        /// <param name="theDecimalPlaces"></param>
        ///-------------------------------------------------------------------
        public static string ToDecimalPlaces( double? theDouble, int theDecimalPlaces = 0 )
        {
            return (theDouble != null) ?
                       ((double)theDouble).ToString( "F" + theDecimalPlaces.ToString() ) :
                       string.Empty;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns a currency string representation of 'theDouble',
        /// optionally stripping the trailing '.00' sub-string on whole numbers.
        /// </summary>
        /// <param name="theDouble"></param>
        /// <param name="stripTrailing00"></param>
        ///-------------------------------------------------------------------
        public static string ToCurrency( double? theDouble, bool stripTrailing00 = true )
        {
            if (theDouble != null)
            {
                if (stripTrailing00)
                {
                    if (theDouble == Math.Floor( (double)theDouble ))
                    {
                        return ToThousandSep( (double)theDouble, 0 );
                    }
                }
            }

            return (theDouble != null) ?
                       ((double)theDouble).ToString( "F2" ) :
                       string.Empty;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns a string representation of 'theDouble' from 0.0 to 100.0 with
        /// a self-varying number of decimal places.  If less than 0.01,
        /// '0' is returned.  If greater than or equal to 100.0, '100' is returned.
        /// Values less than 10.0 are returned with two decimal places, and values
        /// greater than or equal to 10.0 are returned with one decimal place.  All
        /// values return 3 or fewer total digits, providing consistent length
        /// strings for better formatting.
        /// </summary>
        /// <param name="theDouble"></param>
        ///-------------------------------------------------------------------
        public static string ToPercentage( double? theDouble )
        {
            if (theDouble == null)
            {
                return string.Empty;
            }

            double theDoubleNotNull = ((theDouble != null) ? (double)theDouble : 0.0);

            if (theDoubleNotNull < 0.01)
            {
                return "0";
            }
            else
            if (theDoubleNotNull >= 100.0)
            {
                return "100";
            }

            return theDoubleNotNull.ToString( "F" + ((theDoubleNotNull < 10.0) ? "2" : "1" ) );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// ToDoubleDef
        /// </summary>
        /// <param name="theDouble"></param>
        /// <param name="theDoubleDefault"></param>
        ///-------------------------------------------------------------------
        public static double ToDoubleDef( double? theDouble, double theDoubleDefault = 0.0 )
        {
            return (theDouble != null) ? (double)theDouble : theDoubleDefault;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns a string that is leading space padded to 'theWidth' digits.
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="theWidth"></param>
        ///-------------------------------------------------------------------
        public static string LSPad( string theString, int theWidth = 2 )
        {
            string lsPadStr = theString;

            while (lsPadStr.Length < theWidth)
            {
                lsPadStr = " " + lsPadStr;
            }

            return lsPadStr;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns a string that is leading space padded to 'theWidth' digits.
        /// </summary>
        /// <param name="theInt"></param>
        /// <param name="theWidth"></param>
        ///-------------------------------------------------------------------
        public static string LSPad( int theInt, int theWidth = 2 )
        {
            return LSPad( theInt.ToString(), theWidth );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns a string that is leading space padded to 'theWidth' digits.
        /// </summary>
        /// <param name="theUInt"></param>
        /// <param name="theWidth"></param>
        ///-------------------------------------------------------------------
        public static string LSPad( uint theUInt, int theWidth = 2 )
        {
            return LSPad( theUInt.ToString(), theWidth );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns a string that is leading space padded to 'theWidth' digits.
        /// </summary>
        /// <param name="theLong"></param>
        /// <param name="theWidth"></param>
        ///-------------------------------------------------------------------
        public static string LSPad( long theLong, int theWidth = 2 )
        {
            return LSPad( theLong.ToString(), theWidth );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns a string that is leading space padded to 'theWidth' digits.
        /// </summary>
        /// <param name="theULong"></param>
        /// <param name="theWidth"></param>
        ///-------------------------------------------------------------------
        public static string LSPad( ulong theULong, int theWidth = 2 )
        {
            return LSPad( theULong.ToString(), theWidth );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns a string that is leading space padded to 'theWidth' digits.
        /// </summary>
        /// <param name="theDoub"></param>
        /// <param name="theWidth"></param>
        ///-------------------------------------------------------------------
        public static string LSPad( double theDoub, int theWidth = 2 )
        {
            return LSPad( theDoub.ToString(), theWidth );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns a string that is leading zero padded to 'theWidth' digits.
        /// </summary>
        /// <param name="theInt"></param>
        /// <param name="theWidth"></param>
        ///-------------------------------------------------------------------
        public static string LZPad( int theInt, int theWidth = 2 )
        {
            string lzPadStr = theInt.ToString();

            while (lzPadStr.Length < theWidth)
            {
                lzPadStr = "0" + lzPadStr;
            }

            return lzPadStr;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Given a number string with a decimal point, returns a string
        /// that has any/all trailing zeroes removed.  If the resulting string
        /// ends in a decimal point, it is removed as well.
        /// </summary>
        /// <param name="theDecStr"></param>
        ///-------------------------------------------------------------------
        public static string StripTZ(string theDecStr)
        {
            string tzStripStr = theDecStr;

            if (tzStripStr.IndexOf(".") > 0)
            {
                int tzStripStrLen = tzStripStr.Length;

                while ((tzStripStrLen >= 1) && (tzStripStr[tzStripStrLen - 1] == '0'))
                {                    
                    tzStripStrLen--;
                }

                if ((tzStripStrLen >= 1) && (tzStripStr[tzStripStrLen - 1] == '.'))
                {
                    tzStripStrLen--;
                }                    

                tzStripStr = tzStripStr.Substring(0, tzStripStrLen);
            }

            return tzStripStr;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Round
        /// </summary>
        /// <param name="theDouble"></param>
        ///-------------------------------------------------------------------
        public static int Round( double? theDouble )
        {
            return (theDouble != null) ? (int)(theDouble + 0.5) : 0;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Return '+' or '-' for positive and negative numbers, respectively.
        /// Return an empty string for 0.0
        /// </summary>
        /// <param name="theDouble"></param>
        ///-------------------------------------------------------------------
        public static string Sign( double? theDouble )
        {
            if ((theDouble == null) || (theDouble == 0.0))
            {
                return string.Empty;
            }

            if (theDouble > 0.0)
            {
                return "+";
            }

            return "-";
        }

        //-------------------------------------------------------------------
        public static int InvertSign( int theInt )
        {
            return theInt * -1;
        }

        //-------------------------------------------------------------------
        public static double InvertSign( double theDoub )
        {
            return theDoub * -1.0;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Converts 'theObject' to a byte value.
        /// </summary>
        /// <param name="theObject"></param>
        ///-------------------------------------------------------------------
        public static byte ObjectToByte( object theObject )
        {
            return (byte)(int)theObject;
        }

        //-------------------------------------------------------------------
        public static int Half( int theInt )
        {
            return theInt / 2;
        }

        //-------------------------------------------------------------------
        public static int Third( int theInt )
        {
            return theInt / 3;
        }

        //-------------------------------------------------------------------
        public static int Quarter( int theInt )
        {
            return theInt / 4;
        }

        //-------------------------------------------------------------------
        public static int Fifth( int theInt )
        {
            return theInt / 5;
        }

        //-------------------------------------------------------------------
        public static int Sixth( int theInt )
        {
            return theInt / 6;
        }

        //-------------------------------------------------------------------
        public static int Eighth( int theInt )
        {
            return theInt / 8;
        }

        //-------------------------------------------------------------------
        public static int Tenth( int theInt )
        {
            return theInt / 10;
        }

        //-------------------------------------------------------------------
        public static int Twice( int theInt )
        {
            return theInt * 2;
        }

        //-------------------------------------------------------------------
        public static int Square( int theInt )
        {
            return (int)Math.Pow( theInt, 2 );
        }

        //-------------------------------------------------------------------
        public static int Cube( int theInt )
        {
            return (int)Math.Pow( theInt, 3 );
        }

        //-------------------------------------------------------------------
        public static double Half( double theDoub )
        {
            return theDoub / 2.0;
        }

        //-------------------------------------------------------------------
        public static double Third( double theDoub )
        {
            return theDoub / 3.0;
        }

        //-------------------------------------------------------------------
        public static double Quarter( double theDoub )
        {
            return theDoub / 4.0;
        }

        //-------------------------------------------------------------------
        public static double Fifth( double theDoub )
        {
            return theDoub / 5.0;
        }

        //-------------------------------------------------------------------
        public static double Sixth( double theDoub )
        {
            return theDoub / 6.0;
        }

        //-------------------------------------------------------------------
        public static double Eighth( double theDoub )
        {
            return theDoub / 8.0;
        }

        //-------------------------------------------------------------------
        public static double Tenth( double theDoub )
        {
            return theDoub / 10.0;
        }

        //-------------------------------------------------------------------
        public static double Twice( double theDoub )
        {
            return theDoub * 2.0;
        }

        //-------------------------------------------------------------------
        public static double Square( double theDoub )
        {
            return Math.Pow( theDoub, 2 );
        }

        //-------------------------------------------------------------------
        public static double Cube( double theDoub )
        {
            return Math.Pow( theDoub, 3 );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns true if the parity of 'theInt' is odd.
        /// </summary>
        /// <param name="theInt"></param>
        /// <remarks>
        /// Zero is defined as an even number.
        /// </remarks>
        ///-------------------------------------------------------------------
        public static bool IsOdd(int theInt)
        {
            return (theInt % 2) == 1;
        }

        //-------------------------------------------------------------------
        public static bool IsOdd(uint theUInt)
        {
            return (theUInt % 2) == 1;
        }

        //-------------------------------------------------------------------
        public static bool IsEven(int theInt)
        {
            return ! IsOdd(theInt);
        }

        //-------------------------------------------------------------------
        public static bool IsEven(uint theUInt)
        {
            return ! IsOdd(theUInt);
        }

        //-------------------------------------------------------------------
        public static bool IsZero( int theInt )
        {
            return theInt == 0;
        }

        //-------------------------------------------------------------------
        public static bool IsZero( long theLong )
        {
            return theLong == 0L;
        }

        //-------------------------------------------------------------------
        public static bool IsZero( double theDoub )
        {
            return theDoub == 0.0;
        }

        // Zero is also known as the 'origin'
        //
        //-------------------------------------------------------------------
        public static bool IsOrigin( int theInt )
        {
            return IsZero( theInt );
        }

        //-------------------------------------------------------------------
        public static bool IsOrigin( long theLong )
        {
            return IsZero( theLong );
        }

        //-------------------------------------------------------------------
        public static bool IsOrigin( double theDoub )
        {
            return IsZero( theDoub );
        }

        // Zero is neither positive or negative
        //
        //-------------------------------------------------------------------
        public static bool IsPositive( int theInt, bool includeZero = false )
        {
            return includeZero ? theInt >= 0 : theInt > 0;
        }

        //-------------------------------------------------------------------
        public static bool IsPositive( long theLong, bool includeZero = false )
        {
            return includeZero ? theLong >= 0L : theLong > 0L;
        }

        //-------------------------------------------------------------------
        public static bool IsPositive( double theDoub, bool includeZero = false )
        {
            return includeZero ? theDoub >= 0.0 : theDoub > 0.0;
        }

        //-------------------------------------------------------------------
        public static bool IsNegative( int theInt )
        {
            return theInt < 0;
        }

        //-------------------------------------------------------------------
        public static bool IsNegative( long theLong )
        {
            return theLong < 0L;
        }

        //-------------------------------------------------------------------
        public static bool IsNegative( double theDoub )
        {
            return theDoub < 0.0;
        }

        //-------------------------------------------------------------------
        public static bool IsWholeNumber( double theDoub )
        {
            return (theDoub - (long)theDoub) == 0.0;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns 'theInt' with the ordinal suffix appended.
        /// All numbers 'mod 100' are supported, and all others simply return
        /// the number string itself.
        /// </summary>
        /// <param name="theInt"></param>
        /// <seealso cref="ToOrdinalSSStr(int)"/>
        ///-------------------------------------------------------------------
        public static string ToOrdinalStr( int theInt )
        {
            string[] ordinalArr = new string[ 100 +1 ]
            {
                "th",

                "st", "nd", "rd", "th", "th",
                "th", "th", "th", "th", "th",

	            "th", "th", "th", "th", "th",
                "th", "th", "th", "th", "th",

                "st", "nd", "rd", "th", "th",
                "th", "th", "th", "th", "th",

                "st", "nd", "rd", "th", "th",
                "th", "th", "th", "th", "th",

                "st", "nd", "rd", "th", "th",
                "th", "th", "th", "th", "th",

                "st", "nd", "rd", "th", "th",
                "th", "th", "th", "th", "th",

                "st", "nd", "rd", "th", "th",
                "th", "th", "th", "th", "th",

                "st", "nd", "rd", "th", "th",
                "th", "th", "th", "th", "th",

                "st", "nd", "rd", "th", "th",
                "th", "th", "th", "th", "th",

                "st", "nd", "rd", "th", "th",
                "th", "th", "th", "th", "th"
            };

            int modInt = Math.Abs( theInt ) % 100;

            modInt = (modInt == 0) ? 100 : modInt;

            return theInt.ToString() + ordinalArr[ modInt ];
        }

        //-------------------------------------------------------------------
        public static string ToOrdinalStr( uint theUInt )
        {
            return ToOrdinalStr( (int)theUInt );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns 'theInt' with the ordinal suffix appended in super-script
        /// characters.
        /// All numbers 'mod 100' are supported, and all others simply return
        /// the number string itself.
        /// </summary>
        /// <param name="theInt"></param>
        /// <seealso cref="ToOrdinalStr(int)"/>
        ///-------------------------------------------------------------------
        public static string ToOrdinalSSStr( int theInt )
        {
            string[] ordinalSSArr = new string[ 100 +1 ]
            {
                "ᵗʰ",

                "ˢᵗ", "ⁿᵈ", "ʳᵈ", "ᵗʰ", "ᵗʰ",
                "ᵗʰ", "ᵗʰ", "ᵗʰ", "ᵗʰ", "ᵗʰ",

	            "ᵗʰ", "ᵗʰ", "ᵗʰ", "ᵗʰ", "ᵗʰ",
                "ᵗʰ", "ᵗʰ", "ᵗʰ", "ᵗʰ", "ᵗʰ",

                "ˢᵗ", "ⁿᵈ", "ʳᵈ", "ᵗʰ", "ᵗʰ",
                "ᵗʰ", "ᵗʰ", "ᵗʰ", "ᵗʰ", "ᵗʰ",

                "ˢᵗ", "ⁿᵈ", "ʳᵈ", "ᵗʰ", "ᵗʰ",
                "ᵗʰ", "ᵗʰ", "ᵗʰ", "ᵗʰ", "ᵗʰ",

                "ˢᵗ", "ⁿᵈ", "ʳᵈ", "ᵗʰ", "ᵗʰ",
                "ᵗʰ", "ᵗʰ", "ᵗʰ", "ᵗʰ", "ᵗʰ",

                "ˢᵗ", "ⁿᵈ", "ʳᵈ", "ᵗʰ", "ᵗʰ",
                "ᵗʰ", "ᵗʰ", "ᵗʰ", "ᵗʰ", "ᵗʰ",

                "ˢᵗ", "ⁿᵈ", "ʳᵈ", "ᵗʰ", "ᵗʰ",
                "ᵗʰ", "ᵗʰ", "ᵗʰ", "ᵗʰ", "ᵗʰ",

                "ˢᵗ", "ⁿᵈ", "ʳᵈ", "ᵗʰ", "ᵗʰ",
                "ᵗʰ", "ᵗʰ", "ᵗʰ", "ᵗʰ", "ᵗʰ",

                "ˢᵗ", "ⁿᵈ", "ʳᵈ", "ᵗʰ", "ᵗʰ",
                "ᵗʰ", "ᵗʰ", "ᵗʰ", "ᵗʰ", "ᵗʰ",

                "ˢᵗ", "ⁿᵈ", "ʳᵈ", "ᵗʰ", "ᵗʰ",
                "ᵗʰ", "ᵗʰ", "ᵗʰ", "ᵗʰ", "ᵗʰ"
            };

            int modInt = Math.Abs( theInt ) % 100;

            modInt = (modInt == 0) ? 100 : modInt;

            return theInt.ToString() + ordinalSSArr[ modInt ];
        }

        //-------------------------------------------------------------------
        public static string ToOrdinalSSStr( uint theUInt )
        {
            return ToOrdinalSSStr( (int)theUInt );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns the ordinal word representing 'theInt'
        /// The numbers 0 to 31 are supported, and all others simply return
        /// the number string itself.
        /// </summary>
        /// <param name="theInt"></param>
        ///-------------------------------------------------------------------
        public static string ToOrdinalWord( int theInt )
        {
            string[] ordinalArr = new string[ 31 +1 ]
            {
                "Zeroth",

                "First", "Second", "Third", "Fourth", "Fifth",
                "Sixth", "Seventh", "Eighth", "Nineth", "Tenth",

	            "Eleventh", "Twelveth", "Thirteenth", "Fourteenth", "Fifteenth",
                "Sixteenth", "Seventeenth", "Eighteenth", "Nineteenth", "Twentieth",

                "Twenty First", "Twenty Second", "Twenty Third", "Twenty Fourth", "Twenty Fifth",
                "Twenty Sixth", "Twenty Seventh", "Twenty Eighth", "Twenty Nineth", "Thirtieth",

	            "Thirty First"
            };

            if ((0 <= theInt) && (theInt <= 31))
            {
                return ordinalArr[ theInt ];
            }

            return theInt.ToString();
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns the cardinal word representing 'theInt'
        /// The numbers 0 to 31 are supported, and all others simply return
        /// the number string itself.
        /// </summary>
        /// <param name="theInt"></param>
        ///-------------------------------------------------------------------
        public static string ToCardinalStr( int theInt )
        {
            string[] cardinalArr = new string[ 31 +1 ]
            {
                "Zero",

                "One", "Two", "Three", "Four", "Five",
                "Six", "Seven", "Eight", "Nine", "Ten",

	            "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen",
                "Sixteen", "Seventeen", "Eighteen", "Nineteen", "Twenty",

                "Twenty One", "Twenty Two", "Twenty Three", "Twenty Four", "Twenty Five",
                "Twenty Six", "Twenty Seven", "Twenty Eight", "Twenty Nine", "Thirty",

	            "Thirty One"
            };

            if ((0 <= theInt) && (theInt <= 31))
            {
                return cardinalArr[ theInt ];
            }

            return theInt.ToString();
        }
		
        //-------------------------------------------------------------------
		public static string ToScratchOffStr( byte theByte, bool toUpper = true )
		{
            string[] scratchOffArr = new string[ 40 +1 ]
            {
                "Zero",

                "One",   "Two",   "Three", "Four",  "Five",
                "Six",   "Seven", "Eight", "Nine",  "Ten",

	            "Elvn",  "Tlve",  "Thrtn", "Frtn",  "Fiftn",
                "Sxtn",  "Svtn",  "Eghtn", "Nntn",  "Twenty",

                "Ttyon", "Ttytw", "Ttytr", "Ttyfr", "Ttyfv",
                "Ttysx", "Ttysv", "Ttyeg", "Ttyni", "Thrty",

                "Thron", "Thrtw", "Thrtr", "Thrfr", "Thrfv",
                "Thrsx", "Thrsv", "Threg", "Thrni", "Forty",
            };

            if ((0 <= theByte) && (theByte <= 40))
            {
				if (toUpper)
				{
					return scratchOffArr[ theByte ].ToUpper();
				}
				
                return scratchOffArr[ theByte ];
            }

            return theByte.ToString();
		}

        //-------------------------------------------------------------------
		public enum ScratchOffBouns
		{
		    Shoe      = 1,
			Cookie    = 2,
			PotOfGold = 3,
			Bar       = 4,
			Cherry    = 5
		}
		
        //-------------------------------------------------------------------
		public static string ToScratchOffBonusStr( byte theByte )
		{
            string[] scratchOffBonusArr = new string[ 5 +1 ]
            {
                "",

                "Shoe", "Cookie", "PotGld", "Bar", "Chry"
            };

            if ((0 <= theByte) && (theByte <= 5))
            {
                return scratchOffBonusArr[ theByte ];
            }

            return theByte.ToString();
		}

        //-------------------------------------------------------------------
        public static string ToStringSign( int theInt )
        {
            return ((theInt > 0) ? "+" : string.Empty) + theInt.ToString();
        }

        //-------------------------------------------------------------------
        public static void Swap<T>(ref T firstNumber, ref T secondNumber)
        {
            T tempNumber = firstNumber;

            firstNumber = secondNumber;

            secondNumber = tempNumber;
        }

        //-------------------------------------------------------------------
        public static string ToStringNone( int theInt )
        {
            return (theInt != 0) ? theInt.ToString() : "None";
        }

        //-------------------------------------------------------------------
        public static string ToNumTimes( ulong theULong )
        {
            switch (theULong)
            {
                case 0UL: return "Never";
                case 1UL: return "Once";
                case 2UL: return "Twice";
            }
            
            return Math.Abs( (long)theULong ) + " times";
        }

        //-------------------------------------------------------------------
        public static string ToNumTimes( int theInt )
        {
            return ToNumTimes( (ulong)theInt );
        }

        //-------------------------------------------------------------------
        public static bool InRange( int theInt, int startOfRange, int endOfRange )
        {
            return (startOfRange <= theInt) && (theInt <= endOfRange);
        }

        //-------------------------------------------------------------------
        public static bool OutOfRange( int theInt, int startOfRange, int endOfRange )
        {
            return ! InRange( theInt, startOfRange, endOfRange );
        }

        //-------------------------------------------------------------------
        public static bool InRange( double theDoub, double startOfRange, double endOfRange )
        {
            return (startOfRange <= theDoub) && (theDoub <= endOfRange);
        }

        //-------------------------------------------------------------------
        public static bool OutOfRange( double theDoub, double startOfRange, double endOfRange )
        {
            return ! InRange( theDoub, startOfRange, endOfRange );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns the Fibonacci number in the 'nth' position, starting at
        /// '1' for the 'first' ordinal position, i.e one-based,
        /// not zero-based.
        /// </summary>
        /// <remarks>
        /// Sequence: 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, ...
        // </remarks>
        /// <param name="nthFib"></param>
        ///-------------------------------------------------------------------
        public static uint Fibonacci( uint nthFib )
        {
            if (nthFib == 0)
            {
                return 0;
            }

            if (nthFib < FibSeeds.Length)
            {
                return FibSeeds[ nthFib - 1 ];
            }

            return Fibonacci( nthFib - 1 ) + Fibonacci( nthFib - 2 );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns a boolean if the last (final) digit of 'theNum' is
        /// 'theFinalDigit'
        /// </summary>
        /// <param name="theNum"></param>
        /// <param name="theFinalDigit"></param>
        /// <returns>
        /// true if the last digit is 'theFinalDigit', false if not.
        /// </returns>
        ///-------------------------------------------------------------------
        public static bool IsFinal( long theNum, byte theFinalDigit )
        {
            if (theFinalDigit >= 10)
            {
                return false;
            }

            return Math.Abs( theNum % 10 ) == theFinalDigit;
        }

        //-------------------------------------------------------------------
        public static int SumOfRange( int startNum, int endNum, int stepNum = 1 )
        {
            int retSum = 0;

            foreach (int i in startNum.To(endNum).Step(stepNum))
            {
                retSum += i;
            }

            return retSum;
        }

        //-------------------------------------------------------------------
        public static uint SumOfRange( uint startNum, uint endNum, int stepNum = 1 )
        {
            uint retSum = 0U;

            foreach (uint u in startNum.To(endNum).Step(stepNum))
            {
                retSum += u;
            }

            return retSum;
        }

        // Extension methods
        //

        ///-------------------------------------------------------------------
        /// <summary>
        /// Creates an iterator 'from' the given value, up 'to' and
        /// including the 'toVal' value.  The values may be positive/increasing
        /// or negative/decreasing for 'counting up' or 'counting down'.
        /// </summary>
        /// <param name="fromVal"></param>
        /// <param name="toVal"></param>
        /// <example>
        /// foreach (int i in 1.To(10)) Console.WriteLine(i);
        /// </example>
        /// <returns>
        /// An iterator within the specified range
        /// </returns>
        ///-------------------------------------------------------------------
        public static IEnumerable<int> To( this int fromVal, int toVal )
        {
            if (fromVal < toVal)
            {
                while (fromVal <= toVal)
                {
                    yield return fromVal++;
                }
            }
            else
            {
                while (fromVal >= toVal)
                {
                    yield return fromVal--;
                }
            }
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Used with <see cref="To"/>, allows incrementing or decrementing
        /// by values other than '1'.
        /// </summary>
        /// <param name="toVal"></param>
        /// <param name="stepVal"></param>
        /// <example>
        /// foreach (int i in 1.To(10).Step(2)) Console.WriteLine(i);
        /// </example>
        /// <returns>
        /// An iterator within the specified range
        /// </returns>
        ///-------------------------------------------------------------------
        public static IEnumerable<int> Step( this IEnumerable<int> toVal, int stepVal )
        {
            if (stepVal == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(stepVal), "Cannot be zero");
            }

            return toVal.Where( (x, i) => (i % stepVal) == 0 );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Creates an iterator 'from' the given value, up 'to' and
        /// including the 'toVal' value.  The values may be positive/increasing
        /// or negative/decreasing for 'counting up' or 'counting down'.
        /// </summary>
        /// <param name="fromVal"></param>
        /// <param name="toVal"></param>
        /// <example>
        /// foreach (uint u in 1.To(10)) Console.WriteLine(u);
        /// </example>
        /// <returns>
        /// An iterator within the specified range
        /// </returns>
        ///-------------------------------------------------------------------
        public static IEnumerable<uint> To( this uint fromVal, uint toVal )
        {
            if (fromVal < toVal)
            {
                while (fromVal <= toVal)
                {
                    yield return fromVal++;
                }
            }
            else
            {
                while (fromVal >= toVal)
                {
                    yield return fromVal--;
                }
            }
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Used with <see cref="To"/>, allows incrementing or decrementing
        /// by values other than '1'.
        /// </summary>
        /// <param name="toVal"></param>
        /// <param name="stepVal"></param>
        /// <example>
        /// foreach (uint u in 1.To(10).Step(2)) Console.WriteLine(u);
        /// </example>
        /// <returns>
        /// An iterator within the specified range
        /// </returns>
        ///-------------------------------------------------------------------
        public static IEnumerable<uint> Step( this IEnumerable<uint> toVal, int stepVal )
        {
            if (stepVal == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(stepVal), "Cannot be zero");
            }

            return toVal.Where( (x, u) => (u % stepVal) == 0U );
        }

        // Salary to/from Hourly Wage
        // 40 hours/week * 50 work weeks/year = 2,000
        //
        //-------------------------------------------------------------------
        public static double HourlyWageToSalary( double hourlyWage )
        {
            if (hourlyWage <= 0.0)
            {
                return 0.0;
            }

            return hourlyWage * 2000.0;
        }

        //-------------------------------------------------------------------
        public static double SalaryToHourlyWage( double annualSalary )
        {
            if (annualSalary <= 0.0)
            {
                return 0.0;
            }

            return annualSalary / 2000.0;
        }

        #endregion Class methods

    } // class - PBFNumber

} // namespace
