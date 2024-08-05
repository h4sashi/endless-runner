using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{


    public IEnumerator Shake(float duration, float magnitude){
        
        Vector3 originalPos = Camera.main.transform.localPosition;

        float elapsed = 0.0f;

        while(elapsed < duration){
            float x = Random.Range(-6f, 12f) * magnitude;
            float y = Random.Range(-6f, 8f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;

        }

        transform.localPosition = originalPos;
        Debug.Log("Camera Shaked");


    }






}
