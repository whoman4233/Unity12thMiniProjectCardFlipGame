using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Transform cards;
    public GameObject card;
    public Vector3 scatterPosition = Vector3.zero;
    public float xTimes;
    public float yTimes;
    public float xMinus;
    public float yMinus;
    public Vector3 size;
    public float a;
    public float moveDuration = 1f;

    void Start()
    {
        xTimes = 0; yTimes = 0; xMinus = 0; yMinus = 0; size = Vector3.zero;
        StartCoroutine(ScatterCardsAfterDelay(0.5f));
    }

    IEnumerator ScatterCardsAfterDelay(float delay)
    {
        int stageNum = ChooseStage.Instance.Stage;

        if (ChooseStage.Instance.Stage == 1)
        {
            Debug.Log("EasyStage");
            int[] arr = { 0, 0, 3, 3, 6, 6, 9, 9, 12, 12, 15, 15 };
            arr = arr.OrderBy(x => Random.Range(0f, arr.Length)).ToArray();
            for (int i = 0; i < 12; i++)
            {
                GameObject go = Instantiate(card, this.transform);
                go.transform.localPosition = scatterPosition;
                go.GetComponent<Card>().Settings(arr[i]);
            }
            GameManager.Instance.cardCount = arr.Length;
            xTimes = 1.4f;
            yTimes = 1.4f;
            xMinus = 1.4f;
            yMinus = 3.0f;
            a = 4f;
            size = new Vector3(1.2f, 1.2f, 1.2f);
        }
        else if (ChooseStage.Instance.Stage == 2)
        {
            Debug.Log("NormalStage");
            int[] arr = { 0, 0, 1, 1, 3, 3, 4, 4, 6, 6, 7, 7, 9, 9, 10, 10, 12, 12, 11, 11, 15, 15, 16, 16 };
            arr = arr.OrderBy(x => Random.Range(0f, arr.Length)).ToArray();

            for (int i = 0; i < 24; i++)
            {
                GameObject go = Instantiate(card, this.transform);
                go.transform.localPosition = scatterPosition;
                go.GetComponent<Card>().Settings(arr[i]);
            }
            GameManager.Instance.cardCount = arr.Length;
            xTimes = 1.3f;
            yTimes = 1.25f;
            xMinus = 1.9f;
            yMinus = 4.2f;
            a = 6f;
            size = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else if (ChooseStage.Instance.Stage == 3)
        {

            int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 17 };
            arr = arr.OrderBy(x => Random.Range(0f, arr.Length)).ToArray();

            for (int i = 0; i < 36; i++)
            {
                Debug.Log("HardStage");

                GameObject go = Instantiate(card, this.transform);
                go.transform.localPosition = scatterPosition;
                go.GetComponent<Card>().Settings(arr[i]);
            }
            GameManager.Instance.cardCount = arr.Length;
            xTimes = 0.95f;
            yTimes = 1.05f;
            xMinus = 2.35f;
            yMinus = 3.9f;
            a = 6f;
            size = new Vector3(0.8f, 0.8f, 0.8f);
        }

        yield return new WaitForSeconds(delay);


        int v = 0;
        int cols = Mathf.RoundToInt(a);   
        foreach (Transform c in cards)
        {
            int col = v / cols;           
            int row = v % cols;           

            float x = col * xTimes - xMinus;
            float y = row * yTimes - yMinus;

            Vector3 targetPos = new Vector3(x, y, 0f);
            c.localScale = size;
            StartCoroutine(MoveCard(c, targetPos, moveDuration));
            v++;
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