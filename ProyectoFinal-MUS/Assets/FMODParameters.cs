using FMODUnity;
using System;
using System.Collections;
using UnityEngine;

public class FMODParameters : MonoBehaviour
{
    [SerializeField]
    string[] _parameters;

    StudioEventEmitter _emitter = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(miau());
    }

    IEnumerator miau()
    {
        yield return new WaitForSeconds(1.0f);
        _emitter = GetComponent<StudioEventEmitter>();
        FMOD.RESULT a = _emitter.EventInstance.setParameterByName(_parameters[0], 3.0f);
        Debug.Log(a);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
