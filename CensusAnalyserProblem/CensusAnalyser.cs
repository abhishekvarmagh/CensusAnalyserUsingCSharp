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
    public class CensusAnalyser
    {

        public enum Country
        {
            INDIA, US
        }

        string[] censusData;
        Dictionary<string, CensusDataDAO> censusDataMap = new Dictionary<string, CensusDataDAO>();

        public Dictionary<string, CensusDataDAO> loadCensusData(Country country, params string[] csvFilePath)
        {
            return CensusAdapterFactory.GetCensusAdapter(country, csvFilePath);
        }

        public string getStateWiseSortedCensusData(Dictionary<string, CensusDataDAO> data, SortBy sortBy)
        {
            List<CensusDataDAO> list = SortFeild.SortCensusData(data.Select(x => x.Value).ToList(), sortBy); 
            return JsonConvert.SerializeObject(list);
        }

    }
}
