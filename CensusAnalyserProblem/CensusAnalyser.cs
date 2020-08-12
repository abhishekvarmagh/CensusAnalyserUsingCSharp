using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CensusAnalyserProblem.DAO;
using static CensusAnalyserProblem.SortFeild;
using CensusAnalyserProblem.POCO;

namespace CensusAnalyserProblem
{
    public class CensusAnalyser : ICSVBuilder
    {
        string[] censusData;
        Dictionary<string, CensusDataDAO> censusDataMap = new Dictionary<string, CensusDataDAO>();

        public Dictionary<string, CensusDataDAO> loadIndiaCensusData(string csvFilePath, string header)
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
            foreach (string records in censusData.Skip(1))
            {
                if (!records.Contains(","))
                {
                    throw new CensusAnalyserException("File Contain Invalid Delimiters", CensusAnalyserException.ExceptionType.FILE_CONTAIN_INVALID_DELIMITER);
                }
                string[] column = records.Split(",");
                if (csvFilePath.Contains("IndiaStateCode.csv"))
                    censusDataMap.Add(column[1], new CensusDataDAO(new IndianStateCode(Convert.ToInt32(column[0]), column[1], Convert.ToInt32(column[2]), column[3])));
                if (csvFilePath.Contains("IndiaStateCensusData.csv"))
                    censusDataMap.Add(column[0], new CensusDataDAO(new IndianCensus(column[0], column[1], column[2], column[3])));
            }
            return censusDataMap;
        }

        public Dictionary<string, CensusDataDAO> loadUSCensusData(string csvFilePath, string header)
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
            foreach (string records in censusData.Skip(1))
            {
                if (!records.Contains(","))
                {
                    throw new CensusAnalyserException("File Contain Invalid Delimiters", CensusAnalyserException.ExceptionType.FILE_CONTAIN_INVALID_DELIMITER);
                }
                string[] column = records.Split(",");
                censusDataMap.Add(column[1], new CensusDataDAO(new USCensus(column[0], column[1], Convert.ToInt32(column[2]), Convert.ToInt32(column[3]), Convert.ToDouble(column[4]), Convert.ToDouble(column[5]), Convert.ToDouble(column[6]), Convert.ToDouble(column[7]), Convert.ToDouble(column[8]))));
            }
            return censusDataMap;
        }

        public string getStateWiseSortedCensusData(Dictionary<string, CensusDataDAO> data, SortBy sortBy)
        {
            List<CensusDataDAO> list = SortFeild.SortCensusData(data.Select(x => x.Value).ToList(), sortBy); 
            return JsonConvert.SerializeObject(list);
        }

    }
}
