﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CensusAnalyserProblem.DAO;

namespace CensusAnalyserProblem
{
    public class CensusAnalyser : ICSVBuilder
    {
        public delegate object CSVData(string csvFilePath, string header);
        string[] censusData;
        Dictionary<string, CensusDataDAO> daoMap = new Dictionary<string, CensusDataDAO>();

        public object loadIndiaCensusData(string csvFilePath, string header)
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
                    daoMap.Add(column[1], new CensusDataDAO(Convert.ToInt32(column[0]), column[1], Convert.ToInt32(column[2]), column[3]));
                if (csvFilePath.Contains("IndiaStateCensusData.csv"))
                    daoMap.Add(column[0], new CensusDataDAO(column[0], column[1], column[2], column[3]));
            }
            return daoMap;
        }

        public string getStateWiseSortedCensusData(string csvFilePath, string header, string sortByField)
        {
            var censusData = (Dictionary<string, CensusDataDAO>)loadIndiaCensusData(csvFilePath, header);
            //CensusDataDAO[] records = censusData.Values.ToArray();
            List<CensusDataDAO> data = censusData.Values.ToList(); 
            return JsonConvert.SerializeObject(getField(sortByField, data));
        }

        public List<CensusDataDAO> getField(string sortfield, List<CensusDataDAO> data)
        {
            switch (sortfield)
            {
                case "stateName": return data.OrderBy(x => x.state).ToList();
                case "stateCode": return data.OrderBy(x => x.stateCode).ToList();
                default: return data;
            }
        }

    }
}
