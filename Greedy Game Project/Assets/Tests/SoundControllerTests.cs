using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class SoundControllerTests
    {
        /// <summary> 
        /// From an initial state in which the all the sounds are active, mutes all sounds. All
        /// sounds should be muted.
        /// </summary>
        /// <returns> An empty IEnumerator that allows this method to be executed as a coroutine. </returns>
        [UnityTest]
        public IEnumerator MuteSound_AllSoundMustBeMuted()
        {
            SceneManager.LoadScene("Level1");

            yield return new WaitForSeconds(1);

            AudioSource gameOverSound = GameObject.Find("GameOverAudioSource").GetComponent<AudioSource>();
            AudioSource winSound = GameObject.Find("WinSoundAudioSource").GetComponent<AudioSource>();
            AudioSource walkSound = GameObject.Find("WalkAudioSource").GetComponent<AudioSource>();
            AudioSource dieSound = GameObject.Find("DieAudioSource").GetComponent<AudioSource>();
            AudioSource eatSound = GameObject.Find("EatAudioSource").GetComponent<AudioSource>();
            AudioSource lifeUpSound = GameObject.Find("LifeUpAudioSource").GetComponent<AudioSource>();
            AudioSource explosionSound = GameObject.Find("BombAudioSource").GetComponent<AudioSource>();
            AudioSource enemySound = GameObject.Find("EnemyAudioSource").GetComponent<AudioSource>();
            AudioSource collisionSound = GameObject.Find("CollisionAudioSource").GetComponent<AudioSource>();
            AudioSource damageSound = GameObject.Find("DamageSoundAudioSource").GetComponent<AudioSource>();
            AudioSource energyCapsuleSound = GameObject.Find("EnergyCapsuleAudioSource").GetComponent<AudioSource>();
            Mediator mediator = GameObject.Find("Mediator").GetComponent<Mediator>();

            mediator.MuteSound(true);

            Assert.AreEqual(true, gameOverSound.mute, "Game over sound must be muted.");
            Assert.AreEqual(true, winSound.mute, "Win sound must be muted.");
            Assert.AreEqual(true, walkSound.mute, "Walk sound must be muted.");
            Assert.AreEqual(true, dieSound.mute, "Die sound must be muted.");
            Assert.AreEqual(true, eatSound.mute, "Eat sound must be muted.");
            Assert.AreEqual(true, lifeUpSound.mute, "Life up sound must be muted.");
            Assert.AreEqual(true, explosionSound.mute, "Explosion sound must be muted.");
            Assert.AreEqual(true, enemySound.mute, "Enemy sound must be muted.");
            Assert.AreEqual(true, collisionSound.mute, "Collision sound must be muted.");
            Assert.AreEqual(true, damageSound.mute, "Damage sound must be muted.");
            Assert.AreEqual(true, energyCapsuleSound.mute, "Energy capsule sound must be muted.");
        }

        /// <summary> 
        /// From an initial state in which the all the sounds are muted, activates all sounds. All
        /// sounds should be activated.
        /// </summary>
        /// <returns> An empty IEnumerator that allows this method to be executed as a coroutine. </returns>
        [UnityTest]
        public IEnumerator UnMuteSound_AllSoundMustBeUnMuted()
        {
            SceneManager.LoadScene("Level1");

            yield return new WaitForSeconds(1);

            AudioSource gameOverSound = GameObject.Find("GameOverAudioSource").GetComponent<AudioSource>();
            AudioSource winSound = GameObject.Find("WinSoundAudioSource").GetComponent<AudioSource>();
            AudioSource walkSound = GameObject.Find("WalkAudioSource").GetComponent<AudioSource>();
            AudioSource dieSound = GameObject.Find("DieAudioSource").GetComponent<AudioSource>();
            AudioSource eatSound = GameObject.Find("EatAudioSource").GetComponent<AudioSource>();
            AudioSource lifeUpSound = GameObject.Find("LifeUpAudioSource").GetComponent<AudioSource>();
            AudioSource explosionSound = GameObject.Find("BombAudioSource").GetComponent<AudioSource>();
            AudioSource enemySound = GameObject.Find("EnemyAudioSource").GetComponent<AudioSource>();
            AudioSource collisionSound = GameObject.Find("CollisionAudioSource").GetComponent<AudioSource>();
            AudioSource damageSound = GameObject.Find("DamageSoundAudioSource").GetComponent<AudioSource>();
            AudioSource energyCapsuleSound = GameObject.Find("EnergyCapsuleAudioSource").GetComponent<AudioSource>();
            Mediator mediator = GameObject.Find("Mediator").GetComponent<Mediator>();

            mediator.MuteSound(false);

            Assert.AreEqual(false, gameOverSound.mute, "Game over sound must be unmuted.");
            Assert.AreEqual(false, winSound.mute, "Win sound must be unmuted.");
            Assert.AreEqual(false, walkSound.mute, "Walk sound must be unmuted.");
            Assert.AreEqual(false, dieSound.mute, "Die sound must be unmuted.");
            Assert.AreEqual(false, eatSound.mute, "Eat sound must be unmuted.");
            Assert.AreEqual(false, lifeUpSound.mute, "Life up sound must be unmuted.");
            Assert.AreEqual(false, explosionSound.mute, "Explosion sound must be unmuted.");
            Assert.AreEqual(false, enemySound.mute, "Enemy sound must be unmuted.");
            Assert.AreEqual(false, collisionSound.mute, "Collision sound must be unmuted.");
            Assert.AreEqual(false, damageSound.mute, "Damage sound must be unmuted.");
            Assert.AreEqual(false, energyCapsuleSound.mute, "Energy capsule sound must be unmuted.");
        }

        /// <summary> 
        /// From an initial state in which the all the sounds are muted, mutes all sounds. All sounds
        /// should remain muted.
        /// </summary>
        /// <returns> An empty IEnumerator that allows this method to be executed as a coroutine. </returns>
        [UnityTest]
        public IEnumerator MuteSound_AllSoundMustRemainMuted()
        {
            SceneManager.LoadScene("Level1");

            yield return new WaitForSeconds(1);

            AudioSource gameOverSound = GameObject.Find("GameOverAudioSource").GetComponent<AudioSource>();
            AudioSource winSound = GameObject.Find("WinSoundAudioSource").GetComponent<AudioSource>();
            AudioSource walkSound = GameObject.Find("WalkAudioSource").GetComponent<AudioSource>();
            AudioSource dieSound = GameObject.Find("DieAudioSource").GetComponent<AudioSource>();
            AudioSource eatSound = GameObject.Find("EatAudioSource").GetComponent<AudioSource>();
            AudioSource lifeUpSound = GameObject.Find("LifeUpAudioSource").GetComponent<AudioSource>();
            AudioSource explosionSound = GameObject.Find("BombAudioSource").GetComponent<AudioSource>();
            AudioSource enemySound = GameObject.Find("EnemyAudioSource").GetComponent<AudioSource>();
            AudioSource collisionSound = GameObject.Find("CollisionAudioSource").GetComponent<AudioSource>();
            AudioSource damageSound = GameObject.Find("DamageSoundAudioSource").GetComponent<AudioSource>();
            AudioSource energyCapsuleSound = GameObject.Find("EnergyCapsuleAudioSource").GetComponent<AudioSource>();
            Mediator mediator = GameObject.Find("Mediator").GetComponent<Mediator>();

            mediator.MuteSound(true);
            mediator.MuteSound(true);

            Assert.AreEqual(true, gameOverSound.mute, "Game over sound must be muted.");
            Assert.AreEqual(true, winSound.mute, "Win sound must be muted.");
            Assert.AreEqual(true, walkSound.mute, "Walk sound must be muted.");
            Assert.AreEqual(true, dieSound.mute, "Die sound must be muted.");
            Assert.AreEqual(true, eatSound.mute, "Eat sound must be muted.");
            Assert.AreEqual(true, lifeUpSound.mute, "Life up sound must be muted.");
            Assert.AreEqual(true, explosionSound.mute, "Explosion sound must be muted.");
            Assert.AreEqual(true, enemySound.mute, "Enemy sound must be muted.");
            Assert.AreEqual(true, collisionSound.mute, "Collision sound must be muted.");
            Assert.AreEqual(true, damageSound.mute, "Damage sound must be muted.");
            Assert.AreEqual(true, energyCapsuleSound.mute, "Energy capsule sound must be muted.");
        }

        /// <summary> 
        /// From an initial state in which the all the sounds are activated, activates all sounds.
        /// All sounds should remain activated.
        /// </summary>
        /// <returns> An empty IEnumerator that allows this method to be executed as a coroutine. </returns>
        [UnityTest]
        public IEnumerator UnMuteSound_AllSoundMustRemainUnMuted()
        {
            SceneManager.LoadScene("Level1");

            yield return new WaitForSeconds(1);

            AudioSource gameOverSound = GameObject.Find("GameOverAudioSource").GetComponent<AudioSource>();
            AudioSource winSound = GameObject.Find("WinSoundAudioSource").GetComponent<AudioSource>();
            AudioSource walkSound = GameObject.Find("WalkAudioSource").GetComponent<AudioSource>();
            AudioSource dieSound = GameObject.Find("DieAudioSource").GetComponent<AudioSource>();
            AudioSource eatSound = GameObject.Find("EatAudioSource").GetComponent<AudioSource>();
            AudioSource lifeUpSound = GameObject.Find("LifeUpAudioSource").GetComponent<AudioSource>();
            AudioSource explosionSound = GameObject.Find("BombAudioSource").GetComponent<AudioSource>();
            AudioSource enemySound = GameObject.Find("EnemyAudioSource").GetComponent<AudioSource>();
            AudioSource collisionSound = GameObject.Find("CollisionAudioSource").GetComponent<AudioSource>();
            AudioSource damageSound = GameObject.Find("DamageSoundAudioSource").GetComponent<AudioSource>();
            AudioSource energyCapsuleSound = GameObject.Find("EnergyCapsuleAudioSource").GetComponent<AudioSource>();
            Mediator mediator = GameObject.Find("Mediator").GetComponent<Mediator>();

            mediator.MuteSound(false);
            mediator.MuteSound(false);

            Assert.AreEqual(false, gameOverSound.mute, "Game over sound must be unmuted.");
            Assert.AreEqual(false, winSound.mute, "Win sound must be unmuted.");
            Assert.AreEqual(false, winSound.mute, "Win sound must be unmuted.");
            Assert.AreEqual(false, walkSound.mute, "Walk sound must be unmuted.");
            Assert.AreEqual(false, dieSound.mute, "Die sound must be unmuted.");
            Assert.AreEqual(false, eatSound.mute, "Eat sound must be unmuted.");
            Assert.AreEqual(false, lifeUpSound.mute, "Life up sound must be unmuted.");
            Assert.AreEqual(false, explosionSound.mute, "Explosion sound must be unmuted.");
            Assert.AreEqual(false, enemySound.mute, "Enemy sound must be unmuted.");
            Assert.AreEqual(false, collisionSound.mute, "Collision sound must be unmuted.");
            Assert.AreEqual(false, damageSound.mute, "Damage sound must be unmuted.");
            Assert.AreEqual(false, energyCapsuleSound.mute, "Energy capsule sound must be unmuted.");
        }

        /// <summary> 
        /// From an initial state in which the all the music are active, mutes the music. Music
        /// should be muted.
        /// </summary>
        /// <returns> An empty IEnumerator that allows this method to be executed as a coroutine. </returns>
        [UnityTest]
        public IEnumerator MuteMusic_AllMusicMustBeMuted()
        {
            SceneManager.LoadScene("Level1");

            yield return new WaitForSeconds(1);

            AudioSource ambientMusicSound = GameObject.Find("AmbientMusicAudioSource").GetComponent<AudioSource>();
            Mediator mediator = GameObject.Find("Mediator").GetComponent<Mediator>();

            mediator.MuteMusic(true);

            Assert.AreEqual(true, ambientMusicSound.mute, "Ambient music must be muted.");
        }

        /// <summary> 
        /// From an initial state in which the all the music are muted, mutes the music. Music should
        /// remain muted.
        /// </summary>
        /// <returns> An empty IEnumerator that allows this method to be executed as a coroutine. </returns>
        [UnityTest]
        public IEnumerator MuteMusic_AllMusicMustRemainMuted()
        {
            SceneManager.LoadScene("Level1");

            yield return new WaitForSeconds(1);

            AudioSource ambientMusicSound = GameObject.Find("AmbientMusicAudioSource").GetComponent<AudioSource>();
            Mediator mediator = GameObject.Find("Mediator").GetComponent<Mediator>();

            mediator.MuteMusic(true);
            mediator.MuteMusic(true);

            Assert.AreEqual(true, ambientMusicSound.mute, "Ambient music must be muted.");
        }

        /// <summary> 
        /// From an initial state in which the music is muted, activates the music. The music should
        /// be activated.
        /// </summary>
        /// <returns> An empty IEnumerator that allows this method to be executed as a coroutine. </returns>
        [UnityTest]
        public IEnumerator UnMuteMusic_AllMusicMustBeUnMuted()
        {
            SceneManager.LoadScene("Level1");

            yield return new WaitForSeconds(1);

            AudioSource ambientMusicSound = GameObject.Find("AmbientMusicAudioSource").GetComponent<AudioSource>();
            Mediator mediator = GameObject.Find("Mediator").GetComponent<Mediator>();

            mediator.MuteMusic(false);

            Assert.AreEqual(false, ambientMusicSound.mute, "Ambient music must be unmuted.");
        }

        /// <summary> 
        /// From an initial state in which the all the music are active, activates the music. Music
        /// should remain active.
        /// </summary>
        /// <returns> An empty IEnumerator that allows this method to be executed as a coroutine. </returns>
        [UnityTest]
        public IEnumerator UnMuteMusic_AllMusicMustRemainUnMuted()
        {
            SceneManager.LoadScene("Level1");

            yield return new WaitForSeconds(1);

            AudioSource ambientMusicSound = GameObject.Find("AmbientMusicAudioSource").GetComponent<AudioSource>();
            Mediator mediator = GameObject.Find("Mediator").GetComponent<Mediator>();

            mediator.MuteMusic(false);
            mediator.MuteMusic(false);

            Assert.AreEqual(false, ambientMusicSound.mute, "Ambient music must be unmuted.");
        }
    }
}