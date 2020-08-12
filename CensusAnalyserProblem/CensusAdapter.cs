using CensusAnalyserProblem.DAO;
using CensusAnalyserProblem.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CensusAnalyserProblem
{
    public abstract class CensusAdapter
    {
        public abstract Dictionary<string, CensusDataDAO> LoadCensusData(params string[] csvFilePath);

        public Dictionary<string, CensusDataDAO> LoadCountryCensusData(string csvFilePath, string header)
        {
            ICSVBuilder iCSVBuilder = CSVFactory.CreateBuilder();
            string[] censusData = iCSVBuilder.LoadCensusData(csvFilePath, header);
            Dictionary<string, CensusDataDAO> censusMap = new Dictionary<string, CensusDataDAO>();
            if (csvFilePath.Contains("IndiaStateCensusData.csv"))
            {
                foreach (string records in censusData.Skip(1))
                {
                    string[] column = records.Split(",");
                    censusMap.Add(column[0], new CensusDataDAO(new IndianCensus(column[0], column[1], column[2], column[3])));
                }
            }
            else if (csvFilePath.Contains("USCensusData.csv"))
            {
                foreach (string records in censusData.Skip(1))
                {
                    string[] column = records.Split(",");
                    censusMap.Add(column[1], new CensusDataDAO(new USCensus(column[0], column[1], Convert.ToInt32(column[2]), Convert.ToInt32(column[3]), Convert.ToDouble(column[4]), Convert.ToDouble(column[5]), Convert.ToDouble(column[6]), Convert.ToDouble(column[7]), Convert.ToDouble(column[8]))));
                }
            }
            return censusMap;
        }
    }
}
