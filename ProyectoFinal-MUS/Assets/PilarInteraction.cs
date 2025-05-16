using System.Collections;
using UnityEngine;

public class PilarInteraction : Interactable
{
    Animator _animator;

    [SerializeField]
    FMODUnity.StudioEventEmitter _sfxEmitter = null;

    private void Start()
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

        _animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        StartCoroutine(PillarActivation());
    }

    IEnumerator PillarActivation()
    {
        _animator?.SetTrigger("Activate");
        _sfxEmitter.Play();

        // Esperamos a que el evento deje de sonar
        while (_sfxEmitter.IsPlaying())
        {
            yield return new WaitForEndOfFrame();
        }

        _animator?.SetTrigger("Deactivate");
    }
}
