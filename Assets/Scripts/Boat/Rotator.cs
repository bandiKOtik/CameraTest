using UnityEngine;

public class Rotator
{
    public void Rotate(Transform transform, Vector3 direction, float speed)
    {
        Quaternion rotationWithOffset = transform.rotation * Quaternion.Euler(direction);

        float smoothedSpeed = Time.deltaTime * speed;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationWithOffset, smoothedSpeed);
    }

    public void Rotate(Transform transform, Vector3 direction, float speed, Transform target, float maxAngle)
    {
        float nextAngle = Quaternion.Angle(transform.rotation * Quaternion.Euler(direction), target.rotation);

        if (nextAngle > maxAngle / 2)
            return;

        Quaternion rotationWithOffset = transform.rotation * Quaternion.Euler(direction);

        float smoothedSpeed = Time.deltaTime * speed;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationWithOffset, smoothedSpeed);
    }
}
