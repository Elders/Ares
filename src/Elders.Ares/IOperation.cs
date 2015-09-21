namespace Elders.Ares
{
    public interface IOperation
    {
        string Name { get; }

        /// <summary>
        /// Executes the operation.
        /// </summary>
        void Run();

        /// <summary>
        /// It is considered a poor practice to have a fallback implementation that can fail. 
        /// A fallback should be implemented such that it is not performing any logic that would fail.
        /// </summary>
        void FallBack();
    }
}