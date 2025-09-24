using System.Collections;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    [Header("Refs")]
    public Transform cards;            // 카드들을 담는 컨테이너(예: Board 오브젝트)
    public GameObject card;            // ★ CardParents 프리팹을 할당하세요
    public Vector3 scatterPosition = Vector3.zero;

    [Header("Grid Params")]
    public float xTimes;
    public float yTimes;
    public float xMinus;
    public float yMinus;
    public float a;                    // 열 수(예: 4, 6 ...)
    public Vector3 size;               // 부모 루트(CardParents) 스케일
    public float moveDuration = 1f;

    void Start()
    {
        if (cards == null) cards = this.transform; // 안전장치
        xTimes = yTimes = xMinus = yMinus = 0f;
        size = Vector3.one;
        StartCoroutine(ScatterCardsAfterDelay(0.5f));
    }

    IEnumerator ScatterCardsAfterDelay(float delay)
    {
        int stage = ChooseStage.Instance.Stage;

        if (stage == 1)
        {
            // 4 x 3
            int[] arr = { 0, 0, 3, 3, 6, 6, 9, 9, 12, 12, 15, 15 };
            arr = arr.OrderBy(x => Random.Range(0f, arr.Length)).ToArray();

            for (int i = 0; i < arr.Length; i++)
            {
                GameObject go = Instantiate(card, cards);                // ★ 부모는 cards
                go.transform.localPosition = scatterPosition;            // 부모 위치
                var cardComp = go.GetComponentInChildren<Card>(true);    // ★ 자식 Card
                if (cardComp) cardComp.Settings(arr[i]);
            }

            GameManager.Instance.cardCount = arr.Length;
            a = 4f;
            xTimes = 1.8f;      // ( 1.8 - (-1.8) ) / (3 - 1) = 3.6 / 2
            xMinus = 1.8f;      // -xMin

            yTimes = -1.6f;     // ( -3.4 - 1.4 ) / (4 - 1) = -4.8 / 3
            yMinus = -1.4f;     // -yTop
            size = new Vector3(1.2f, 1.2f, 1.2f);
        }
        else if (stage == 2)
        {
            // 6 x 4
            int[] arr = { 0, 0, 1, 1, 3, 3, 4, 4, 6, 6, 7, 7, 9, 9, 10, 10, 12, 12, 11, 11, 15, 15, 16, 16 };
            arr = arr.OrderBy(x => Random.Range(0f, arr.Length)).ToArray();

            for (int i = 0; i < arr.Length; i++)
            {
                GameObject go = Instantiate(card, cards);
                go.transform.localPosition = scatterPosition;
                var cardComp = go.GetComponentInChildren<Card>(true);
                if (cardComp) cardComp.Settings(arr[i]);
            }

            GameManager.Instance.cardCount = arr.Length;
            a = 6f;
            xTimes = 1.3333333f;   // ( 2.0 - (-2.0) ) / (4-1) = 4 / 3
            xMinus = 2.0f;         // -xMin

            yTimes = -1.04f;       // ( -3.6 - 1.6 ) / (6-1) = -5.2 / 5
            yMinus = -1.6f;        // -yTop
            size = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else if (stage == 3)
        {
            // 6 x 6 (균일 배치: x -2.3~2.3, y 1.9~-3.9)
            int[] arr =
            {
                0,0, 1,1, 2,2, 3,3, 4,4, 5,5, 6,6, 7,7, 8,8, 9,9,
                10,10, 11,11, 12,12, 13,13, 14,14, 15,15, 16,16, 17,17
            };
            arr = arr.OrderBy(x => Random.Range(0f, arr.Length)).ToArray();

            for (int i = 0; i < arr.Length; i++)
            {
                GameObject go = Instantiate(card, cards);
                go.transform.localPosition = scatterPosition;
                var cardComp = go.GetComponentInChildren<Card>(true);
                if (cardComp) cardComp.Settings(arr[i]);
            }

            GameManager.Instance.cardCount = arr.Length;

            // 6x6 균일 계산식(끝점 포함)
            a = 6f;
            xTimes = 0.88f;   // (2.3 - (-2.3)) / (6-1)
            yTimes = -1.12f;  // (-3.9 - 1.9) / (6-1)
            xMinus = 2.2f;    // -xMin
            yMinus = -1.8f;   // -yTop
            size = new Vector3(0.8f, 0.8f, 0.8f);
        }

        yield return new WaitForSeconds(delay);

        int v = 0;
        int cols = Mathf.RoundToInt(a);

        // ★ cards의 '직접 자식'(= CardParents 루트)만 배치/스케일
        foreach (Transform c in cards)
        {
            int col = v / cols;   // 0..cols-1 (왼→오)
            int row = v % cols;   // 0..cols-1 (위→아래)

            float x = col * xTimes - xMinus;
            float y = row * yTimes - yMinus;

            c.localScale = size;  // 부모(루트)만 스케일
            StartCoroutine(MoveCard(c, new Vector3(x, y, 0f), moveDuration));
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
