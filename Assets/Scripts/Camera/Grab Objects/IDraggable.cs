using UnityEngine;

public interface IDraggable
{
    public void Grab(Vector3 position);
    public void Release(Vector3 position);
    public void Drag(Vector3 position);
}
