using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearAllProperly : MonoBehaviour
{

    public List<CanvasGroup> groups = new List<CanvasGroup>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(appearAll());
    }

    private IEnumerator appearAll()
    {
        foreach (CanvasGroup cg in groups)
        {
            while (cg.alpha < 1)
            {
                cg.alpha += 0.01f;
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.6f);
        }

    }

}
