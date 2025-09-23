using UnityEngine;

public class EndingCredit : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform.anchoredPosition.y < 0)
            transform.position += Vector3.up * 0.5f;

        if (rectTransform.anchoredPosition.y == 0)
            GameManager.Instance.ShowRetryBtn();
    }
}