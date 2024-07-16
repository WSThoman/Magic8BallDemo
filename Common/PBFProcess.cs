//---------------------------------------------------------------------------
//
// Copyright © 2016-2024 Paragon Bit Foundry.  All Rights Reserved.
//
//---------------------------------------------------------------------------
using System.Diagnostics;

namespace PBFCommon
{
    public sealed class PBFProcess
    {
        // Constructors
        //
        #region Constructors

        //-------------------------------------------------------------------
        private PBFProcess()
        {
        } // Constructor - singleton

        #endregion Constructors

        // Class methods
        //
        #region Class methods

        ///-------------------------------------------------------------------
        /// <summary>
        /// Sets the CPU Priority of 'theProcess' to 'theNewPriority'.
        /// </summary>
        /// <param name="theProcess"></param>
        /// <param name="theNewPriority"></param>
        /// <remarks>If 'theProcess' is null, the current process is used.</remarks>
        /// <returns>Returns 'false' on success, 'true' on error</returns>
        ///-------------------------------------------------------------------
        public static bool SetCPUPriority(
            Process theProcess = null,
            ProcessPriorityClass theNewPriority = ProcessPriorityClass.Normal )
        {
            Process setProcess = theProcess ?? Process.GetCurrentProcess();

            try
            {
                setProcess.PriorityClass = theNewPriority;
            }
            catch
            {
                return true;
            }

            return false;
        }

        //-------------------------------------------------------------------
        public static bool SetCPUPriorityToLow( Process theProcess = null )
        {
            return SetCPUPriority( theProcess, ProcessPriorityClass.Idle );
        }

        //-------------------------------------------------------------------
        public static bool SetCPUPriorityToNormal( Process theProcess = null )
        {
            return SetCPUPriority( theProcess, ProcessPriorityClass.Normal );
        }

        //-------------------------------------------------------------------
        public static bool SetCPUPriorityToHigh( Process theProcess = null )
        {
            return SetCPUPriority( theProcess, ProcessPriorityClass.High );
        }

        //-------------------------------------------------------------------
        public static bool SetCPUPriorityToRealTime( Process theProcess = null )
        {
            return SetCPUPriority( theProcess, ProcessPriorityClass.RealTime );
        }

        //-------------------------------------------------------------------
        public static bool SetCPUPriorityToBelowNormal( Process theProcess = null )
        {
            return SetCPUPriority( theProcess, ProcessPriorityClass.BelowNormal );
        }

        //-------------------------------------------------------------------
        public static bool SetCPUPriorityToAboveNormal( Process theProcess = null )
        {
            return SetCPUPriority( theProcess, ProcessPriorityClass.AboveNormal );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Gets the CPU Priority of 'theProcess'
        /// </summary>
        /// <param name="theProcess"></param>
        /// <remarks>If 'theProcess' is null, the current process is used.</remarks>
        /// <returns>Returns 'ProcessPriorityClass' on success, null on error</returns>
        ///-------------------------------------------------------------------
        public static ProcessPriorityClass? GetCPUPriority( Process theProcess = null )
        {
            Process getProcess = theProcess ?? Process.GetCurrentProcess();

            try
            {
                return getProcess.PriorityClass;
            }
            catch
            {
            }

            return null;
        }

        //-------------------------------------------------------------------
        private static bool CPUPriorityIsCheck( Process theProcess = null,
                                                ProcessPriorityClass? thePPC = null )
        {
            ProcessPriorityClass? retGetCPUPriority = GetCPUPriority( theProcess );

            return (retGetCPUPriority != null) &&
                   (retGetCPUPriority == (ProcessPriorityClass)thePPC);
        }

        //-------------------------------------------------------------------
        public static bool CPUPriorityIsLow( Process theProcess = null )
        {
            return CPUPriorityIsCheck( theProcess, ProcessPriorityClass.Idle );
        }

        //-------------------------------------------------------------------
        public static bool CPUPriorityIsNormal( Process theProcess = null )
        {
            return CPUPriorityIsCheck( theProcess, ProcessPriorityClass.Normal );
        }

        //-------------------------------------------------------------------
        public static bool CPUPriorityIsHigh( Process theProcess = null )
        {
            return CPUPriorityIsCheck( theProcess, ProcessPriorityClass.High );
        }

        //-------------------------------------------------------------------
        public static bool CPUPriorityIsRealTime( Process theProcess = null )
        {
            return CPUPriorityIsCheck( theProcess, ProcessPriorityClass.RealTime );
        }

        //-------------------------------------------------------------------
        public static bool CPUPriorityIsBelowNormal( Process theProcess = null )
        {
            return CPUPriorityIsCheck( theProcess, ProcessPriorityClass.BelowNormal );
        }

        //-------------------------------------------------------------------
        public static bool CPUPriorityIsAboveNormal( Process theProcess = null )
        {
            return CPUPriorityIsCheck( theProcess, ProcessPriorityClass.AboveNormal );
        }

        #endregion Class methods

    } // class - PBFProcess

} // namespace
