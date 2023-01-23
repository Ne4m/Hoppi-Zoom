using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointAnimation : MonoBehaviour
{
    [SerializeField] private bool isLoop = false;
    void Start()
    {
        var initialScale = gameObject.transform.localScale;

        LeanTween.scale(gameObject, initialScale * 2, isLoop ? 0.50f : 0.3f).setOnComplete(() =>
        {
            Destroy(gameObject);
            LeanTween.cancel(gameObject);
        }).setLoopType(isLoop ? LeanTweenType.notUsed : LeanTweenType.once);
    }


    void Update()
    {
        
    }
}
