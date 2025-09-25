/* 작성자 : 조아라
 * - 필수 구현 기능 중 하나인 "팀원 소개 보여주기" 를 UI 오브젝트의 활성/비활성화 로 구현
 * - 성공 시, 활성화되는 EndingCredit UI 를 제어
 *   - RectTransform 으로 UI 오브젝트의 좌표에 접근
 *   - EndingCredit 이 아래에서 위로 일정 속도로 이동
 *   - EndingCredit 이 화면 맨 위에 다다르면 정지 후, 다시하기 버튼 표시
 */

using UnityEngine;

public class EndingCredit : MonoBehaviour
{
    void Update()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        if (rectTransform.anchoredPosition.y < 0)
            transform.position += Vector3.up * 0.5f;

        if (rectTransform.anchoredPosition.y == 0)
            GameManager.Instance.ShowRetryBtn();
    }
}