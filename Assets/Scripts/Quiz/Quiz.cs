using UnityEngine;

public class Quiz : MonoBehaviour
{
    private QuizSO quiz;
    Canvas quizCanvas;

    public QuizSO[] quizzes;

    void Start()
    {
        quizCanvas = GameObject.Find("QuizCanvas").GetComponent<Canvas>();
        quizCanvas.enabled = false;
    }

    public void BeginQuiz()
    {

        //choose a random quiz
        quiz = quizzes[Random.Range(0, quizzes.Length)];
        quizCanvas.enabled = true;
        quizCanvas.GetComponent<QuizUI>().SetQuiz(quiz);
    }
}
