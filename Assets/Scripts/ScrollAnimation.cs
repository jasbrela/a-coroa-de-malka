using UnityEngine;

public class ScrollAnimation : MonoBehaviour
{
    [SerializeField] private Transform handle;
    [SerializeField] private int multiplier;
    
    public void Animate(Vector2 vec)
    {
        handle.rotation = Quaternion.Euler(Vector3.forward * vec.y * multiplier);
    }
}
