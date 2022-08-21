using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject showOnHover;

    private void Start()
    {
        showOnHover.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        showOnHover.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        showOnHover.SetActive(false);
    }
}
