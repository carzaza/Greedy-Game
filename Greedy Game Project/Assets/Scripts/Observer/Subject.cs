using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Observer
{
    /// <summary> The subject that works with the observer. </summary>
    public abstract class Subject : MonoBehaviour
    {
        protected IList<IObserver> observers = new List<IObserver>();

        /// <summary> Adds a new observer to this subject. </summary>
        /// <param name="observer"> The observer to be added. </param>
        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        /// <summary> Deletes the given observer from this subject. </summary>
        /// <param name="observer"> The observer to be deleted. </param>
        public void DeleteObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        /// <summary> The action of notify to each observers of a change. </summary>
        /// <param name="calories"> The consumed calories. </param>
        public void Notify(int calories)
        {
            foreach (IObserver observer in observers)
                observer.Actualiza(calories);
        }
    }
}