using UnityEngine;

public class LampSwitch : MonoBehaviour
{
    [SerializeField]
    Light _light = null;

    [SerializeField]
    FMODUnity.StudioEventEmitter _sfxEmitter = null;

    [SerializeField]
    bool _lightOnAwake = false;

    private void Start()
    {
        if (!_light)
        {
            _light = GetComponentInChildren<Light>();
            if (!_light) { 
                Debug.LogError("No light on '" + gameObject.name + "' object");
                return;
            }
        }

        if (!_sfxEmitter)
        {
            _sfxEmitter = GetComponent<FMODUnity.StudioEventEmitter>();
            if (!_sfxEmitter)
            {
                Debug.LogError("No StudioEventEmitter on '" + gameObject.name + "' object");
                return;
            }
        }

        if (_lightOnAwake) TurnLightOn(true);
        else TurnLightOff(false);
    }

    public void TurnLightOn(bool makeSound)
    {
        if (_light.enabled) return;

        if(makeSound)
        {
            _sfxEmitter?.Play();
        }

        _light.enabled = true;
    }


    public void TurnLightOff(bool makeSound)
    {
        if (!_light.enabled) return;

        if (makeSound)
        {
            _sfxEmitter?.Play();
        }

        _light.enabled = false;
    }

    bool aux = false;
    private void Update()
    {
        // BORRAR

        if (Input.GetKeyUp(KeyCode.Space))
        {
            aux = !aux;
            if (aux) TurnLightOn(true);
            else TurnLightOff(true);
        }
    }
}
