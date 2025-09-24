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
        }
        else if (ChooseStage.Instance.Stage == 3)
        {
            Debug.Log("HardStage");
        }
    }
}
