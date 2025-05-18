using FMODUnity;
using TMPro;
using UnityEngine;

public class SetEventParameters : MonoBehaviour
{
    [SerializeField]
    string _name;

    [SerializeField]
    float _value;

    [SerializeField]
    bool _isLabel = false;

    [SerializeField]
    string _label = "";

    StudioEventEmitter _emitter;

    TextMeshProUGUI _text;

    private void Start()
    {
        _emitter = GetComponentInParent<StudioEventEmitter>();
        _text = FindFirstObjectByType<TextMeshProUGUI>();
    }

    bool _inside = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<FirstPersonController>() != null)
        {
            _inside = true;
            if(!_isLabel) _text.text = _name + ": " + _value;
            else _text.text = _name + ": " + _label;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<FirstPersonController>() != null)
        {
            _inside = false;
            _text.text = "";
        }
    }

    private void Update()
    {
        if (_inside && Input.GetKeyUp(KeyCode.E)) {
            if (!_isLabel) _emitter.EventInstance.setParameterByName(_name, _value);
            else _emitter.EventInstance.setParameterByNameWithLabel(_name, _label);
        }
    }
}
