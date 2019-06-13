using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    /// <summary> Class containing tests for <see cref="Food"/> and <see cref="FoodController"/>. </summary>
    public class FoodTests
    {
        /// <summary> 
        /// From an initial state in which the calories consumed are zero, tests the consumption of
        /// two standard foods: an apple and a carrot. The calories should be increased to sixty
        /// (twenty from the apple and forty from the carrot).
        /// </summary>
        /// <returns> An empty IEnumerator that allows this method to be executed as a coroutine. </returns>
        [UnityTest]
        public IEnumerator TestStandardFoodConsume_CaloriesMustBeIncreased()
        {
            SceneManager.LoadScene("Level1");

            yield return new WaitForSeconds(1);

            Food standardApple = GameObject.Find("StandardApple").GetComponent<Food>();
            Food standardCarrot = GameObject.Find("StandardCarrot").GetComponent<Food>();
            FoodController foodController = GameObject.Find("FoodController").GetComponent<FoodController>();

            standardApple.Consume();
            standardCarrot.Consume();

            Assert.AreEqual(60, foodController.Calories, "The amount of calories should be equals to 60.");
        }

        /// <summary> 
        /// From an initial state in which the calories consumed are zero, tests the consumption of
        /// two golden foods: an apple and a carrot. The calories should be increased to eighty
        /// (thirty from the apple and fifty from the carrot).
        /// </summary>
        /// <returns> An empty IEnumerator that allows this method to be executed as a coroutine. </returns>
        [UnityTest]
        public IEnumerator TestSpecialFoodConsume_CaloriesMustBeIncreased()
        {
            SceneManager.LoadScene("Level1");

            yield return new WaitForSeconds(1);

            Food goldenApple = GameObject.Find("GoldenApple").GetComponent<Food>();
            Food goldenCarrot = GameObject.Find("GoldenCarrot").GetComponent<Food>();
            FoodController foodController = GameObject.Find("FoodController").GetComponent<FoodController>();

            goldenApple.Consume();
            goldenCarrot.Consume();

            Assert.AreEqual(80, foodController.Calories, "The amount of calories should be equals to 80.");
        }

        /// <summary> 
        /// From the initial scene, checks if the created foods gives the same calories as the client requeriments.
        /// </summary>
        /// <returns> An empty IEnumerator that allows this method to be executed as a coroutine. </returns>
        [UnityTest]
        public IEnumerator TestFoodCalories_CaloriesMustBeTheSameAsRequirements()
        {
            SceneManager.LoadScene("Level1");

            yield return new WaitForSeconds(1);

            Food standardApple = GameObject.Find("StandardApple").GetComponent<Food>();
            Food standardCarrot = GameObject.Find("StandardCarrot").GetComponent<Food>();
            Food goldenApple = GameObject.Find("GoldenApple").GetComponent<Food>();
            Food goldenCarrot = GameObject.Find("GoldenCarrot").GetComponent<Food>();

            Assert.AreEqual(20, standardApple.Calories, "The calories of a standard apple should be 20.");
            Assert.AreEqual(30, goldenApple.Calories, "The calories of a golden apple should be 30.");
            Assert.AreEqual(40, standardCarrot.Calories, "The calories of a standard carrot should be 40.");
            Assert.AreEqual(50, goldenCarrot.Calories, "The calories of a golden carrot should be 50.");
        }
    }
}