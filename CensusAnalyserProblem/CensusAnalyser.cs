using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CensusAnalyserProblem
{
    public class CensusAnalyser : ICSVBuilder
    {
        public delegate object CSVData(string csvFilePath, string header);
        string[] censusData;
        int key = 0;
        Dictionary<int, string> censusDataMap;

        public object loadIndiaCensusData(string csvFilePath, string header)
        {
            censusDataMap = new Dictionary<int, string>();
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
                key++;
                censusDataMap.Add(key, records);
                if (!records.Contains(","))
                {
                    throw new CensusAnalyserException("File Contain Invalid Delimiters", CensusAnalyserException.ExceptionType.FILE_CONTAIN_INVALID_DELIMITER);
                }
            }
            return censusDataMap.Skip(1).ToDictionary(p => p.Key, p => p.Value);
        }

        public object getStateWiseSortedCensusData(string csvFilePath, string header, int index)
        {
            Dictionary<int, string> censusData = (Dictionary<int, string>)loadIndiaCensusData(csvFilePath, header);
            string[] records = censusData.Values.ToArray();
            var dataWithoutHeader = records;
            var sorted =
                from data in dataWithoutHeader
                let column = data.Split(',')
                orderby column[index]
                select data;
            List<string> sortedData = sorted.ToList();
            return JsonConvert.SerializeObject(sortedData);
        }

    }
}
