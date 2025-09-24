using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseStage : MonoBehaviour
{
    public static ChooseStage Instance; //ChooseStage를 싱글톤을 만들기 위한 변수

    public Image CenterImage;

    public GameObject ChickenBtn;
    public GameObject NormalBtn;
    public GameObject HardBtn;

    public GameObject StartBtn;

    public int Stage = 2;

    private void Awake()
    {
        Instance = this;
    }

    public void Choose(GameObject clickButton)
    {
        if (!StartBtn.activeSelf)
        {
            StartBtn.SetActive(true);
        }

        if (clickButton.name == "EasyBtn")
        {
            Debug.Log("Chicken");
            CenterImage.sprite = Resources.Load<Sprite>("membercard2");
            Stage = 1;
        }
        else if (clickButton.name == "NormalBtn")
        {
            Debug.Log("Normal");
            CenterImage.sprite = Resources.Load<Sprite>("membercard8");
            Stage = 2;
        }
        else
        {
            Debug.Log("Hard");
            CenterImage.sprite = Resources.Load<Sprite>("membercard10");
            Stage = 3;
        }
    }
}
