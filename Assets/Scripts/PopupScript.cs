using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PopupScript : MonoBehaviour
{
    private RectTransform rectTransform;
    [Range(0f, 1f), SerializeField]
    private float closeF;
    [Range(0f, 1f), SerializeField]
    private float openF;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Open()
    {
        Vector3 newPos = transform.position;
        newPos.y += rectTransform.sizeDelta.y;
        transform.DOMove(newPos, 0.5f)
            .SetEase(Ease.OutSine);
    }

    public void Close()
    {
        Vector3 newPos = transform.position;
        newPos.y -= rectTransform.sizeDelta.y;
        transform.DOMove(newPos, 0.65f)
            .SetEase(Ease.OutSine);
    }
}
