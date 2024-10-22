﻿//---------------------------------------------------------------------------
//
// Copyright © 2017-2024 Paragon Bit Foundry.  All Rights Reserved.
//
//---------------------------------------------------------------------------
using System;
using System.Threading;
using System.Windows.Forms;

using PBFCommon;

namespace Magic8BallDemo
{
    public static class Program
    {
        public static AppCustomApplicationContext MainCustomAppContext { get; private set; } = null;

        ///-------------------------------------------------------------------
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        ///-------------------------------------------------------------------
        [STAThread]
        private static void Main()
        {
            // Check if the [Ctrl] key is down
            //
            if (PBFKeyboard.IsCtrlKeyDown)
            {
                return;
            }

            // Check for a single application instance
            //
            if (PBFSingleAppInstanceBase.IsAppInstanceRunning(
                    AppCustomApplicationContext.APP_NAME ))
            {
                return;
            }

            // Start the application's icon
            //
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            
            MainCustomAppContext = new AppCustomApplicationContext();

            MainCustomAppContext.Start();

            Application.Run( MainCustomAppContext );
        }

    } // class - Program


    //-------------------------------------------------------------------
    public sealed class AppCustomApplicationContext : ApplicationContext
    {
        #region Constants

        public const string APP_NAME = "Magic8BallDemo";

        public const string APP_TITLE = "Magic 8-Ball Demo";

        private const string APP_BALLOON_TIP_TITLE = "Magic 8-Ball Demo";

        public const string PBF_URL = "https://www.ParagonBitFoundry.com";

        // The number of milliseconds to delay while displaying the 'answer' icon and
        // before re-displaying the main application icon.
        //
        private const ushort ONE_SEC_IN_MSEC = 1000;

        private const ushort ASK_A_QUESTION_DELAY = ONE_SEC_IN_MSEC;

        private const ushort ASK_A_QUESTION_PERIOD = 0;

        private const ushort SHOW_BALLOON_TIP_TIMEOUT = ONE_SEC_IN_MSEC;

        // Menu item indexes
        //
        public enum AppMenuIndex : byte
        {
            AskAQuestion = 0,
            Sep1         = 1,
            Icon         = 2,
            Application  = 3,
            PBFWebsite   = 4,
            About        = 5,
            Sep2         = 6,
            Exit         = 7
        }

        #endregion

        #region PBF Website

        private static MenuItem miPBFWebsite = null;

        private struct PBFWebsiteMenuText
        {
            public const string Title = "ParagonBitFoundry.com";
        }

        #endregion

        #region Icon sub-menu

        private enum IconMenuIndex : byte
        {
            TaskbarSettings  = 0,
            OpenControlPanel = 1
        }

        private static MenuItem miIcon = null;

        private struct IconMenuText
        {
            public const string Title = "Icon";

            public const string TaskbarSettings  = "Taskbar Settings";
            public const string OpenControlPanel = "Open Control Panel";
        }

        #endregion

        #region Application sub-menu

        private enum ApplicationMenuIndex : byte
        {
            RunAtStartup = 0
        }

        private static MenuItem miApplication = null;

        private struct ApplicationMenuText
        {
            public const string Title = "Application";

            public const string RunAtStartup = "Run at Startup";
        }

        #endregion

        #region Data members

        // Application's Notification Area icon
        //
        private NotifyIcon mAppNotifyIcon = null;

        // Magic 8-Ball
        //
        private readonly PBFMagic8Ball mMagic8Ball = new PBFMagic8Ball();

        // 'Ask a Question' timer
        //
        private static System.Threading.Timer mAskAQuestionTimer = null;

        // 'Ask a Question' coin
        //
        private readonly PBFCoin mAskAQuestionCoin = new PBFCoin();

        #endregion

        #region Class methods

