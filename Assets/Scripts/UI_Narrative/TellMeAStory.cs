using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TellMeAStory : MonoBehaviour
{
    public CanvasGroup whole;
    public TextMeshProUGUI tmpro;

    public List<string> narrative;

    string storedString;
    bool isTalking;

    private void Start()
    {
        StartTalking();
    }

    public void StartTalking()
    {
        StartCoroutine(makeAppearAndSpeak());
    }
    public void StopTalking()
    {
        StartCoroutine(makeDisappearAndLoad());
    }

    public void proceedToNext()
    {
        StopCoroutine("printText");

        if (isTalking)
        {
            tmpro.text = storedString;
            isTalking = false;
        }

        else if (narrative.Count > 0)
        {
            tmpro.text = "";
            storedString = "";
            string speak = narrative[0];
            narrative.RemoveAt(0);
            StartCoroutine(printText(speak));
        }

        else
        {
            StopTalking();
        }
    }

    private IEnumerator printText(string toPrint)
    {
        isTalking = true;
        storedString = toPrint;
        foreach(char c in toPrint)
        {
            if (!isTalking)
                break;

            tmpro.text += c;
            yield return new WaitForSeconds(0.05f);
        }
        isTalking = false;
    }

    private IEnumerator makeAppearAndSpeak()
    {
        while (whole.alpha < 1)
        {
            whole.alpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        string speak = narrative[0];
        narrative.RemoveAt(0);
        StartCoroutine(printText(speak));
    }

    private IEnumerator makeDisappearAndLoad()
    {
        while (whole.alpha > 0)
        {
            whole.alpha -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        FindObjectOfType<LevelLoader>().LoadNextLevel();
    }
}
