using UnityEngine;
using RoboticsAcademy.DataCollection;
using System;
using UnityEngine.UI;

public class ExperimentProtocol : MonoBehaviour
{
    [SerializeField] Text instructionText;

    [SerializeField] DataCollectionSystem dataCollection;
    [SerializeField] DemographicWriter demographicData;
    [SerializeField] AssessmentWriter assessmentData;

    [SerializeField] AssessmentController preTest;
    [SerializeField] AssessmentController postTest;
    bool pretestCurrent = true;
    string uploadPath = "experiments\\MagicLeapExperiment_" + DateTime.UtcNow.ToString("MM-dd-yyyy_HH-mm-ss-fff");

    public void Awake()
    {
        instructionText.text = "Press Begin after filling out this information.";
    }

    public void StartButton()
    {
        instructionText.gameObject.SetActive(false);

        if (pretestCurrent)
        {
            StartAssessmentCollection("PRETEST_" + uploadPath);

            demographicData.WriteDemographics();
            preTest.Begin();
        }
        else
        {
            demographicData.gameObject.SetActive(false);
            StartAssessmentCollection("POSTTEST_" + uploadPath);
            
            postTest.Begin();
        }
        assessmentData.WriteEventData("START");

    }
    public void SubmitButton()
    {
        assessmentData.WriteEventData("SUBMIT");
        if (pretestCurrent)
        {
            preTest.Submit();
            FinishAssessmentCollection(preTest);

            pretestCurrent = false;

            instructionText.gameObject.SetActive(true);
            instructionText.text = "Press Begin ONLY AFTER the proctor tells you to.";
        }
        else
        {
            postTest.Submit();
            FinishAssessmentCollection(postTest);

            Application.Quit();
        }
    }
    public void NextButton()
    {
        assessmentData.WriteEventData("NEXT");
        if (pretestCurrent)
        {
            preTest.NextQuestion();
        }
        else
        {
            postTest.NextQuestion();
        }
    }

    public void BackButton()
    {
        assessmentData.WriteEventData("BACK");
        if (pretestCurrent)
        {
            preTest.PreviousQuestion();
        }
        else
        {
            postTest.PreviousQuestion();
        }
    }

    public void AnswerButton(int value)
    {
        if (pretestCurrent)
        {
            if (preTest.AnswerQuestion(value))
            {
                assessmentData.WriteQuestionData(preTest.assessment.questions[preTest.questionIndex], preTest.questionIndex);
            }
        }
        else
        {
            if (postTest.AnswerQuestion(value))
            {
                assessmentData.WriteQuestionData(postTest.assessment.questions[postTest.questionIndex], postTest.questionIndex);
            }
        }
    }

    private void StartAssessmentCollection(string path)
    {
        dataCollection.SetUploadPath(path);
        dataCollection.DataCollectionStart();
    }

    private void FinishAssessmentCollection(AssessmentController controller)
    {
        assessmentData.WriteResults(controller.assessment);

        // Finish collecting all data.
        dataCollection.DataCollectionStop();
        dataCollection.DataCollectionUpload();
    }
}
