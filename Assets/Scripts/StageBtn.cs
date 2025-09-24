using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageBtn : MonoBehaviour
{
    public void StageChoose(GameObject ObjectName)
    {
        ChooseStage.Instance.Choose(ObjectName);
    }
}
