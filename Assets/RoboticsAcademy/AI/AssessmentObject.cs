using System;
using System.IO;
using UnityEngine;

/// <summary>
/// Data collection classes.
/// </summary>
public class AssessmentData
{
    public int answersCorrect = 0;
    public float percentageCorrect = 0;
    public bool passingStatus = false;
}
public class QuestionData
{
    public int? selection = null;
    public bool correct = false;
}

/// <summary>
/// Classes for JSON Deserialization
/// </summary>
[System.Serializable]
public class QuestionObject
{
    public string question;
    public AnswerChoiceObject[] choices;
    public int answer;
    public QuestionData data = new QuestionData();
}

[System.Serializable]
public class AnswerChoiceObject
{
    public string choice;
}

[System.Serializable]
public class AssessmentObject
{
    public QuestionObject[] questions;
    public int passingPercentage;
    public AssessmentData data = new AssessmentData();

    public static AssessmentObject CreateFromJSON(string jsonPath)
    {
        StreamReader reader = new StreamReader(jsonPath);
        string json = reader.ReadToEnd();
        reader.Close();
        return JsonUtility.FromJson<AssessmentObject>(json);
    }
}

