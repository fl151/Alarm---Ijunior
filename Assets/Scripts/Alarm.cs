using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _recoveryRateSoundOn;
    [SerializeField] private float _recoveryRateSoundOff;
    [SerializeField] private float _maxSoundVolume;

    private float _soundVolume;
    private readonly float _minSoundVolume = 0;
    private Coroutine _alarmCoroutine;

    private void Awake()
    {
        if(_maxSoundVolume >= 1)
        {
            _maxSoundVolume = 1;
        }
        if (_maxSoundVolume <=0)
        {
            _maxSoundVolume = 0;
        }

        _soundVolume = _minSoundVolume;
    }

    private void Update()
    {
        _audio.volume = _soundVolume;
    }

    private void SoundVolumeMoveTowards(float targetSoundVolume, float recoveryRate)
    {
        _soundVolume = Mathf.MoveTowards(_soundVolume, targetSoundVolume, recoveryRate * Time.deltaTime);
    }

    public void StartAlarm()
    {
        if(_alarmCoroutine != null)
        {
            StopCoroutine(_alarmCoroutine);
        }

        _alarmCoroutine = StartCoroutine(OnAlarm());
    }

    public void StopAlarm()
    {
        if (_alarmCoroutine != null)
        {
            StopCoroutine(_alarmCoroutine);
        }

        _alarmCoroutine = StartCoroutine(OffAlarm());
    }

    private IEnumerator OnAlarm()
    {
        while (_maxSoundVolume > _soundVolume)
        {
            SoundVolumeMoveTowards(_maxSoundVolume, _recoveryRateSoundOn);

            yield return null;
        } 
    }

    private IEnumerator OffAlarm()
    {
        while (_minSoundVolume < _soundVolume)
        {
            SoundVolumeMoveTowards(_minSoundVolume, _recoveryRateSoundOff);

            yield return null;
        }
    }
}
