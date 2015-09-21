namespace Elders.Ares
{
    public sealed class OperationResult
    {
        private OperationResult(string[] errors)
        {
            Errors = errors;
        }

        public static OperationResult Success = new OperationResult(null);

        public static OperationResult Error(string error)
        {
            return new OperationResult(new string[1] { error });
        }

        public bool IsSuccess { get { return Errors == null || Errors.Length == 0; } }

        public string[] Errors { get; private set; }
    }
}