using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource[] audioPool;

    public float soundsVolume = 0.5f;
    [SerializeField] private AudioClip btnClickClip;
    [SerializeField] private AudioClip errorClip;
    [SerializeField] private AudioClip bgmClip;
    [SerializeField] private AudioClip recieveClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        audioPool = new AudioSource[100];
    }

    public void ChangeVolume(float volume) //type = 1 Bgm / type = 0 SFX
    {
        soundsVolume = volume;
    }

    private IEnumerator DestroyAudio(GameObject audioGO, float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        Destroy(audioGO);
    }
    public void ErrorClipPlay()
    {
        for (int i = 0; i < audioPool.Length; i++)
        {
            if (audioPool[i] == null || !audioPool[i].isPlaying)
            {
                GameObject go = new GameObject { name = errorClip.name };
                audioPool[i] = go.AddComponent<AudioSource>();
                audioPool[i].transform.position = Camera.main.transform.position;
                audioPool[i].spatialBlend = 0.0f;
                audioPool[i].PlayOneShot(errorClip, 0.5f);


                StartCoroutine(DestroyAudio(go, errorClip.length * 3.5f));
                return;
            }
        }
        return;
    }
    public void BtnClickPlay()
    {
        for (int i = 0; i < audioPool.Length; i++)
        {
            if (audioPool[i] == null || !audioPool[i].isPlaying)
            {
                GameObject go = new GameObject { name = btnClickClip.name };
                audioPool[i] = go.AddComponent<AudioSource>();
                audioPool[i].transform.position = Camera.main.transform.position;
                audioPool[i].spatialBlend = 0.0f;
                audioPool[i].PlayOneShot(btnClickClip, soundsVolume);


                StartCoroutine(DestroyAudio(go, btnClickClip.length * 3.5f));
                return;
            }
        }
        return;
    }
    public void RecieveBtnPlay()
    {
        for (int i = 0; i < audioPool.Length; i++)
        {
            if (audioPool[i] == null || !audioPool[i].isPlaying)
            {
                GameObject go = new GameObject { name = recieveClip.name };
                audioPool[i] = go.AddComponent<AudioSource>();
                audioPool[i].transform.position = Camera.main.transform.position;
                audioPool[i].spatialBlend = 0.0f;
                audioPool[i].PlayOneShot(btnClickClip, soundsVolume);


                StartCoroutine(DestroyAudio(go, recieveClip.length * 3.5f));
                return;
            }
        }
        return;
    } 
    public void PlaySound(AudioClip _clip)
    {
        for (int i = 0; i < audioPool.Length; i++)
        {
            if (audioPool[i] == null || !audioPool[i].isPlaying)
            {
                GameObject go = new GameObject { name = _clip.name };
                audioPool[i] = go.AddComponent<AudioSource>();
                audioPool[i].transform.position = Camera.main.transform.position;
                audioPool[i].spatialBlend = 0.0f;
                audioPool[i].PlayOneShot(_clip, soundsVolume);


                StartCoroutine(DestroyAudio(go, _clip.length * 3.5f));
                return;
            }
        }
        return;
    }
    public void PlayBGM()
    {
        for (int i = 0; i < audioPool.Length; i++)
        {
            if (audioPool[i] == null || !audioPool[i].isPlaying)
            {
                GameObject go = new GameObject { name = bgmClip.name };
                audioPool[i] = go.AddComponent<AudioSource>();
                audioPool[i].transform.position = Camera.main.transform.position;
                audioPool[i].spatialBlend = 0.0f;
                
                audioPool[i].clip = bgmClip;
                audioPool[i].volume = soundsVolume;
                audioPool[i].Play();
                //audioPool[i].PlayOneShot(bgmClip, soundsVolume);
                audioPool[i].loop = true;

                return;
            }
        }
        return;
    }
    public void PlayBGM(AudioClip _clip)
    {
        for (int i = 0; i < audioPool.Length; i++)
        {
            if (audioPool[i] == null || !audioPool[i].isPlaying)
            {
                GameObject go = new GameObject { name = _clip.name };
                audioPool[i] = go.AddComponent<AudioSource>();
                audioPool[i].transform.position = Camera.main.transform.position;
                audioPool[i].spatialBlend = 0.0f;

                audioPool[i].clip = _clip;
                audioPool[i].volume = soundsVolume;
                audioPool[i].Play();
                //audioPool[i].PlayOneShot(bgmClip, soundsVolume);
                audioPool[i].loop = true;

                return;
            }
        }
        return;
    }

}