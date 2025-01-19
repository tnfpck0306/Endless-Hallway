using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerShack : MonoBehaviour
{
    [SerializeField] private float shakeTime = 0.6f;
    [SerializeField] private float shakeSpeed = 1.2f;
    [SerializeField] private float shakeAmount = 0.5f;

    private bool check = false;
    void Update()
    {
        if (!check)
            StartCoroutine(Shack());
    }

    IEnumerator Shack()
    {
        check = true;
        
        Vector3 originalPosition = transform.localPosition;
        float elapsedTime = 0.0f;

        while (elapsedTime < shakeTime)
        {
            Vector3 randomPoint = originalPosition + Random.insideUnitSphere * shakeAmount;
            transform.localPosition = Vector3.Lerp(transform.localPosition, randomPoint, Time.deltaTime * shakeSpeed);

            yield return null;

            elapsedTime += Time.deltaTime;
        }

        transform.localPosition = originalPosition;

        yield return new WaitForSeconds(0.5f);

        check = false;
    }
}
