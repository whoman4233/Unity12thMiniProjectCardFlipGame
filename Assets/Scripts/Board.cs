using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Transform cards;
    public GameObject card;
    public Vector3 scatterPosition = Vector3.zero;
    public int rows = 4;
    public int cols = 4;
    public float spacing = 1.4f;
    public float moveDuration = 1f;

    void Start()
    {
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();

        for (int i = 0; i < arr.Length; i++)
        {
            GameObject go = Instantiate(card, cards);
            var cardComp = go.GetComponent<Card>();
            if (cardComp != null) cardComp.Settings(arr[i]);
            go.transform.localPosition = scatterPosition;
        }

        StartCoroutine(ScatterCardsAfterDelay(0.5f));
    }

    IEnumerator ScatterCardsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Vector3 startPos = new Vector3(-(cols - 1) * spacing / 2f, (rows - 1) * spacing / 2f, 0);

        int i = 0;
        foreach (Transform c in cards)
        {
            int row = i / cols;
            int col = i % cols;

            Vector3 targetPos = startPos + new Vector3(col * spacing, -row * spacing, 0);
            StartCoroutine(MoveCard(c, targetPos, moveDuration));
            i++;
        }
    }

    IEnumerator MoveCard(Transform cardTransform, Vector3 targetLocalPos, float duration)
    {
        if (cardTransform == null) yield break;

        Vector3 start = cardTransform.localPosition;
        float t = 0f;
        float safeDuration = Mathf.Max(0.0001f, duration);

        while (t < 1f)
        {
            t += Time.deltaTime / safeDuration;
            cardTransform.localPosition = Vector3.Lerp(start, targetLocalPos, t);
            yield return null;
        }

        cardTransform.localPosition = targetLocalPos;
    }
}
