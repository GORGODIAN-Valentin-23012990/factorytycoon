using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    public Image overlay;
    public float flashIn = 0.05f;
    public float flashHold = 0.10f;
    public float flashOut = 0.25f;
    public float maxAlpha = 0.55f;

    Coroutine routine;

    public void FlashRed()
    {
        if (!overlay) return;
        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        yield return Fade(0f, maxAlpha, flashIn);
        yield return new WaitForSeconds(flashHold);
        yield return Fade(maxAlpha, 0f, flashOut);
        routine = null;
    }

    IEnumerator Fade(float a, float b, float t)
    {
        float time = 0f;
        Color c = overlay.color;

        while (time < t)
        {
            time += Time.unscaledDeltaTime;
            float k = t <= 0.0001f ? 1f : Mathf.Clamp01(time / t);
            c.a = Mathf.Lerp(a, b, k);
            overlay.color = c;
            yield return null;
        }
        c.a = b;
        overlay.color = c;
    }
}