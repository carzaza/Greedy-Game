using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    /// <summary> Class containing test for the <see cref="DamageSystemController"/>. </summary>
    public class DamageSystemTest
    {
        /// <summary> 
        /// From an initial state in which the damage registered is thirty, checks its decreasement
        /// in a ten percent. The damage should be reduced to twenty.
        /// </summary>
        /// <returns> An empty IEnumerator that allows this method to be executed as a coroutine. </returns>
        [UnityTest]
        public IEnumerator DecreaseDamageInTenPercent_DamageMustBeTenUnitsLower()
        {
            SceneManager.LoadScene("Level1");

            yield return new WaitForSeconds(1);

            DamageSystemController damageSystemController = GameObject.Find("DamageSystemController").GetComponent<DamageSystemController>();

            damageSystemController.IncreaseDamage(30);

            damageSystemController.DecreaseDamage();

            Assert.AreEqual(20, damageSystemController.Damage, "The damage should be 20.");
        }

        /// <summary> 
        /// From an initial state in which the damage registered is zero, checks its decreasement in
        /// a ten percent. The damage should be maintained in zero.
        /// </summary>
        /// <returns> An empty IEnumerator that allows this method to be executed as a coroutine. </returns>
        [UnityTest]
        public IEnumerator DecreaseDamageInTenPercent_DamageMustMaintainInCero()
        {
            SceneManager.LoadScene("Level1");

            yield return new WaitForSeconds(1);

            DamageSystemController damageSystemController = GameObject.Find("DamageSystemController").GetComponent<DamageSystemController>();

            damageSystemController.ResetDamage();

            damageSystemController.DecreaseDamage();

            Assert.AreEqual(0, damageSystemController.Damage, "The damage should be 0.");
        }

        /// <summary> 
        /// From an initial state in which the damage registered is ninety, checks its increasement
        /// in a twenty percent. Lives should be reduced to two and damaged should be reset.
        /// </summary>
        /// <returns> An empty IEnumerator that allows this method to be executed as a coroutine. </returns>
        [UnityTest]
        public IEnumerator DamageOverOneHundred_LivesMustBeReduced()
        {
            SceneManager.LoadScene("Level1");

            yield return new WaitForSeconds(1);

            DamageSystemController damageSystemController = GameObject.Find("DamageSystemController").GetComponent<DamageSystemController>();
            LifeSystemController lifeSystemController = GameObject.Find("LifeSystemController").GetComponent<LifeSystemController>();

            int lives = lifeSystemController.lives;

            damageSystemController.Damage = 90;
            damageSystemController.IncreaseDamage(20);

            Assert.AreEqual(lives - 1, lifeSystemController.lives, "Lives should have been reduced.");
            Assert.AreEqual(0, damageSystemController.Damage, "The damage should have been reset.");
        }
    }
}