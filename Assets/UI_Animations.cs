using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Animations : MonoBehaviour
{

    [SerializeField] private GameObject titleContainer;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject titleBackground;
    [SerializeField] private GameObject fireworks;
    [SerializeField] private GameObject containerMovePoint;

    private Vector3 containerMovePointVecs;

    private Vector3 startPos;
    public float rotateSpeed = 1f;
    private bool isHS;

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

        containerMovePointVecs = containerMovePoint.GetComponent<RectTransform>().position;

        startPos = titleContainer.GetComponent<RectTransform>().position;
       // 
    }


    public void SetAnimations()
    {
        titleContainer.LeanMove(containerMovePointVecs, 1.25f).setEase(LeanTweenType.easeOutBounce).setOnComplete(ActivateFireworks).setIgnoreTimeScale(true);
        title.LeanRotate(new Vector3(0f, 0f, 358f), 0.5f).setLoopPingPong().setRotateLocal().setIgnoreTimeScale(true);

        if (isHS)
        {
            var _scale = title.transform.localScale;
            title.LeanScale(new Vector3(_scale.x + 0.7796129f, _scale.y + 0.7796129f, _scale.z + 0.7796129f), 3f).setLoopPingPong().setEase(LeanTweenType.easeInOutElastic).setIgnoreTimeScale(true);
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
