using UnityEngine;

[CreateAssetMenu(fileName = "New Quiz", menuName = "Quiz")]
public class QuizSO : ScriptableObject
{
    public string question;
    public string[] answers;
    public int correctAnswer;

    public bool CheckAnswer(int answer)
    {
        return answer == correctAnswer;
    }
}
