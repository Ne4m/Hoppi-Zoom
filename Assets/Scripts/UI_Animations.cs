using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UI_Animations : MonoBehaviour
{

    [SerializeField] private GameObject titleContainer;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject titleBackground;
    [SerializeField] private GameObject fireworks;

    [SerializeField] private GameObject containerMovePoint;
    [SerializeField] private GameObject watchAdContainerMovePoint;
    [SerializeField] private GameObject goldPaidContinueContainerMovePoint;


    [SerializeField] private Button continueButton;
    [SerializeField] private Button watchAdButton;
    [SerializeField] private Button goldPaidContinueButton;

    [SerializeField] private Button goBackMenuButton;

    private Vector3 containerMovePointVecs;

    private Vector3 startPos;

    private (Vector3 FreeContinue, Vector3 PaidContinue) startPosition;

    public float rotateSpeed = 1f;
    private bool isHS;

    private int continueButtonTweenID;

    private Vector3 contLocalScale;

   // [SerializeField] private Sprite[] titleImages;

    [SerializeField] private TMP_Text titleText;

    // 1817

    public static UI_Animations instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (!(watchAdButton is null)) watchAdButton.onClick.AddListener(WatchAdButton_Clicked);
        if (!(continueButton is null)) continueButton.onClick.AddListener(continueButton_Clicked);
        if (!(goldPaidContinueButton is null)) goldPaidContinueButton.onClick.AddListener(goldPaidContinueButton_Clicked);
        if (!(goBackMenuButton is null)) goBackMenuButton.onClick.AddListener(GoBackMenuButton_Clicked);

        containerMovePointVecs = containerMovePoint.GetComponent<RectTransform>().position;

        startPos = titleContainer.GetComponent<RectTransform>().position;

        contLocalScale = continueButton.gameObject.transform.localScale;

        startPosition.FreeContinue = watchAdButton.transform.position;
        startPosition.PaidContinue = goldPaidContinueButton.transform.position;

    }

    private void goldPaidContinueButton_Clicked()
    {

    }

    private void continueButton_Clicked()
    {
        if(LeanTween.isTweening(continueButton.gameObject)) LeanTween.cancel(continueButton.gameObject);

        LeanTween.scale(continueButton.gameObject, new Vector3(contLocalScale.x, contLocalScale.y, contLocalScale.z), 0.65f)
            .setEase(LeanTweenType.easeOutBounce)
            .setIgnoreTimeScale(true);

        watchAdButton.gameObject.LeanMove(watchAdContainerMovePoint.GetComponent<RectTransform>().position, 1.25f)
            .setEase(LeanTweenType.easeOutBounce)
            .setOnComplete(() =>
            {

                if (!watchAdButton.gameObject.LeanIsTweening())
                {
                    var _scale = watchAdButton.transform.localScale;
                    var offset = 0.35f;
                    watchAdButton.gameObject.LeanScale(new Vector3(_scale.x + offset, _scale.y + offset, _scale.z + offset), 1f)
                    .setLoopClamp()
                    .setEaseShake()
                    .setIgnoreTimeScale(true);
                }

            })
            .setIgnoreTimeScale(true);

        goldPaidContinueButton.gameObject.LeanMove(goldPaidContinueContainerMovePoint.GetComponent<RectTransform>().position, 1.25f)
            .setEase(LeanTweenType.easeOutBounce)
            .setOnComplete(() =>
            {
                if (!goldPaidContinueButton.gameObject.LeanIsTweening())
                {
                    var _scale = goldPaidContinueButton.transform.localScale;
                    var offset = 0.35f;
                    goldPaidContinueButton.gameObject.LeanScale(new Vector3(_scale.x + offset, _scale.y + offset, _scale.z + offset), 1f)
                    .setLoopClamp()
                    .setEaseShake()
                    .setIgnoreTimeScale(true);
                }
            })
            .setIgnoreTimeScale(true);




    }

    private void GoBackMenuButton_Clicked()
    {
        AdsManager.instance.IncreaseInterstitialTriggerCounter(1);
        LevelManager.instance.goBackMainMenu();

    }

    private void WatchAdButton_Clicked()
    {
        AdsManager.instance.DisplayRewardedAd();
       
    }

    public void SetAnimations()
    {
        // GREEN CONTINUE BUTTON
        float scaleValue = 0.10f;
        continueButtonTweenID = LeanTween.scale(continueButton.gameObject, new Vector3(contLocalScale.x + scaleValue, contLocalScale.y + scaleValue, contLocalScale.z + scaleValue), 1.25f)
            .setLoopPingPong()
            .setEase(LeanTweenType.easeOutBounce)
            .setIgnoreTimeScale(true)
            .setOnCompleteOnRepeat(true)
            .id;


        // TITLE
        titleContainer.LeanMove(containerMovePointVecs, 1.25f).setEase(LeanTweenType.easeOutBounce).setOnComplete(ActivateFireworks).setIgnoreTimeScale(true);
        title.LeanRotate(new Vector3(0f, 0f, 358f), 0.5f).setLoopPingPong().setRotateLocal().setIgnoreTimeScale(true);

        if (isHS)
        {
            var _scale = title.transform.localScale;
            title.LeanScale(new Vector3(_scale.x + 0.7796129f, _scale.y + 0.7796129f, _scale.z + 0.7796129f), 3f).setLoopPingPong().setEase(LeanTweenType.easeInOutElastic).setIgnoreTimeScale(true);
            LeanTween.rotateAroundLocal(titleBackground, Vector3.back, 360, rotateSpeed).setLoopClamp().setIgnoreTimeScale(true);
        }
        

    }

    public void ResetContinueButtons()
    {
   
        if (LeanTween.isTweening(watchAdButton.gameObject))
        {
            LeanTween.cancel(watchAdButton.gameObject);
        }

        if (LeanTween.isTweening(goldPaidContinueButton.gameObject))
        {
            LeanTween.cancel(goldPaidContinueButton.gameObject);
        }


        watchAdButton.transform.position = startPosition.FreeContinue;
        goldPaidContinueButton.transform.position = startPosition.PaidContinue;
     
    }

    public void ResetAnimations()
    {
        titleContainer.transform.position = startPos; // (startPos, 0.1f).setOnComplete(DeactivateFireworks).setIgnoreTimeScale(true);
        DeactivateFireworks();
        ResetContinueButtons();
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
            // title.GetComponent<Image>().sprite = titleImages[0];
            titleText.text = "NEW HIGH\nSCORE!!!";
            return;
        }

        // title.GetComponent<Image>().sprite = titleImages[1];
        titleText.text = "you got\nZOOM'd";
    }

    public void SetHighScoreState(bool hs)
    {
        isHS = hs;
    }


}
