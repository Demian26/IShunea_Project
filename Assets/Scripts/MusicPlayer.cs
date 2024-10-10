using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Plays intro followed by loop when intro is done
public class MusicPlayer : MonoBehaviour
{
    public AudioSource introSource, loopSource;

    // Start is called before the first frame update
    void Start()
    {
        introSource.Play();
        loopSource.PlayScheduled(AudioSettings.dspTime + introSource.clip.length);
    }

    // Метод для остановки музыки при смерти игрока
    public void StopMusic()
    {
        introSource.Stop();
        loopSource.Stop();
    }
}
