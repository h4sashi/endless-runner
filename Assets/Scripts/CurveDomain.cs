
using System.Collections;
using UnityEngine;

public class CurveDomain : MonoBehaviour
{
    [Range(-0.1f, 0.1f)]
    public float curveStrength;

    public float transitionDuration = 2f; // Adjust the duration of the transition

    private WorldCurver worldCurver;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Debug.Log("Player collided with "+gameObject.name);
            worldCurver = GameObject.FindObjectOfType<WorldCurver>();
            StartCoroutine(TransitionCurveStrength(worldCurver, curveStrength, transitionDuration));
        }
    }

    private IEnumerator TransitionCurveStrength(WorldCurver targetWorldCurver, float targetCurveStrength, float duration)
    {
        float elapsedTime = 0f;
        float startCurveStrength = targetWorldCurver.curveStrength;

        while (elapsedTime < duration)
        {
            // Interpolate between the start and target curveStrength values
            targetWorldCurver.curveStrength = Mathf.Lerp(startCurveStrength, targetCurveStrength, elapsedTime / duration);

            // Wait for the next frame
            yield return null;

            // Update the elapsed time
            elapsedTime += Time.deltaTime;
        }

        // Ensure the target curveStrength is set to the final value
        targetWorldCurver.curveStrength = targetCurveStrength;
    }
}
