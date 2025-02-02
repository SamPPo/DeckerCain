using UnityEngine;
using Decker;
using System.Collections;
using UnityEngine.UIElements;

public class CardMovements_sc : MonoBehaviour
{
    public delegate void MovementCompletedDelegate();
    public static MovementCompletedDelegate movementCompleted;
    private bool destroyAfter;

    public void MoveCardToTransform(DTransform trans, float time, float delay = 0f, bool destroy = false)
    {
        destroyAfter = destroy;
        StartCoroutine(EMoveCardToTransform(trans, time, delay));
    }

    private IEnumerator EMoveCardToTransform(DTransform trans, float time, float delay = 0f)
    {
        delay = Mathf.Clamp(delay, 0f, 0.9f);
        float delayTime = delay * time;
        float moveTime = time - delayTime;
        if (delay>0f)
        {
            yield return new WaitForSeconds(delayTime);
        }
        StartCoroutine(MoveToTransform(trans, moveTime));
    }

    private IEnumerator MoveToTransform(DTransform target, float duration)
    {
        Debug.Log("CardMovements_sc: Moving to transform! " + duration);
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

        // Invoke movement completed delegate
        Debug.Log("CardMovements_sc: Movement completed!");
        movementCompleted?.Invoke();

        if (destroyAfter)
        {
            Debug.Log("CardMovements_sc: Destroying card!");
            Destroy(gameObject);
        }
    }
}
