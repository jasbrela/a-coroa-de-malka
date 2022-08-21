using UnityEngine;
using UnityEngine.UI;

public class SpeedButton : MonoBehaviour
{
    [SerializeField] private Image image;
    
    [Space(10)] [Header("Sprites")]
    [SerializeField] private Sprite fast;
    [SerializeField] private Sprite faster;
    [SerializeField] private Sprite fastest;


    private void Start()
    {
        image.sprite = fast;
    }

    public void ToggleSpeed()
    {
        if (image.sprite == fast)
        {
            image.sprite = faster;
        }
        else if (image.sprite == faster)
        {
            image.sprite = fastest;
        }
        else if (image.sprite == fastest)
        {
            image.sprite = fast;
        }
    }
}
