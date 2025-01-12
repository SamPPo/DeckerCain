using UnityEngine;
using Decker;
using System.Collections;

public class Card_sc : MonoBehaviour
{
    public void MoveCardToTransform(DTransform trans, float time)
    {
        StartCoroutine(MoveToTransform(trans, time));
    }

    private IEnumerator MoveToTransform(DTransform target, float duration)
    {
        transform.GetPositionAndRotation(out Vector3 initialPosition, out Quaternion initialRotation);
        Vector3 initialScale = transform.localScale;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.SetPositionAndRotation(Vector3.Lerp(initialPosition, target.position, elapsedTime / duration), Quaternion.Lerp(initialRotation, target.rotation, elapsedTime / duration));
            transform.localScale = Vector3.Lerp(initialScale, target.scale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position, rotation, and scale are set
        transform.SetPositionAndRotation(target.position, target.rotation);
        transform.localScale = target.scale;
    }
}
