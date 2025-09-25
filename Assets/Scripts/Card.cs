using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0;
    public SpriteRenderer frontImage;
    public GameObject Front;
    public GameObject Back;
    public Animator anim;
    public AudioClip clip;
    public AudioSource audioSource;

    public void Settings(int numbs)
    {
        idx = numbs;

        if (ChooseStage.Instance.Stage <= 3)
        {
            frontImage.sprite = Resources.Load<Sprite>($"membercard{idx}");
        }
        else if (ChooseStage.Instance.Stage == 99) //Hidden
        {
            frontImage.sprite = Resources.Load<Sprite>($"rtan{idx}");
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Open()
    {
        audioSource.PlayOneShot(clip);

        anim.SetBool("IsOpen", true);

        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
        }
        else
        {
            if(GameManager.Instance.firstCard.transform.position != this.gameObject.transform.position)
            {
                GameManager.Instance.secondCard = this;
                GameManager.Instance.isMatched();
            }
        }
    }

    public void ShowFront()
    {
        Back.SetActive(false);
        Front.SetActive(true);
    }

    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 0.5f);
    }
    public void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }
    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 0.5f);
    }
    void CloseCardInvoke()
    {
        anim.SetBool("IsOpen", false);
        Front.SetActive(false);
        Back.SetActive(true);
    }

    public void ShakeCard()
    {
        StartCoroutine(ShakeCardAnim());
    }

    IEnumerator ShakeCardAnim()
    {
        Vector3 originPos = transform.localPosition;
        float time = 0f;
        float duration = 0.4f;
        float shakeAmount = 0.1f;
        float frequency = 50f;

        while (time < duration)
        {
            float offset = Mathf.Sin(time * frequency) * shakeAmount;
            transform.localPosition = originPos + new Vector3(offset, 0f, 0f);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originPos;
    }

}
