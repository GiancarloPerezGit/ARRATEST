using UnityEngine;

namespace RoboticsAcademy.DataCollection
{
    /// <summary>
    /// Data writer for pointer data.
    /// </summary>
    class AssessmentWriter : DataWriter
    {
        /// <summary>
        /// Get Assessment capability.
        /// </summary>
        protected override bool InitializeDataProvider() { return true; }

        /// <summary>
        /// Write assessment header.
        /// </summary>
        protected override void WriteHeader()
        {
            dataStream.WriteLine("Question#,Selection,Answer,CorrectStatus,Event,SecondsElapsed,AnswersCorrect,PassingPercent,PercentageCorrect,PassingStatus,TotalSeconds");
        }
        protected override void WriteData() { }

        public void WriteQuestionData(QuestionObject question, int number)
        {
            dataStream.WriteLine("{0},{1},{2},{3},{4},{5}",
                number,
                question.data.selection,
                question.answer,
                question.data.correct,
                "ANSWER",
                GetElapsedSeconds());
        }

        public void WriteEventData(string eventName)
        {
            dataStream.WriteLine(",,,,{0},{1}",
                eventName,
                GetElapsedSeconds());
        }

        public void WriteResults(AssessmentObject assessment)
        {
            dataStream.WriteLine(",,,,,,{0},{1},{2},{3},{4}",
                assessment.data.answersCorrect,
                assessment.passingPercentage,
                assessment.data.percentageCorrect,
                assessment.data.passingStatus,
                GetElapsedSeconds());
        }
    }
}
