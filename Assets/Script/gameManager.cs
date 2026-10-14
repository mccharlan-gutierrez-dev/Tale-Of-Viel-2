using TMPro;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;
    public TextMeshProUGUI meatText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI attkText;
    public int meatCollected = 0;
    public int coinCollected = 0;
    public int attkCollected = 0;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    
    void Update()
    {
        meatText.text = meatCollected.ToString();
        coinText.text = coinCollected.ToString();
        attkText.text = attkCollected.ToString();
    }
}
