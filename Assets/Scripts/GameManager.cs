using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Time.timeScale = 1.0f;
    }

    public AudioSource audioSource;
    public AudioClip clip;
    public AudioClip siren;
    public AudioClip beep;

    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;
    float time = 0.0f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Onsiren", 25f, 0.9f);
    }

    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (time >= 30.0f)
        {
            GameOver();
        }
    }


    public int cardCount = 0;
    public void isMatched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(clip);
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            if (cardCount <= 0)
            {
                GameOver();
            }
        }
        else
        {
            audioSource.PlayOneShot(beep);
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        firstCard = null;
        secondCard = null;
    }

    public GameObject endTxt;

    public void GameOver()
    {
        endTxt.SetActive(true);
        Time.timeScale = 0.0f;
    }

    void Onsiren()
    {
        audioSource.PlayOneShot(siren);
    }
}
