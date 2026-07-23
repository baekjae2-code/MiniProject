using Unity.VectorGraphics.Editor;
using UnityEngine;
using UnityEngine.UI;
public enum SFXType
{
    slash, magic, meteor, heal, arrow, die, hit, button, warning, money
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] AudioSource bgmAudioSource;
    [SerializeField] AudioSource sfxAudioSource;

    public AudioClip[] bgmClip;
    public AudioClip[] soundClip;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX(SFXType type) // (AudioClip clip) , sfxAudioSource.PlayOneShot(clip);
    {
        if ((int)type >= soundClip.Length)
            return;
        sfxAudioSource.PlayOneShot(soundClip[(int)type]);
    }

    public void ChangeBgm()
    {
        if(bgmAudioSource.clip == bgmClip[0])
        {
            bgmAudioSource.Stop();
            bgmAudioSource.generator = bgmClip[1];
            bgmAudioSource.Play();
        }
        else
        {
            bgmAudioSource.Stop();
            bgmAudioSource.generator = bgmClip[0];
            bgmAudioSource.Play();
        }
    }

    public void OnBGMSettingChanged(float value)
    {
        bgmAudioSource.volume = value;
        GameManager.instance.bgmSoundVolume = value;
    }
    public void OnSFXSettingChanged(float value)
    {
        sfxAudioSource.volume = value;
        GameManager.instance.sfxSoundVolume = value;
    }
}
