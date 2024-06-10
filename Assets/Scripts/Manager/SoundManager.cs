using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private bool isOn;

    public GameObject stoneMoveSoundObject;
    public GameObject stoneStopSoundObject;
    public GameObject ambientSoundObject;

    public AudioSource buttonSound;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void Switch(bool on)
    {
        isOn = on;

        PlayAmbientSound();
    }

    private void PlayAmbientSound()
    {
        if (!isOn)
            return;

        AudioSource ambientSound = ambientSoundObject.GetComponent<AudioSource>();

        ambientSound.Play();
    }

    public void PlayStoneMove()
    {
        if (!isOn)
            return;

        //GameObject soundObject = Instantiate(stoneMoveSoundObject);
        //soundObject.transform.parent = stoneMoveSoundObject.transform;

        //AudioSource stoneMoveSound = soundObject.GetComponent<AudioSource>();

        AudioSource stoneMoveSound = stoneMoveSoundObject.GetComponent<AudioSource>();
        AudioSource newStoneMoveSound = stoneMoveSoundObject.AddComponent<AudioSource>();


        newStoneMoveSound.clip = stoneMoveSound.clip;
        newStoneMoveSound.maxDistance = stoneMoveSound.maxDistance;
        newStoneMoveSound.Play();

        Destroy(newStoneMoveSound, stoneMoveSound.clip.length + 1);
        //DestroyGameObject destroyObjectComp = soundObject.AddComponent<DestroyGameObject>();
        //destroyObjectComp.DestroyDelay(stoneMoveSound.clip.length+1);
    }

    public void StopStoneMove()
    {
        if (stoneMoveSoundObject.transform.childCount == 0)
            return;

        AudioSource[] allStoneMoveSound = stoneMoveSoundObject.GetComponents<AudioSource>();

        for (int i = 1; i < allStoneMoveSound.Length; i++)
        {
            Destroy(allStoneMoveSound[i]);
        }
    }

    public void PlayStoneStop()
    {
        if (!isOn)
            return;

        GameObject soundObject = Instantiate(stoneStopSoundObject);
        soundObject.transform.parent = stoneStopSoundObject.transform;

        AudioSource stoneMoveSound = soundObject.GetComponent<AudioSource>();
        stoneMoveSound.Play();


        //DestroyGameObject destroyObjectComp = soundObject.AddComponent<DestroyGameObject>();
        //destroyObjectComp.DestroyDelay(stoneMoveSound.clip.length + 1);
        Destroy(soundObject, stoneMoveSound.clip.length + 1);
    }

    public void PlayButtonSound()
    {
        if (!isOn)
            return;

        buttonSound.Play();
    }

}