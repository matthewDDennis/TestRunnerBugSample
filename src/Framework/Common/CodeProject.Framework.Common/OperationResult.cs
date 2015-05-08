namespace CodeProject.Framework
{
    public class OperationResult
    {
        /// <summary>
        /// Initializes a new instance of the OperationResult class.
        /// </summary>
        /// <param name="errorCode">The ErrorCode value.</param>
        /// <param name="message">The associated with the error code.</param>
        protected OperationResult(int errorCode, string message)
        {
            ErrorCode = errorCode;
            Message   = message;
        }

        /// <summary>
        /// Gets a value indicating if the operation was successful.
        /// </summary>
        public bool IsSuccess { get { return ErrorCode == 0; } }

        /// <summary>
        /// Gets the Error Code for the operation. 0 == Success
        /// </summary>
        public int ErrorCode { get; private set; }

        /// <summary>
        /// Get the message associated with the ErrorCode, if any.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Creates a new instance of a successful result;
        /// </summary>
        /// <param name="value">The data value to return.</param>
        public static OperationResult SuccessResult()
        {
            return new OperationResult(0, null);
        }

        /// <summary>
        /// Creates a new instance of a failure result;
        /// </summary>
        /// <param name="errorCode">The ErrorCode value.</param>
        /// <param name="message">The associated with the error code.</param>
        /// <returns></returns>
        public static OperationResult ErrorResult(int errorCode, string message)
        {
            return new OperationResult(errorCode, message);
        }
    }

    public class OperationResult<T> : OperationResult
    {
        /// <summary>
        /// Initializes a new instance of the OperationResult[T] class.
        /// </summary>
        /// <param name="errorCode">The ErrorCode value.</param>
        /// <param name="message">The associated with the error code.</param>
        /// <param name="value">The value to store in the result.</param>
        private OperationResult(int errorCode, string message, T value = default(T)) : base(errorCode, message)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the value associated with this result.
        /// </summary>
        public T Value         { get; private set; }

        /// <summary>
        /// Creates a new instance of a successful result;
        /// </summary>
        /// <param name="value">The data value to return.</param>
        public static OperationResult<T> SuccessResult(T value)
        {
            return new OperationResult<T>(0, null, value);
        }

        /// <summary>
        /// Creates a new instance of a successful result;
        /// </summary>
        /// <param name="errorCode">The ErrorCode value.</param>
        /// <param name="message">The associated with the error code.</param>
        /// <param name="value">The value to store in the result.</param>
        public static OperationResult<T> ErrorResult(int errorCode, string message, T value = default(T))
        {
            return new OperationResult<T>(errorCode, message, value);
        }
    }
}
