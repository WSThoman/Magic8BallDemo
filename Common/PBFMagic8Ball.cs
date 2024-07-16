//---------------------------------------------------------------------------
//
// Copyright © 2018-2023 Paragon Bit Foundry.  All Rights Reserved.
//
//---------------------------------------------------------------------------
using System;

namespace PBFCommon
{
    public class PBFMagic8Ball
    {
        #region Constants

        private const byte INDEX_START_ANSWERS_RANDOM   = 0;
        private const byte INDEX_START_ANSWERS_POSITIVE = 0;
        private const byte INDEX_START_ANSWERS_NEUTRAL  = 10;
        private const byte INDEX_START_ANSWERS_NEGATIVE = 15;

        private const byte INDEX_NUM_ANSWERS_RANDOM   = 20;
        private const byte INDEX_NUM_ANSWERS_POSITIVE = 10;
        private const byte INDEX_NUM_ANSWERS_NEUTRAL  = 5;
        private const byte INDEX_NUM_ANSWERS_NEGATIVE = 5;

        private const byte PREVIOUS_RANDOM_LIST_SIZE = 3;

        public enum AnswerType
        {
            Random, Positive, Neutral, Negative
        }

        // Answer list from: https://en.wikipedia.org/wiki/Magic_8-Ball
        // 
        private readonly string[] mAnswers =
        {
            "It is certain",
            "It is decidedly so",
            "Without a doubt",
            "Yes, definitely",
            "You may rely on it",
                    
            "As I see it, yes",
            "Most likely",
            "Outlook good",
            "Yes",
            "Signs point to yes",

            "Reply hazy try again",
            "Ask again later",
            "Better not tell you now",
            "Cannot predict now",
            "Concentrate and ask again",

            "Don't count on it",
            "My reply is no",
            "My sources say no",
            "Outlook not so good",
            "Very doubtful"
        };

        #endregion

        #region Data Members

        private readonly PBFRandomPrevStandard mRandomPrev = null;

        private byte mAnswerIndex = 0;

        #endregion

        #region Properties

        public string Answer { get { return mAnswers[ mAnswerIndex ]; } }

        #endregion

        #region Constructors

        //-------------------------------------------------------------------
        public PBFMagic8Ball( Random theRandomObj = null )
        {
            if (theRandomObj != null)
            {
                mRandomPrev =
                    new PBFRandomPrevStandard( theRandomObj,
                                               INDEX_START_ANSWERS_RANDOM,
                                               INDEX_NUM_ANSWERS_RANDOM,
                                               PREVIOUS_RANDOM_LIST_SIZE );
            }
            else
            {
                mRandomPrev =
                    new PBFRandomPrevStandard( new Random( DateTime.Now.GetHashCode() ),
                                               INDEX_START_ANSWERS_RANDOM,
                                               INDEX_NUM_ANSWERS_RANDOM,
                                               PREVIOUS_RANDOM_LIST_SIZE );
            }

            // Initialize the Magic 8-Ball to a 'positive' answer
            //
            SelectAPositiveAnswer();

        } // Constructor - default

        #endregion

        #region Class methods

        ///-------------------------------------------------------------------
        /// <summary>
        /// Selects a random answer from the array of all possible answers.
        /// </summary>
        /// <remarks>
        /// Sets <see cref="mAnswerIndex"/> to a random number, which is later
        /// used by the read-only property <seealso cref="Answer"/> to return
        /// the corresponding string.
        /// </remarks>
        ///-------------------------------------------------------------------
        private void SelectAnAnswer()
        {
            mAnswerIndex =
                (byte)(INDEX_START_ANSWERS_RANDOM +
                       mRandomPrev.NextButNotPrev( INDEX_NUM_ANSWERS_RANDOM ));
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Selects a random 'positive' answer from the array of possible answers.
        /// </summary>
        /// <remarks>
        /// Sets <see cref="mAnswerIndex"/> to a random 'positive'/'Yes' answer.
        /// Similar to <seealso cref="SelectAnAnswer"/>.
        /// </remarks>
        ///-------------------------------------------------------------------
        private void SelectAPositiveAnswer()
        {
            mAnswerIndex =
                (byte)(INDEX_START_ANSWERS_POSITIVE +
                       mRandomPrev.NextButNotPrev( INDEX_NUM_ANSWERS_POSITIVE ));
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Selects a random 'neutral' answer from the array of possible answers.
        /// </summary>
        /// <remarks>
        /// Sets <see cref="mAnswerIndex"/> to a random 'neutral'/'Maybe' answer.
        /// Similar to <seealso cref="SelectAnAnswer"/>.
        /// </remarks>
        ///-------------------------------------------------------------------
        private void SelectANeutralAnswer()
        {
            mAnswerIndex =
                (byte)(INDEX_START_ANSWERS_NEUTRAL +
                       mRandomPrev.NextButNotPrev( INDEX_NUM_ANSWERS_NEUTRAL ));
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Selects a random 'negative' answer from the array of possible answers.
        /// </summary>
        /// <remarks>
        /// Sets <see cref="mAnswerIndex"/> to a random 'negative'/'No' answer.
        /// Similar to <seealso cref="SelectAnAnswer"/>.
        /// </remarks>
        ///-------------------------------------------------------------------
        private void SelectANegativeAnswer()
        {
            mAnswerIndex =
                (byte)(INDEX_START_ANSWERS_NEGATIVE +
                       mRandomPrev.NextButNotPrev( INDEX_NUM_ANSWERS_NEGATIVE ));
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Return one of 20 possible answers
        /// </summary>
        /// <param name="theQuestion"></param>
        /// <remarks>
        /// Note that the parameter <paramref name="theQuestion"/> is, of course,
        /// not used.
        /// </remarks>
        ///-------------------------------------------------------------------
        public string GetAnswerToQuestion( string theQuestion = "",
                                           AnswerType theAnswerType = AnswerType.Random )
        {
            // The question that the Magic 8-Ball is asked does not matter, of course :)
            //
            if (theQuestion.Length == 0)
            {
            }

            switch (theAnswerType)
            {
                case AnswerType.Positive: SelectAPositiveAnswer(); break;
                case AnswerType.Neutral:  SelectANeutralAnswer();  break;
                case AnswerType.Negative: SelectANegativeAnswer(); break;
                
                default: SelectAnAnswer(); break;
            }

            return Answer;
        }

        #endregion

    } // class - PBFMagic8Ball

} // namespace
