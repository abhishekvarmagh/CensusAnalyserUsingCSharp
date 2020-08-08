using System;

namespace CensusAnalyserProblem
{
    public class CensusAnalyserException : Exception
    {
        public enum ExceptionType
        {
            FILE_NOT_FOUND, INCORRECT_FILE_TYPE, FILE_CONTAIN_INVALID_DELIMITER
        }
        public ExceptionType type;

        public CensusAnalyserException(String message, ExceptionType type) : base(String.Format(message))
        {
            this.type = type;
        }
    }
}