using System;
using System.IO;
using System.Linq;

namespace CensusAnalyserProblem
{
    public class CensusAnalyser
    {
        public delegate object CSVData();
        string[] censusData;
        string csvFilePath;
        string header;

        public CensusAnalyser(string csvFilePath, string header)
        {
            this.csvFilePath = csvFilePath;
            this.header = header;
        }
        public object loadIndiaCensusData()
        {
            if (!File.Exists(csvFilePath))
            {
                throw new CensusAnalyserException("File Not Found", CensusAnalyserException.ExceptionType.FILE_NOT_FOUND);
            }
            if (Path.GetExtension(csvFilePath) != ".csv")
            {
                throw new CensusAnalyserException("Incorrect File Type", CensusAnalyserException.ExceptionType.INCORRECT_FILE_TYPE);
            }
            censusData = File.ReadAllLines(csvFilePath);
            if (censusData[0] != header)
            {
                throw new CensusAnalyserException("Invalid Headers", CensusAnalyserException.ExceptionType.INVALID_HEADERS);
            }
            foreach (string records in censusData)
            {
                if (!records.Contains(","))
                {
                    throw new CensusAnalyserException("File Contain Invalid Delimiters", CensusAnalyserException.ExceptionType.FILE_CONTAIN_INVALID_DELIMITER);
                }
            }
            return censusData.Skip(1).ToArray();
        }

    }
}
