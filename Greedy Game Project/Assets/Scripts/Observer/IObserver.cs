namespace Assets.Scripts.Observer
{
    /// <summary> The interface for observers implementing the Obsever Pattern. </summary>
    public interface IObserver
    {
        /// <summary> The action of being notified of a change in a subject. </summary>
        /// <param name="calories"> The consumed calories. </param>
        void Actualiza(int calories);
    }
}