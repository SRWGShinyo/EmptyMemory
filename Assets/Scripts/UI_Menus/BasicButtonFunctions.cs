using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BasicButtonFunctions : MonoBehaviour
{
    public CanvasGroup panel;

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Open()
    {
        EventSystem.current.SetSelectedGameObject(null);
        panel.blocksRaycasts = true;
        panel.interactable = true;

        StartCoroutine(Appear(true));
    }

    public void Close()
    {
        panel.blocksRaycasts = false;
        panel.interactable = false;

        StartCoroutine(Appear(false));
    }

    private IEnumerator Appear(bool appaerOrnot)
    {
        if (appaerOrnot)
        {
            while (panel.alpha < 1)
            {
                panel.alpha += 0.01f;
                yield return new WaitForSeconds(0.01f);
            }
        }

        else
        {
            while (panel.alpha > 0)
            {
                panel.alpha -= 0.01f;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
