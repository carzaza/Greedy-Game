using UnityEngine;

/// <summary> The controller for the sound. </summary>
public class SoundController : MonoBehaviour
{
    public AudioSource ambientMusicSound, gameOverSound, collisionSound, winSound;
    public AudioSource walkSound, lifeUpSound, eatSound, explosionSound, dieSound, damageSound, energyCapsuleSound;
    public AudioSource enemySound;
    public AudioSource WinSound;

    /// <summary> Update is called once per frame. </summary>
    private void Update()
    {
        MuteMusic(!GlobalControl.Instance.musicActivated);
        MuteSound(!GlobalControl.Instance.soundActivated);
    }

    /// <summary> Mutes or unmutes the sound. </summary>
    /// <param name="mute"> Indicates if the sound must be muted or not. </param>
    public void MuteSound(bool mute)
    {
        gameOverSound.mute = mute;
        winSound.mute = mute;
        walkSound.mute = mute;
        dieSound.mute = mute;
        eatSound.mute = mute;
        lifeUpSound.mute = mute;
        explosionSound.mute = mute;
        enemySound.mute = mute;
        collisionSound.mute = mute;
        damageSound.mute = mute;
        energyCapsuleSound.mute = mute;
    }

    /// <summary> Mutes or unmutes the music. </summary>
    /// <param name="mute"> Indicates if the music must be muter or not. </param>
    public void MuteMusic(bool mute)
    {
        ambientMusicSound.mute = mute;
    }
}