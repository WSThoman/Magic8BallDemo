//---------------------------------------------------------------------------
//
// Copyright © 2016-2024 Paragon Bit Foundry.  All Rights Reserved.
//
//---------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Reflection;

namespace PBFCommon
{
    public sealed class PBFFileVersionInfo
    {
        #region Data members

        private static Version mFileVersion = null;

        #endregion

        #region Properties

        public static FileVersionInfo EXEsFVI { get; private set; } = null;

        public static string LastError { get; private set; } = string.Empty;

        public static Assembly EXEAssembly { get; private set; } = null;

        public static Version FileVersion
        {
            get 
            {
                if (EXEsFVI == null)
                {
                    return null;
                }

                if (mFileVersion == null)
                { 
                    mFileVersion = new Version( EXEsFVI.FileMajorPart,
                                                EXEsFVI.FileMinorPart,
                                                EXEsFVI.FileBuildPart,
                                                EXEsFVI.FilePrivatePart );
                }

                return mFileVersion;
            }
        }

        public static string FileVersionStr
        {
            get { return (EXEsFVI != null) ? FileVersionString( true ) : string.Empty; }
        }

        public static int VersionMajor
        {
            get { return (EXEsFVI != null) ? EXEsFVI.FileMajorPart : 0; }
        }
          
        public static int VersionMinor
        {
            get { return (EXEsFVI != null) ? EXEsFVI.FileMinorPart : 0; }
        }
          
        public static int VersionBuild
        {
            get { return (EXEsFVI != null) ? EXEsFVI.FileBuildPart : 0; }
        }
          
        public static int VersionPrivate
        {
            get { return (EXEsFVI != null) ? EXEsFVI.FilePrivatePart : 0; }
        }

        public static string Description { get; private set; } = string.Empty;

        public static string Company
        {
            get { return (EXEsFVI != null) ? EXEsFVI.CompanyName: string.Empty; }
        }

        public static string Product
        {
            get { return (EXEsFVI != null) ? EXEsFVI.ProductName: string.Empty; }
        }

        public static string Copyright
        {
            get { return (EXEsFVI != null) ? EXEsFVI.LegalCopyright: string.Empty; }
        }

        public static string Trademarks
        {
            get { return (EXEsFVI != null) ? EXEsFVI.LegalTrademarks: string.Empty; }
        }

        #endregion

        #region Constructors

        //-------------------------------------------------------------------
        static PBFFileVersionInfo()
        {
            ClearLastError();

            GetEXEAssembly();

            GetEXEVersion();

            GetEXEAssemblyDescription();

        } // Constructor - default

        #endregion

        #region Class methods

        ///-------------------------------------------------------------------
        /// <summary>
        /// ClearLastError
        /// </summary>
        ///-------------------------------------------------------------------
        private static void ClearLastError()
        {
            LastError = string.Empty;
		}

        ///-------------------------------------------------------------------
        /// <summary>
        /// GetEXEAssembly
        /// </summary>
        ///-------------------------------------------------------------------
        private static void GetEXEAssembly()
        {
            EXEAssembly = Assembly.GetExecutingAssembly();
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// GetEXEVersion
        /// </summary>
        ///-------------------------------------------------------------------
        private static FileVersionInfo GetEXEVersion()
        {
            try
            {            
                EXEsFVI = FileVersionInfo.GetVersionInfo( EXEAssembly?.Location );
            }
            catch (Exception e)
            {
                LastError = e.Message;
            }

            return EXEsFVI;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// FileVersionString
        /// </summary>
        /// <param name="shortString"></param>
        ///-------------------------------------------------------------------
        public static string FileVersionString( bool shortString = true )
        {
            if (EXEsFVI == null)
            {
                GetEXEVersion();			
            }

            // If the FileVersionInfo is still null, return an empty string
            //
            if (EXEsFVI == null)
            {
                return string.Empty;
            }

            // Version <Major>.<Minor>.<Build>.<Private>
            //
            return VersionString( FileVersion, shortString );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// VersionDate
        /// </summary>
        /// <param name="theVersion"></param>
        /// <remarks>
        /// Returns a DateTime of 'theVersion'
        /// </remarks>
        ///-------------------------------------------------------------------
        public static DateTime VersionDate( Version theVersion )
        {
            return new DateTime( theVersion.Major, theVersion.Minor, theVersion.Build );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// VersionDateString
        /// </summary>
        /// <param name="theVersion"></param>
        /// <remarks>
        /// Formats a 'Version' object in the format 'ccyy.m.d' to 'm/d/ccyy'.
        /// </remarks>
        ///-------------------------------------------------------------------
        public static string VersionDateString( Version theVersion )
        {
            return theVersion.Minor.ToString() + "/" +
                   theVersion.Build.ToString() + "/" +
                   theVersion.Major.ToString();
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// VersionDateStringShort
        /// </summary>
        /// <param name="theVersion"></param>
        ///-------------------------------------------------------------------
        public static string VersionDateStringShort( Version theVersion )
        {
            DateTime dtLong =
                new DateTime( theVersion.Major, theVersion.Minor, theVersion.Build );

            string retStr =
                dtLong.ToString( "ddd MMM" ) + " " + PBFNumber.ToOrdinalSSStr( dtLong.Day );

            if (DateTime.Now.Year != dtLong.Year)
            {
                retStr += ", " + dtLong.ToString( "yyyy" );
            }

            return retStr;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// VersionDateStringLong
        /// </summary>
        /// <param name="theVersion"></param>
        ///-------------------------------------------------------------------
        public static string VersionDateStringLong( Version theVersion )
        {
            DateTime dtLong =
                new DateTime( theVersion.Major, theVersion.Minor, theVersion.Build );

            string retStr =
                dtLong.ToString( "MMMM" ) + " " + PBFNumber.ToOrdinalSSStr( dtLong.Day );

            if (DateTime.Now.Year != dtLong.Year)
            {
                retStr += ", " + dtLong.ToString( "yyyy" );
            }

            return retStr;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// VersionString
        /// </summary>
        /// <param name="theVersion"></param>
        /// <param name="shortString"></param>
        ///-------------------------------------------------------------------
        public static string VersionString( Version theVersion, bool shortString = true )
        {
            // Version <Major>.<Minor>.<Build>.<Private>
            //
            string verString =
                theVersion.Major.ToString() + "." +
                theVersion.Minor.ToString();

            if ((theVersion.Build > 0) || ( ! shortString))
            {
                verString += "." + theVersion.Build.ToString();
            }

            if ((theVersion.Revision > 0) || ( ! shortString))
            {
                verString += "." + theVersion.Revision.ToString();
            }

            return verString;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// GetEXEAssemblyDescription
        /// </summary>
        ///-------------------------------------------------------------------
        private static void GetEXEAssemblyDescription()
        {
            Description = string.Empty;

            Type asmDescType = typeof(AssemblyDescriptionAttribute);

            if (Attribute.IsDefined( EXEAssembly, asmDescType ))
            {
                AssemblyDescriptionAttribute asmDescAttr =
                    (AssemblyDescriptionAttribute)Attribute.
                        GetCustomAttribute( EXEAssembly, asmDescType );

                if (asmDescAttr != null)
                {
                    Description = asmDescAttr.Description;
                }
            }
        }

        #endregion

    } // class - PBFFileVersionInfo

} // namespace
