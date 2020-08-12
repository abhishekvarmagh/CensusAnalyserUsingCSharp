using System;

namespace CensusAnalyserProblem
{
    public class CensusAnalyserException : Exception
    {
        public enum ExceptionType
        {
            FILE_NOT_FOUND, INCORRECT_FILE_TYPE, FILE_CONTAIN_INVALID_DELIMITER, INVALID_HEADERS, NO_SUCH_COUNTRY
        }
        public ExceptionType type;

        public CensusAnalyserException(String message, ExceptionType type) : base(String.Format(message))
        {
            this.type = type;
        }
    }
}