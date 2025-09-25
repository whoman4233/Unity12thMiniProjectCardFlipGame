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

    public AudioClip beep;

    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;
    float time = 0.0f;
    // ★ 매칭 처리 중 입력 잠금 플래그
    public bool IsResolvingPair { get; private set; }


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.CancelInvoke("Onsiren");
        AudioManager.Instance.InvokeRepeating("Onsiren", 25f, 0.9f);
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
            if (cardCount == 2)
            {
                firstCard.DestroyCardInvoke();
                secondCard.DestroyCardInvoke();
            }
            else
            {
                firstCard.DestroyCard();
                secondCard.DestroyCard();
            }

            cardCount -= 2;

            if (cardCount <= 0)
            {
                GameClear();
            }
        }
        else
        {
            audioSource.PlayOneShot(beep);
            firstCard.ShakeCard();
            secondCard.ShakeCard();
            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        firstCard = null;
        secondCard = null;
    }

    public GameObject endPanel;
    public GameObject retryBtn;
    public GameObject endingCredit;

    public void GameOver()
    {
        endPanel.SetActive(true);
        ShowRetryBtn();
        Time.timeScale = 0.0f;
    }

    public void GameClear()
    {
        if (ChooseStage.Instance.StageUnlocked == 1)
        {
            Debug.Log(ChooseStage.Instance.StageUnlocked);
            PlayerPrefs.SetInt("StageUnlocked", 2);
            PlayerPrefs.Save();
        }
        else if (ChooseStage.Instance.StageUnlocked == 2)
        {
            PlayerPrefs.SetInt("StageUnlocked", 3);
            PlayerPrefs.Save();
        }
        Debug.Log(ChooseStage.Instance.StageUnlocked);
        endingCredit.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void ShowRetryBtn()
    {
        retryBtn.SetActive(true);
    }
}
