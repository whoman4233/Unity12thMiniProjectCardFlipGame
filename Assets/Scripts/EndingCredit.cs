/* �ۼ��� : ���ƶ�
 * - �ʼ� ���� ��� �� �ϳ��� "���� �Ұ� �����ֱ�" �� UI ������Ʈ�� Ȱ��/��Ȱ��ȭ �� ����
 * - ���� ��, Ȱ��ȭ�Ǵ� EndingCredit UI �� ����
 *   - RectTransform ���� UI ������Ʈ�� ��ǥ�� ����
 *   - EndingCredit �� �Ʒ����� ���� ���� �ӵ��� �̵�
 *   - EndingCredit �� ȭ�� �� ���� �ٴٸ��� ���� ��, �ٽ��ϱ� ��ư ǥ��
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