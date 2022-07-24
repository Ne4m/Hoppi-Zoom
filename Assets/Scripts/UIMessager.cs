using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMessager : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

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

}
