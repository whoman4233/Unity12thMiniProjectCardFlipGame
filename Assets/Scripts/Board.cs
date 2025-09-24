using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Transform cards;
    public GameObject card;

    void Start()
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
                float x = (i / 4) * 1.4f - 1.4f;
                float y = (i % 4) * 1.4f - 3.0f;

                go.transform.position = new Vector2(x, y);
                go.GetComponent<Card>().Settings(arr[i]);
            }

            GameManager.Instance.cardCount = arr.Length;
        }
        else if (ChooseStage.Instance.Stage == 2)
        {
            Debug.Log("NormalStage");
            int[] arr = { 0, 0, 1, 1, 3, 3, 4, 4, 6, 6, 7, 7, 9, 9, 10, 10, 12, 12, 11, 11, 15, 15, 16, 16 };
            arr = arr.OrderBy(x => Random.Range(0f, arr.Length)).ToArray();

            for (int i = 0; i < 24; i++)
            {

                GameObject go = Instantiate(card, this.transform);

                go.transform.localScale = new Vector3(1f, 1f, 1f); //카드 크기 수정

                float x = (i / 6) * 1.3f - 1.9f;
                float y = (i % 6) * 1.25f - 4.2f;

                go.transform.position = new Vector2(x, y);

                go.GetComponent<Card>().Settings(arr[i]);
            }
        }
        else if (ChooseStage.Instance.Stage == 3)
        {

            int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 17 };
            arr = arr.OrderBy(x => Random.Range(0f, arr.Length)).ToArray();

            for (int i = 0; i < 36; i++)
            {
                Debug.Log("HardStage");

                GameObject go = Instantiate(card, this.transform);

                go.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f); //카드 크기 수정

                float x = (i / 6) * 0.95f - 2.35f;
                float y = (i % 6) * 1.05f - 3.9f;

                go.transform.position = new Vector2(x, y);

                go.GetComponent<Card>().Settings(arr[i]);
            }

        }
    }
}
