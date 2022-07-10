using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Animations : MonoBehaviour
{

    [SerializeField] private GameObject titleContainer;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject titleBackground;
    [SerializeField] private GameObject fireworks;

    private Vector3 startPos;
    public float rotateSpeed = 1f;
    private bool isHS;

    [SerializeField] private Sprite[] titleImages;

    // 1817

    public static UI_Animations instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {


        startPos = titleContainer.GetComponent<RectTransform>().position;
       // 
    }


    public void SetAnimations()
    {
        titleContainer.LeanMove(new Vector3(720, 2350, 0), 1.25f).setEase(LeanTweenType.easeOutBounce).setOnComplete(ActivateFireworks).setIgnoreTimeScale(true);
        title.LeanRotate(new Vector3(0f, 0f, 358f), 0.5f).setLoopPingPong().setRotateLocal().setIgnoreTimeScale(true);

        if (isHS)
        {
            title.LeanScale(new Vector3(0.7796129f, 0.7796129f, 0.7796129f), 3f).setLoopPingPong().setEase(LeanTweenType.easeInOutElastic).setIgnoreTimeScale(true);
            LeanTween.rotateAroundLocal(titleBackground, Vector3.back, 360, rotateSpeed).setLoopClamp().setIgnoreTimeScale(true);
        }
        

    }

    public void ResetAnimations()
    {
        titleContainer.LeanMove(startPos, 0.1f).setOnComplete(DeactivateFireworks).setIgnoreTimeScale(true);
    }

    public void ActivateFireworks()
    {
        if(isHS) fireworks.gameObject.SetActive(true);
    }

    public void DeactivateFireworks()
    {
        fireworks.gameObject.SetActive(false);
    }

    public void SwitchTitleImage()
    {
        if (isHS)
        {
            title.GetComponent<Image>().sprite = titleImages[0];
            return;
        }

        title.GetComponent<Image>().sprite = titleImages[1];

    }

    public void SetHighScoreState(bool hs)
    {
        isHS = hs;
    }


}
