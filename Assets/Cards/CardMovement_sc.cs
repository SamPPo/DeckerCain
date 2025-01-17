using UnityEngine;
using Decker;
using System.Collections;

public class CardMovements_sc : MonoBehaviour
{
    public delegate void MovementCompletedDelegate();
    public static MovementCompletedDelegate movementCompleted;

    public void MoveCardToTransform(DTransform trans, float time)
    {
        StartCoroutine(MoveToTransform(trans, time));
    }

    private IEnumerator MoveToTransform(DTransform target, float duration)
    {
        transform.GetPositionAndRotation(out Vector3 initialPosition, out Quaternion initialRotation);
        Vector3 initialScale = transform.localScale;

        float elapsedTime = 0f;
        float height = 2.0f; // Adjust this value to control the height of the arc

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            Vector3 currentPosition = Vector3.Lerp(initialPosition, target.position, t);
            currentPosition.y += height * Mathf.Sin(Mathf.PI * t); // Create an arc

            transform.SetPositionAndRotation(currentPosition, Quaternion.Lerp(initialRotation, target.rotation, t));
            transform.localScale = Vector3.Lerp(initialScale, target.scale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position, rotation, and scale are set
        transform.SetPositionAndRotation(target.position, target.rotation);
        transform.localScale = target.scale;

        //Invoke movement completed delegate
        movementCompleted?.Invoke();
    }
}
