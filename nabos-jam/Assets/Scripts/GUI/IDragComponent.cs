using UnityEngine;

public interface IDragComponent<T>
{
    void StartDrag(T elementToDrag);

    void UpdateDrag(Vector2 mousePos);

    void StopDrag();
}  
