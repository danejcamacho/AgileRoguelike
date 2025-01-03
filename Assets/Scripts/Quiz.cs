using UnityEngine;

public class Quiz : MonoBehaviour
{
    public QuizSO quiz;
    Canvas quizCanvas;

    void Start()
    {
        quizCanvas = GameObject.Find("QuizCanvas").GetComponent<Canvas>();
        quizCanvas.gameObject.SetActive(false);
    }

    public void BeginQuiz()
    {
        quizCanvas.gameObject.SetActive(true);
        quizCanvas.GetComponent<QuizUI>().SetQuiz(quiz);
    }
}
