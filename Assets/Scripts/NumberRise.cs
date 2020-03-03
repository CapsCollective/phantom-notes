using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberRise : MonoBehaviour
{

    public List<TextMesh> texts;

    public void Setup (float _value)
    {
        foreach (TextMesh txt in texts)
            txt.text = _value+"";
    }

    public void RunEffect(float _height)
    {
        StartCoroutine(RiseEffect(_height));
    }


    private IEnumerator RiseEffect (float _height)
    {
        float timeElapsed = 0;
        float duration = Random.Range(0.5f, 1);
        float fraction = 0;
        Vector3 start = transform.position;
        Vector3 end = transform.position + new Vector3(0, _height, 0);

        transform.position = Vector3.Lerp(start, end, 0);

        while (timeElapsed < duration)
        {
            fraction = timeElapsed / duration;

            transform.position = Vector3.Lerp(start, end, fraction);
            timeElapsed += Time.deltaTime;
            yield return null;

        }


        transform.position = Vector3.Lerp(start, end, 1);
        GameObject.Destroy(gameObject);
    }


}
