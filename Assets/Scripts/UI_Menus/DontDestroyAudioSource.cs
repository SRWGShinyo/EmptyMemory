using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyAudioSource : MonoBehaviour
{
    public List<int> scenes;
    AudioSource source;

    public float fadeInto;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnLevelWasLoaded(int level)
    {
        if (!scenes.Contains(SceneManager.GetActiveScene().buildIndex))
        {
            StartCoroutine(fadeOut());
        }
        else
        {
            StartCoroutine(fadeInTo());
        }
    }

    private IEnumerator fadeInTo()
    {
        source.mute = false;
        while (source.volume < fadeInto)
        {
            source.volume += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator fadeOut()
    {
        while(source.volume > 0)
        {
            source.volume -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        source.mute = true;
    }
}
