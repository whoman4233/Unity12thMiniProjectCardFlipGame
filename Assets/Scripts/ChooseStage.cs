using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseStage : MonoBehaviour
{
    public static ChooseStage Instance; //ChooseStage를 싱글톤을 만들기 위한 변수

    public Image CenterImage;

    public GameObject ChickenBtn;

    public GameObject LockNormalBtan;
    public GameObject NormalBtn;
    public GameObject LockHardBtan;
    public GameObject HardBtn;

    public GameObject StartBtn;

    public int StageUnlocked = 0;

    public int Stage = 1;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StageUnlocked = PlayerPrefs.GetInt("StageUnlocked", 1);
    }

    public void Choose(GameObject clickButton)
    {
        switch (clickButton.name)
        {
            case "EasyBtn":
                CenterImage.sprite = Resources.Load<Sprite>("membercard2");
                Stage = 1;
                if (!StartBtn.activeSelf) //만약 시작하기 버튼이 활성화가 안 되어있으면 활성화 시켜줌
                {
                    StartBtn.SetActive(true);
                }
                break;

            case "NormalBtn":
                CenterImage.sprite = Resources.Load<Sprite>("membercard8");

                if (StageUnlocked >= 2)
                {
                    Stage = 2;

                    if (!StartBtn.activeSelf) //만약 시작하기 버튼이 활성화가 안 되어있으면 활성화 시켜줌
                    {
                        StartBtn.SetActive(true);
                    }
                }
                else
                {
                    if (StartBtn.activeSelf)//만약 활성화가 되어 있으면 비활성화 시켜 시작을 막음
                    {
                        StartBtn.SetActive(false);
                    }
                    Debug.Log(StageUnlocked);
                }
                break;

            case "HardBtn":
                CenterImage.sprite = Resources.Load<Sprite>("membercard10");

                if (StageUnlocked >= 3)
                {
                    Stage = 3;
                    if (!StartBtn.activeSelf) //만약 시작하기 버튼이 활성화가 안 되어있으면 활성화 시켜줌
                    {
                        StartBtn.SetActive(true);
                    }
                }
                else
                {
                    if (StartBtn.activeSelf)//만약 활성화가 되어 있으면 비활성화 시켜 시작을 막음
                    {
                        StartBtn.SetActive(false);
                    }
                }
                break;
        }
    }
}
