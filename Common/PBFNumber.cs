//---------------------------------------------------------------------------
//
// Copyright © 2015-2024 Paragon Bit Foundry.  All Rights Reserved.
//
//---------------------------------------------------------------------------
using System;

namespace PBFCommon
{
    public static class PBFNumber
    {
        #region Class methods

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns an 'int' represented by <paramref name="theIntString"/>,
        /// or <paramref name="theDefaultInt"/> if the string is empty or
        /// not-a-number (NaN).
        /// </summary>
        /// <param name="theIntString"></param>
        /// <param name="theDefaultInt"></param>
        ///-------------------------------------------------------------------
        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns true if the parity of <paramref name="theInt"/> is odd.
        /// </summary>
        /// <param name="theInt"></param>
        /// <remarks>
        /// Inverse of <seealso cref="IsEven(int)"/>
        /// </remarks>
        ///-------------------------------------------------------------------
        public static bool IsOdd(int theInt)
        {
            return (theInt % 2) == 1;
        }

        //-------------------------------------------------------------------
        public static bool IsEven(int theInt)
        {
            return ! IsOdd(theInt);
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Returns true if the parity of <paramref name="theInt"/> is even.
        /// </summary>
        /// <param name="theInt"></param>
        /// <remarks>
        /// Inverse of <seealso cref="IsOdd(int)"/>
        /// Zero is defined as an even number.
        /// </remarks>
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
        /// Returns <paramref name="theInt"/> with the ordinal suffix appended
        /// in super-script characters.
        /// All numbers 'mod 100' are supported, and all others simply return
        /// the number string itself.
        /// </summary>
        /// <param name="theInt"></param>
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

        #endregion

    } // class - PBFNumber

} // namespace
