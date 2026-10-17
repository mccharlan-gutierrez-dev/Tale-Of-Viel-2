
using UnityEngine;

public class WelcomeDialogue : MonoBehaviour
{
    [Header("Dialogue References")]
    public GameObject welcomePanel;
    public QuestionManager questionManager;

    private bool hasTriggered = false;

    private void Awake()
    {
        if (welcomePanel != null)
            welcomePanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        // Prevent the dialogue from opening again
        if (hasTriggered)
            return;

        hasTriggered = true;

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
