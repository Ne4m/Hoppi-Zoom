using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIMessager : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Text text2;

    public Vector3 startPos;
    public float speed;
    public float duration;
    public string messageText;

    bool isRunning = false;
    Coroutine msgCoroutine;

    private void Start()
    {


    }

    public void startMsg(string msg, float duration, Vector3 pos)
    {

        if(msgCoroutine != null && isRunning)
        {
            StopCoroutine(msgCoroutine);
        }

        msgCoroutine = StartCoroutine(CreateMessage(msg, duration, pos));
    }

    public IEnumerator CreateMessage(string msg, float duration, Vector3 pos)
    {
        isRunning = true;

        if (pos != Vector3.zero)
        {
            transform.position = pos;
        }
        
        text.gameObject.SetActive(true);
        text.text = msg;

        //rb.velocity = Vector2.down * speed;

        yield return new WaitForSeconds(duration);

        text.text = string.Empty;
        text.gameObject.SetActive(false);

        isRunning = false;
    }


    public void startMsgv2(string msg, float duration, Vector3 pos, Color color)
    {

        if (msgCoroutine != null && isRunning)
        {
            StopCoroutine(msgCoroutine);
        }

        msgCoroutine = StartCoroutine(CreateMessagev2(msg, duration, pos, color));
    }

    public IEnumerator CreateMessagev2(string msg, float duration, Vector3 pos, Color color)
    {
        isRunning = true;

        if (pos != Vector3.zero)
        {
            transform.position = pos;
        }

        text2.gameObject.SetActive(true);
        text2.text = msg;
        text2.color = color;

        //rb.velocity = Vector2.down * speed;

        yield return new WaitForSeconds(duration);

        text2.text = string.Empty;
        text2.gameObject.SetActive(false);

        isRunning = false;
    }

}
