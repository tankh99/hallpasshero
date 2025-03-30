using UnityEngine;

public class BackgroundZoom : MonoBehaviour
{
    public RectTransform background;
    public float zoomTime = 20f;
    public float zoomScale = 1.5f;

    private float elapsedTime = 0f;
    private bool zoomingIn = true;
    private Vector3 originalScale;

    void Start()
    {
        if (background == null)
            background = GetComponent<RectTransform>();

        originalScale = background.localScale;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / zoomTime;

        if (zoomingIn)
        {
            background.localScale = Vector3.Lerp(originalScale, originalScale * zoomScale, t);
        }
        else
        {
            background.localScale = Vector3.Lerp(originalScale * zoomScale, originalScale, t);
        }

        if (t >= 1f)
        {
            elapsedTime = 0f;
            zoomingIn = !zoomingIn;
        }
    }
}
