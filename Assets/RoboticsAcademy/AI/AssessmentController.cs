using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using RoboticsAcademy.DataCollection;

public class AssessmentController : MonoBehaviour
{
    [SerializeField] string jsonAssessmentData;
    
    [SerializeField] Text questionText;
    [SerializeField] Text answersText;
    [SerializeField] Toggle[] answerChoices;

    public AssessmentObject assessment { get; private set; }
    public int questionIndex { get; private set; }
    bool inputToggled = true;

    void Awake()
    {
        questionIndex = 0;
        string[] paths = { Application.streamingAssetsPath, jsonAssessmentData };
        assessment = AssessmentObject.CreateFromJSON(Path.Combine(paths));
    }

    public void Begin()
    {
        questionText.gameObject.SetActive(true);
        answersText.gameObject.SetActive(true);

        questionIndex = 0;
        SetQuestion(assessment.questions[0]);
    }

    private void SetQuestion(QuestionObject q)
    {
        questionText.text = q.question;

        string answers = "";
        foreach(var ans in q.choices)
        {
            answers += ans.choice + "\n\n";
        }
        answersText.text = answers;
        RefreshSelectedAnswer(q);
    }
    public void NextQuestion()
    {
        if (questionIndex < assessment.questions.Length - 1)
        {
            ++questionIndex;
            SetQuestion(assessment.questions[questionIndex]);
        }
    }

    public void PreviousQuestion()
    {
        if (questionIndex > 0)
        {
            --questionIndex;
            SetQuestion(assessment.questions[questionIndex]);
        }
    }

    private void RefreshSelectedAnswer(QuestionObject q)
    {
        inputToggled = false;
        foreach (var toggle in answerChoices)
        {
            toggle.isOn = false;
        }

        var currentSelect = q.data.selection;
        if (currentSelect != null)
        {
            answerChoices[currentSelect.GetValueOrDefault()].isOn = true;
        }
        inputToggled = true;
    }

    public bool AnswerQuestion(int selection)
    {
        if (inputToggled && answerChoices[selection].isOn) // Make sure input was actually toggled, and not just UI update.
        {
            assessment.questions[questionIndex].data.selection = selection;
            if (selection == assessment.questions[questionIndex].answer)
            {
                assessment.questions[questionIndex].data.correct = true;
            }
            else
            {
                assessment.questions[questionIndex].data.correct = false;
            }
            return true;
        }
        else return false;
    }

    public void Submit()
    {
        questionText.gameObject.SetActive(false);
        answersText.gameObject.SetActive(false);

        int score = 0;
        foreach (QuestionObject q in assessment.questions)
        {
            if (q.data.correct)
                score++;
        }

        assessment.data.answersCorrect = score;
        assessment.data.percentageCorrect = (float)score / assessment.questions.Length * 100;
        assessment.data.passingStatus = assessment.data.percentageCorrect > assessment.passingPercentage;
    }
}
