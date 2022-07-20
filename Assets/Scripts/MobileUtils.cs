using UnityEngine;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine.Rendering;

public class MobileUtils : MonoBehaviour
{

    private int FramesPerSec;
    private float frequency = 1.0f;
    private string fps;

    public TMP_Text debugtxt;


    void Start()
    {
        StartCoroutine(FPS());

        //var pacing = PlayerSettings.Android.optimizedFramePacing;

        //if (Application.platform == RuntimePlatform.Android)
        //{
        //    Application.targetFrameRate = 60;
        //}
        //else
        //{
        //    Application.targetFrameRate = 144;

        //}

       // Debug.Log($"Screen Refresh {Screen.currentResolution.refreshRate}");

    }

    private IEnumerator FPS()
    {
        for (; ; )
        {
            if (Application.platform == RuntimePlatform.Android)
            {
            }
            if (QualitySettings.vSyncCount > 0) QualitySettings.vSyncCount = 0;
            if (Application.targetFrameRate != Screen.currentResolution.refreshRate)
            {
                Application.targetFrameRate = Screen.currentResolution.refreshRate;

            }

            if (LevelManager.instance.playerControl.isPlayerDead())
            {
                Debug.Log("Player Is DEAD!");
                OnDemandRendering.renderFrameInterval = 2;
            }
            else
            {
                OnDemandRendering.renderFrameInterval = 1;
            }




            // Capture frame-per-second
            int lastFrameCount = Time.frameCount;
            float lastTime = Time.realtimeSinceStartup;
            yield return new WaitForSeconds(frequency);
            float timeSpan = Time.realtimeSinceStartup - lastTime;
            int frameCount = Time.frameCount - lastFrameCount;

            // Display it

            fps = string.Format("FPS: {0}", Mathf.RoundToInt(frameCount / timeSpan));

            debugtxt.text = fps;
        }
    }



}