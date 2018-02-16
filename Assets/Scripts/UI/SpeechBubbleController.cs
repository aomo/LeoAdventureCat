﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubbleController : MonoBehaviour {

    public GameObject bubbleObj, textObj;
    public float fadeOutTime = .5f;
    Text text;
    CanvasGroup cg;

	void Start () {
        text = textObj.GetComponent<Text>();
        cg = GetComponent<CanvasGroup>();
        cg.alpha = 1f;
        Debug.Log("started text=" + text);
	}
	
	void Update () {
        // we leave the text in the direction of reading
        if (this.transform.lossyScale.x < 0)
            this.transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
	}

    public void SetText(string textId, float time = 5f)
    {
        gameObject.SetActive(true);
        if(text==null)
            Start();
        Debug.Log("setText() before=" + text+ "local="+LocalizationManager.Instance);
        this.text.text = LocalizationManager.Instance.GetText(textId);
        Debug.Log("setText() after=" + text);
        DisplayBubble();
        StopAllCoroutines();
        StartCoroutine(WaitThenFadeOut(time));
    }

    IEnumerator WaitThenFadeOut(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float alpha;
        for (float t = 0.0f; t <= 0.5f; t += Time.deltaTime)
        {
            alpha = Mathf.Lerp(1, 0, t * 2);
            cg.alpha = alpha;
            yield return null;
        }
        cg.alpha = 1f;
        gameObject.SetActive(false);
        yield return null;
    }

    void DisplayBubble()
    {
        cg.alpha = 1f;
        gameObject.SetActive(true);
    }
}