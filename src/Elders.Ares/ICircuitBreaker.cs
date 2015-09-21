namespace Elders.Ares
{
    public interface ICircuitBreaker
    {
        bool IsOpen();
        void OperationSuccess();
    }
}