
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

    //sound
    public GameObject SFXinstance;

    //public AudioClip Attack;
    //public AudioClip Death;
    //public AudioClip deathSFX;
    //public AudioClip collectSFX;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            // Use the UI references from the new scene
            instance.meatText = meatText;
            instance.coinText = coinText;
            instance.attkText = attkText;

            Destroy(gameObject);
        }
    }

    public void playSFX(AudioClip clip, int volume, Transform t)
    {
        AudioSource aud = Instantiate(SFXinstance, t.position, Quaternion.identity).GetComponent<AudioSource>();
        aud.clip = clip;
        aud.volume = volume;
        aud.Play();
    }

    void Update()
    {
        if (meatText != null)
            meatText.text = meatCollected.ToString();

        if (coinText != null)
            coinText.text = coinCollected.ToString();

        if (attkText != null)
            attkText.text = attkCollected.ToString();
    }
}
