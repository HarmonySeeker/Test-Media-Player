using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PopupScript : MonoBehaviour
{
    private RectTransform rectTransform;

    [SerializeField]
    private Button closeButton;
    [SerializeField]
    private Button openButton;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        openButton.onClick.AddListener(Open);
        closeButton.onClick.AddListener(Close);
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
