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
        frontImage.sprite = Resources.Load<Sprite>($"membercard{idx}");
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Open()
    {
        audioSource.PlayOneShot(clip);
        anim.SetBool("IsOpen", true);
        Front.SetActive(true);
        Back.SetActive(false);

        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
        }
        else
        {
            GameManager.Instance.secondCard = this;
            GameManager.Instance.isMatched();
        }
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
        anim.SetBool("isOpen", false);
        Front.SetActive(false);
        Back.SetActive(true);
    }

    public void ShakeCard()
    {
        StartCoroutine(ShakeCardAnim());
    }

    IEnumerator ShakeCardAnim()
    {
        Vector3 originalPos = transform.localPosition; // OK: 로컬 저장
        float time = 0f;
        float duration = 0.3f;
        float shakeAmount = 0.1f;
        float frequency = 50f;

        while (time < duration)
        {
            float offset = Mathf.Sin(time * frequency) * shakeAmount;
            transform.localPosition = originalPos + new Vector3(offset, 0f, 0f); // 로컬로 흔들기
            time += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos; // ★ 마지막도 로컬로 복구
    }

}
