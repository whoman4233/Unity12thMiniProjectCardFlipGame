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

    public GameObject LockTxt;

    public int StageUnlocked = 0;
    public int Stage = 1;
    int Hidden = 0;

    private void Awake()
    {
        Instance = this;
        StageUnlocked = PlayerPrefs.GetInt("StageUnlocked", 1);
    }

    void Start()
    {

    }

    public void Choose(GameObject clickButton)
    {
        Image spriteName = CenterImage.GetComponent<Image>();

        if (clickButton.name == "StageImage" && spriteName.sprite.name == "membercard7")
        {
            Debug.Log("test");
            Hidden += 1;
            if (Hidden >= 3)
            {
                Stage = 99;
                CenterImage.sprite = Resources.Load<Sprite>("rtan6");
                StartBtn.SetActive(true);
            }
        }
        else
        {
            switch (clickButton.name)
            {
                case "EasyBtn":
                    CenterImage.sprite = Resources.Load<Sprite>("membercard2");
                    Stage = 1;
                    if (!StartBtn.activeSelf) //만약 시작하기 버튼이 활성화가 안 되어있으면 활성화 시켜줌
                    {
                        LockTxt.SetActive(false);
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
                            LockTxt.SetActive(false);
                            StartBtn.SetActive(true);
                        }
                    }
                    else
                    {
                        LockTxt.SetActive(true);
                        if (StartBtn.activeSelf)//만약 활성화가 되어 있으면 비활성화 시켜 시작을 막음
                        {
                            StartBtn.SetActive(false);
                        }
                        Invoke("HideLockTxt", 1f);
                    }
                    break;

                case "HardBtn":
                    CenterImage.sprite = Resources.Load<Sprite>("membercard10");
                    Debug.Log(StageUnlocked);
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
                        LockTxt.SetActive(true);
                        if (StartBtn.activeSelf)//만약 활성화가 되어 있으면 비활성화 시켜 시작을 막음
                        {
                            StartBtn.SetActive(false);
                        }
                        Invoke("HideLockTxt", 1f);
                    }
                    break;
            }
        }
    }

    public void HideLockTxt()
    {
        LockTxt.SetActive(false);
    }
}