        ///-------------------------------------------------------------------
        /// <summary>
        /// Starts the application
        /// </summary>
        /// <remarks>
        /// Called from within <seealso cref="Program.Main"/>
        ///</remarks>
        ///-------------------------------------------------------------------
        public void Start()
        {
            #region Icon sub-menu

            miIcon = new MenuItem( IconMenuText.Title );

            miIcon.MenuItems.Add( IconMenuText.TaskbarSettings,  MIEventIconTaskbarSettings );
            miIcon.MenuItems.Add( IconMenuText.OpenControlPanel, MIEventIconOpenControlPanel );

            #endregion

            #region Application sub-menu

            miApplication = new MenuItem( ApplicationMenuText.Title );

            miApplication.MenuItems.Add( ApplicationMenuText.RunAtStartup,
                                         MIEventApplicationRunAtStartup );
            #endregion

            // PBF Website
            //
            miPBFWebsite = new MenuItem( PBFWebsiteMenuText.Title,
                                         MIEventPBFWebsite );

            // Application's icon
            //
            mAppNotifyIcon = new NotifyIcon()
            {
                // Be sure to add a resource named 'AppIcon' to the Solution Explorer
                // item's 'Resources.resx' designer.
                //
                Icon = Properties.Resources.AppIcon,

                Text = APP_TITLE,

                ContextMenu = new ContextMenu(
                                  new MenuItem[]
                                  {
                                      new MenuItem( "Ask a question!", MIEventAskAQuestion ),

                                      new MenuItem( "-" ),

                                      miIcon,
                                      miApplication,
                                      miPBFWebsite,

                                      new MenuItem( "About " + APP_TITLE, MIEventAbout ),

                                      new MenuItem( "-" ),

                                      new MenuItem( "Exit", MIEventExit )
                                  } ),

                Visible = true
            };

            // Set the 'mouse up' event to our handler method
            //
            mAppNotifyIcon.MouseUp += new MouseEventHandler( AppNotifyIcon_MouseUp );

            // Enable/disable the 'Taskbar Settings' menu option based on the version
            // of Windows
            //
            miIcon.MenuItems[ (int)IconMenuIndex.TaskbarSettings ].Enabled =
                PBFOSVersion.IsAtLeastWindows8;

            // Run at Startup
            //
            miApplication.MenuItems[ (int)ApplicationMenuIndex.RunAtStartup ].Enabled =
                PBFRunAtStartup.CanRunAtStartup( APP_NAME );

            if (miApplication.MenuItems[ (int)ApplicationMenuIndex.RunAtStartup ].Enabled)
            {
                miApplication.MenuItems[ (int)ApplicationMenuIndex.RunAtStartup ].Checked =
                    PBFRunAtStartup.GetRunAtStartup( APP_NAME );
            }

            // Set the 'About' menu item text to 'bold'
            //
            mAppNotifyIcon.ContextMenu.MenuItems[ (int)AppMenuIndex.About ].DefaultItem = true;

            // Set the application icon
            //
            SetApplicationIcon();

            // Set the application icon's tooltip to the initial random answer
            //
            mAppNotifyIcon.Text = mMagic8Ball.Answer;

            // Set the CPU Base Priority to Idle/Low, since this application is intended
            // to be an 'ambient' icon and needs only limited processing time.  This
            // allows this application to leave more resources for the other applications
            // that are running on this machine.
            //
            PBFProcess.SetCPUPriorityToLow();
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Start the 'Ask a Question' timer, which changes the application's
        /// icon for one second to represent the ball being turned over, and
        /// then changes it back with the notification text set to the
        /// question's answer.
        /// </summary>
        ///-------------------------------------------------------------------
        private void StartAskAQuestionTimer()
        {
            SetAskAQuestionIcon();

            mAskAQuestionTimer =
                new System.Threading.Timer( AskAQuestionTimer_Callback,
                                            null,
                                            ASK_A_QUESTION_DELAY,
                                            ASK_A_QUESTION_PERIOD );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Mouse click handler for the application's icon.
        /// </summary>
        /// <remarks>
        /// On a left mouse click, ask a question and display the answer using
        /// the corresponding menu option's event.
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="mea"></param>
        ///-------------------------------------------------------------------
        private void AppNotifyIcon_MouseUp( object sender, MouseEventArgs mea )
        {
            if (mea.Button == MouseButtons.Left)
            {
                // Get the answer from the Magic 8-Ball object
                //
                // Check the keyboard for [Ctrl] and [Shift] states, and
                // 'load' the answer appropriately:
                //
                //   [Ctrl] and [Shift]  Always a Neutral answer
                //   [Ctrl]              Always a Positive answer
                //   [Shift]             Always a Negative answer
                //   <no modifier keys>  A random answer
                //
                if (PBFKeyboard.IsCtrlKeyDown &&
                    PBFKeyboard.IsShiftKeyDown)
                {
                    mMagic8Ball.GetAnswerToQuestion( string.Empty, PBFMagic8Ball.AnswerType.Neutral );
                }
                else
                if (PBFKeyboard.IsCtrlKeyDown)
                {
                    mMagic8Ball.GetAnswerToQuestion( string.Empty, PBFMagic8Ball.AnswerType.Positive );
                }
                else
                if (PBFKeyboard.IsShiftKeyDown)
                {
                    mMagic8Ball.GetAnswerToQuestion( string.Empty, PBFMagic8Ball.AnswerType.Negative );
                }
                else
                { 
                    mMagic8Ball.GetAnswerToQuestion();
                }

                // Start the 'flip' animation timer
                //
                StartAskAQuestionTimer();
            }
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Called after the 'Ask a Question' timer expires.
        /// </summary>
        /// <param name="state"></param>
        ///-------------------------------------------------------------------
        private void AskAQuestionTimer_Callback( object state )
        {
            // Immediately stop the timer
            //
            mAskAQuestionTimer.Change( Timeout.Infinite, 0 );
            mAskAQuestionTimer = null;

            SetApplicationIcon();

            // Show the answer in the application's icon
            //
            DisplayTheAnswerInTheAppIcon();

            GC.Collect();
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Changes the main application icon to the appropriate '8-ball' icon.
        /// </summary>
        ///-------------------------------------------------------------------
        private void SetApplicationIcon()
        {
            switch (PBFNotifyIconSize.IconSize)
            {
                case 64: mAppNotifyIcon.Icon = Properties.Resources.Magic8Ball64; break;

                case 32: mAppNotifyIcon.Icon = Properties.Resources.Magic8Ball32; break;

                default: mAppNotifyIcon.Icon = Properties.Resources.Magic8Ball16; break;
            }
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Changes the main application icon to the appropriate 'Ask a
        /// Question' icon.
        /// </summary>
        ///-------------------------------------------------------------------
        private void SetAskAQuestionIcon()
        {
            switch (PBFNotifyIconSize.IconSize)
            {
                case 64:
                {
                    if (mAskAQuestionCoin.Flip())
                    {
                        mAppNotifyIcon.Icon = Properties.Resources.BlueWindowUp64;
                    }
                    else
                    {
                        mAppNotifyIcon.Icon = Properties.Resources.BlueWindowDown64;
                    }
                }
                break;

                case 32:
                {
                    if (mAskAQuestionCoin.Flip())
                    {
                        mAppNotifyIcon.Icon = Properties.Resources.BlueWindowUp32;
                    }
                    else
                    {
                        mAppNotifyIcon.Icon = Properties.Resources.BlueWindowDown32;
                    }
                }
                break;

                default:
                {
                    if (mAskAQuestionCoin.Flip())
                    {
                        mAppNotifyIcon.Icon = Properties.Resources.BlueWindowUp16;
                    }
                    else
                    {
                        mAppNotifyIcon.Icon = Properties.Resources.BlueWindowDown16;
                    }
                }
                break;
            }
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Display the answer in the application icon's tooltip and in a
        /// balloon tip.
        /// </summary>
        ///-------------------------------------------------------------------
        private void DisplayTheAnswerInTheAppIcon()
        {
            // Set the application icon's tooltip to the answer
            //
            mAppNotifyIcon.Text = mMagic8Ball.Answer;
    
            // Show a balloon tip with the answer
            //
            mAppNotifyIcon.ShowBalloonTip(
                SHOW_BALLOON_TIP_TIMEOUT,
                mMagic8Ball.Answer,
                APP_BALLOON_TIP_TITLE,
                ToolTipIcon.None );
        }

        #endregion

        #region Menu Item Events

        ///-------------------------------------------------------------------
        /// <summary>
        /// Ask the Magic 8-Ball a question and display the answer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ea"></param>
        /// <remarks>
        /// This method demonstrates the principle of 'separation of concerns',
        /// where calculations are performed in one step, and the results of
        /// those calculations are displayed in another step.
        /// </remarks>
        ///-------------------------------------------------------------------
        private void MIEventAskAQuestion( object sender, EventArgs ea )
        {
            StartAskAQuestionTimer();
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Launches the Taskbar Settings page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ea"></param>
        ///-------------------------------------------------------------------
        private void MIEventIconTaskbarSettings( object sender, EventArgs ea )
        {
            PBFSystem.LaunchTaskbarSettings();
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Launches the Control Panel applet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ea"></param>
        ///-------------------------------------------------------------------
        private void MIEventIconOpenControlPanel( object sender, EventArgs ea )
        {
            PBFSystem.LaunchControlPanel( PBFOSVersion.IsWindows7 );
        }

        // Application
        //
        //-------------------------------------------------------------------
        private void MIEventApplicationRunAtStartup( object sender, EventArgs ea )
        {
            if ( ! miApplication.MenuItems[ (int)ApplicationMenuIndex.RunAtStartup ].Enabled)
            {
                return;
            }

            if (((MenuItem)sender).Checked)
            {
                PBFRunAtStartup.DeleteRunAtStartup( APP_NAME );
            }
            else
            {
                PBFRunAtStartup.SetRunAtStartup( APP_NAME );
            }

            ((MenuItem)sender).Checked = ! ((MenuItem)sender).Checked;
        }
        
        //-------------------------------------------------------------------
        private void MIEventPBFWebsite( object sender, EventArgs ea )
        {
            PBFSystem.LaunchWebBrowser( PBF_URL );
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Displays the 'About' dialog box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ea"></param>
        ///-------------------------------------------------------------------
        private void MIEventAbout( object sender, EventArgs ea )
        {
            if ( ! mAppNotifyIcon.ContextMenu.MenuItems[ (int)AppMenuIndex.About ].Enabled)
            {
                return;
            }

            mAppNotifyIcon.ContextMenu.MenuItems[ (int)AppMenuIndex.About ].Enabled = false;

            try
            {
                MessageBox.Show(
                    PBFFileVersionInfo.Description + "\n\n" +
                    PBFFileVersionInfo.Copyright + "\n\n" +
                    "Visit ParagonBitFoundry.com for more extraordinary software!",
                    APP_TITLE + " v" + PBFFileVersionInfo.FileVersionStr,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information );
            }
            finally
            {
                mAppNotifyIcon.ContextMenu.MenuItems[ (int)AppMenuIndex.About ].Enabled = true;
            }
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Hides the application's icon, and exits the program.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ea"></param>
        ///-------------------------------------------------------------------
        private void MIEventExit( object sender, EventArgs ea )
        {
            // Hide the program's icon
            //
            mAppNotifyIcon.Visible = false;

            // Exit the program
            //
            Application.Exit();
        }

        #endregion

    } // class - AppCustomApplicationContext

} // namespace
