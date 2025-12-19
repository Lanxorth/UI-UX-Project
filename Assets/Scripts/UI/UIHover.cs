using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIHoverEffect : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    public Color normalColor = Color.white;
    public Color hoverColor = Color.yellow;
    public float hoverScale = 1.1f;

    Image image;
    Vector3 baseScale;

    void Awake()
    {
        image = GetComponent<Image>();
        baseScale = transform.localScale;
        image.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = hoverColor;
        transform.localScale = baseScale * hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = normalColor;
        transform.localScale = baseScale;
    }
}
