using UnityEngine;

public class QuizUI : MonoBehaviour
{
    public QuizSO quiz;
    public TMPro.TextMeshProUGUI questionText;
    public TMPro.TextMeshProUGUI[] answerTexts;

    PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    public void SetQuiz(QuizSO quiz)
    {
        Time.timeScale = 0;
        this.quiz = quiz;
        questionText.text = quiz.question;
        for (int i = 0; i < quiz.answers.Length; i++)
        {
            answerTexts[i].text = quiz.answers[i];
        }
    }

    public void CheckAnswer(int answer)
    {
        if (quiz.CheckAnswer(answer))
        {
            Debug.Log("Correct!");
            playerHealth.Heal(2);
        }
        else
        {
            Debug.Log("Incorrect!");
            playerHealth.TakeDamage(2);
        }
        Time.timeScale = 1;
        gameObject.GetComponent<Canvas>().enabled = false;
    }
}
