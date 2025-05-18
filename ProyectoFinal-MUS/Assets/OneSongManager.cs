using FMODUnity;
using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class OneSongManager : MonoBehaviour
{
    List<FMOD.DSP> _dsps = new List<FMOD.DSP>();

    [SerializeField]
    string[] _returnBusGUIDs;

    FMOD.GUID[] _guids;

    StudioEventEmitter _emitter;

    [SerializeField]
    Light[] _lights;

    [SerializeField]
    float _lightIntesityFactor = 3.0f;

    [SerializeField]
    float _minimumIntensity = 1.5f;

    float[] _baseLightIntensities;

    private void Start()
    {
        FMOD.System core;

        core = RuntimeManager.CoreSystem;

        _guids = new FMOD.GUID[_returnBusGUIDs.Length];
        for (int i = 0; i < _guids.Length; ++i)
        {
            _returnBusGUIDs[i].Trim('{');
            _returnBusGUIDs[i].Trim('}');
            _guids[i] = new FMOD.GUID(new Guid(_returnBusGUIDs[i]));
        }

        foreach (Light light in _lights)
        {
            core.createDSPByType(FMOD.DSP_TYPE.LOUDNESS_METER, out FMOD.DSP meteringDsp);
            _dsps.Add(meteringDsp);
        }

        _emitter = GetComponent<StudioEventEmitter>();
        _emitter.Play();
        RuntimeManager.AttachInstanceToGameObject(_emitter.EventInstance, gameObject);
        StartCoroutine(AddDSPS_Delayed());

        _baseLightIntensities = new float[_lights.Length];

        for(int i = 0; i < _baseLightIntensities.Length; ++i)
        {
            _baseLightIntensities[i] = _lights[i].intensity;
        }
    }

    IEnumerator AddDSPS_Delayed()
    {
        for (int i = 0; i < _dsps.Count; ++i)
        {
            FMOD.RESULT res= RuntimeManager.StudioSystem.getBusByID(_guids[i], out FMOD.Studio.Bus bus);
            bus.lockChannelGroup();
            RuntimeManager.StudioSystem.update();
            yield return new WaitForEndOfFrame();
            bus.getChannelGroup(out FMOD.ChannelGroup instanceGroup);

            _emitter.EventInstance.getChannelGroup(out FMOD.ChannelGroup instanceGroup_miau);

            _dsps[i].setMeteringEnabled(false, true);

            instanceGroup.addDSP(FMOD.CHANNELCONTROL_DSP_INDEX.TAIL, _dsps[i]);
        }
        RuntimeManager.StudioSystem.update();

    }

    private void Update()
    {
        for (int i = 0; i < _dsps.Count; ++i)
        {
            if (_dsps[i].hasHandle())
            {
                _dsps[i].getMeteringInfo(IntPtr.Zero, out FMOD.DSP_METERING_INFO _meteringInfo);

                if (_meteringInfo.numchannels > 0)
                {
                    float rms = _meteringInfo.rmslevel[0];   // RMS of Left channel

                    _lights[i].intensity = Mathf.Clamp(_baseLightIntensities[i] * rms * _lightIntesityFactor, _minimumIntensity, float.MaxValue);
                }
                else {
                    _lights[i].intensity = 0.0f;
                }
            }
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _dsps.Count; ++i)
        {
            if (_dsps[i].hasHandle()) _dsps[i].release();
        }
        _dsps.Clear();
    }
}
