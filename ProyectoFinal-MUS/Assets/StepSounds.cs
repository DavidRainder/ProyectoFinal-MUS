using FMOD;
using UnityEngine;

public class StepSounds : MonoBehaviour
{
    [SerializeField]
    FMODUnity.StudioEventEmitter _sfxEmitter = null;

    [SerializeField]
    FirstPersonController _firstPersonController = null;

    //Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!_sfxEmitter)
        {
            _sfxEmitter = GetComponent<FMODUnity.StudioEventEmitter>();
            if (!_sfxEmitter)
            {
                UnityEngine.Debug.LogError("No StudioEventEmitter on '" + gameObject.name + "' object");
                return;
            }
        }

        if (!_firstPersonController)
        {
            _firstPersonController = GetComponent<FirstPersonController>();
            if (!_firstPersonController)
            {
                UnityEngine.Debug.LogError("No FirstPersonController on '" + gameObject.name + "' object");
                return;
            }
        }
    }


    bool _playing = false;
    // Update is called once per frame
    void Update()
    {
        if (_firstPersonController.IsWalking() && !_playing)
        {
           _sfxEmitter.Play();
            _playing = true;
        }
        else if (!_firstPersonController.IsWalking() && _playing)
        {
            _sfxEmitter.Stop();
            _playing = false;
        }
    }
}
