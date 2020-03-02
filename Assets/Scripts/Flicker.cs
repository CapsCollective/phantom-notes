using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    public GameObject optGameObject;
    public List<Renderer> renderers;

    public Vector2 delayBetweenFlickers, flickerCount, flickerSpeed;

    public bool invertFlicker = false;

    public bool useDelayTimeBrackets = false;
    public List<Vector2> delayTimeBrackets;


    private void Start()
    {
        if (renderers.Count == 0)
            renderers.Add(GetComponent<Renderer>());


        StartCoroutine(FlickerRun());
    }


    public IEnumerator FlickerRun()
    {
        if (useDelayTimeBrackets)
        {
            Vector2 pickTimeBracket = delayTimeBrackets[Random.Range(0, delayTimeBrackets.Count)];
            float timeDelay = RoundTo(0.25f, Random.Range(pickTimeBracket.x, pickTimeBracket.y));
            yield return new WaitForSeconds(timeDelay);
        }
        else
        {
            yield return new WaitForSeconds(RoundTo(0.25f, Random.Range(delayBetweenFlickers.x, delayBetweenFlickers.y)));
        }

        StartCoroutine(DoFlicker());

        StartCoroutine(FlickerRun());
    }

    IEnumerator DoFlicker ()
    {
        float flickers = Random.Range(flickerCount.x, flickerCount.y);
        for (int i = 0; i < flickers; i++)
        {
            SetRenderer(false);
            yield return new WaitForSeconds(Random.Range(flickerSpeed.x, flickerSpeed.y));
            SetRenderer(true);
            yield return new WaitForSeconds(Random.Range(flickerSpeed.x, flickerSpeed.y));
        }
    }

    void SetRenderer (bool _state)
    {
        if (invertFlicker)
            _state = !_state;

        if (optGameObject != null)
            optGameObject.SetActive(_state);
        foreach (Renderer renderer in renderers)
            renderer.enabled = _state;
    }

    public void End ()
    {
        SetRenderer(true);
    }

    private float RoundTo (float _interval, float _value)
    {
        return Mathf.Round(_value / _interval) * _interval;
    }
}
