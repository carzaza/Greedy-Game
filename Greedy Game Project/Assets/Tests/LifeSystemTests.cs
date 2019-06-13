using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    /// <summary> Class containing tests for <see cref="LifeSystemController"/>. </summary>
    public class LifeSystemTests
    {
        /// <summary> 
        /// From an initial state in which the player has three lives, decreases this quantity in
        /// one. Lives must be reduced to two.
        /// </summary>
        /// <returns> An empty IEnumerator that allows this method to be executed as a coroutine. </returns>
        [UnityTest]
        public IEnumerator DecreaseLife_LivesMustBeReducedInOneUnit()
        {
            SceneManager.LoadScene("Level1");

            yield return new WaitForSeconds(1);

            LifeSystemController lifeSystemController = GameObject.Find("LifeSystemController").GetComponent<LifeSystemController>();

            lifeSystemController.lives = 3;
            lifeSystemController.DecreaseLife();

            Assert.AreEqual(2, lifeSystemController.lives, "Lives should be equals to 2.");
        }

        /// <summary> 
        /// From an initial state in which the player has one live, decreases this quantity in one.
        /// Lives must be reduced to zero.
        /// </summary>
        /// <returns> An empty IEnumerator that allows this method to be executed as a coroutine. </returns>
        [UnityTest]
        public IEnumerator DecreaseLife_LivesMustBeCero()
        {
            SceneManager.LoadScene("Level1");

            yield return new WaitForSeconds(1);

            LifeSystemController lifeSystemController = GameObject.Find("LifeSystemController").GetComponent<LifeSystemController>();

            lifeSystemController.lives = 1;
            lifeSystemController.DecreaseLife();

            Assert.AreEqual(0, lifeSystemController.lives, "Lives should be equals to 0.");
        }

        /// <summary> 
        /// From an initial state in which the player has zero lives, decreases this quantity in one.
        /// Lives must be maintained in zero, as it is the minimum ammount.
        /// </summary>
        /// <returns> An empty IEnumerator that allows this method to be executed as a coroutine. </returns>
        [UnityTest]
        public IEnumerator DecreaseLife_LivesMustNotChange()
        {
            SceneManager.LoadScene("Level1");

            yield return new WaitForSeconds(1);

            LifeSystemController lifeSystemController = GameObject.Find("LifeSystemController").GetComponent<LifeSystemController>();

            lifeSystemController.lives = 0;
            lifeSystemController.DecreaseLife();

            Assert.AreEqual(0, lifeSystemController.lives, "Lives should be equals to 0.");
        }

        /// <summary> 
        /// From an initial state in which the player has three lives, increases this quantity in
        /// one. Lives must be maintained to three as it is the maximum ammount.
        /// </summary>
        /// <returns> An empty IEnumerator that allows this method to be executed as a coroutine. </returns>
        [UnityTest]
        public IEnumerator IncreaseLife_LivesMustNotChange()
        {
            SceneManager.LoadScene("Level1");

            yield return new WaitForSeconds(1);

            yield return new WaitForSeconds(1);

            LifeSystemController lifeSystemController = GameObject.Find("LifeSystemController").GetComponent<LifeSystemController>();

            lifeSystemController.lives = 3;
            lifeSystemController.IncreaseLife();

            Assert.AreEqual(3, lifeSystemController.lives, "Lives should be equals to 3.");
        }

        /// <summary> 
        /// From an initial state in which the player has two lives, increases this quantity in one.
        /// Lives must be increased to three.
        /// </summary>
        /// <returns> An empty IEnumerator that allows this method to be executed as a coroutine. </returns>
        [UnityTest]
        public IEnumerator IncreaseLife_LivesMustIncreaseInOneUnit()
        {
            SceneManager.LoadScene("Level1");

            yield return new WaitForSeconds(1);

            LifeSystemController lifeSystemController = GameObject.Find("LifeSystemController").GetComponent<LifeSystemController>();

            lifeSystemController.lives = 2;
            lifeSystemController.IncreaseLife();

            Assert.AreEqual(3, lifeSystemController.lives, "Lives should be equals to 3.");
        }
    }
}