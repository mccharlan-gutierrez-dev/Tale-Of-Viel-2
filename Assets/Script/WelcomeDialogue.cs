
using UnityEngine;

public class WelcomeDialogue : MonoBehaviour
{
    [Header("Dialogue References")]
    public GameObject welcomePanel;
    public QuestionManager questionManager;

    private void Awake()
    {
        if (welcomePanel != null)
            welcomePanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (welcomePanel != null)
            welcomePanel.SetActive(true);

        if (questionManager != null)
            questionManager.OpenDialogue();
        else
            Debug.LogWarning(
                "WelcomeDialogue: Question Manager is not assigned!",
                this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (welcomePanel != null)
            welcomePanel.SetActive(false);
    }
}