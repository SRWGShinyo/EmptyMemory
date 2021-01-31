using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamHandler : MonoBehaviour
{
    static Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Shake()
    {
        StartCoroutine(ShakeC());
    }

    private IEnumerator ShakeC()
    {
        anim.Play("CameraShake");
        yield return new WaitForSeconds(0.2f);
        anim.Play("IdleCam");
    }
}
