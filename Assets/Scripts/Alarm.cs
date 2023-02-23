using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _recoveryRateSoundOn;
    [SerializeField] private float _recoveryRateSoundOff;
    [SerializeField] private float _maxSoundVolume;

    private readonly float _minSoundVolume = 0;
    private Coroutine _changeVolumeCoroutine;

    private void Awake()
    {
        Mathf.Clamp01(_maxSoundVolume);

        _audio.volume = _minSoundVolume;
    }

    public void StartAlarm()
    {
        StartChangingVolume(_maxSoundVolume);
    }

    public void StopAlarm()
    {
        StartChangingVolume(_minSoundVolume);
    }

    private void SoundVolumeMoveTowards(float targetSoundVolume, float recoveryRate)
    {
        _audio.volume = Mathf.MoveTowards(_audio.volume, targetSoundVolume, recoveryRate * Time.deltaTime);
    }

    private void StartChangingVolume(float targetVolume)
    {
        if (_changeVolumeCoroutine != null)
        {
            StopCoroutine(_changeVolumeCoroutine);
        }

        _changeVolumeCoroutine = StartCoroutine(ChangeAlarmVolume(targetVolume));
    }

    private IEnumerator ChangeAlarmVolume(float targetVolume)
    {
        while (targetVolume != _audio.volume)
        {
            SoundVolumeMoveTowards(targetVolume, _recoveryRateSoundOn);

            yield return null;
        } 
    }
}
