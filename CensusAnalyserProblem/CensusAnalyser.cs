using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using CensusAnalyserProblem.DAO;
using static CensusAnalyserProblem.SortFeild;

namespace CensusAnalyserProblem
{
    public class CensusAnalyser
    {

        public enum Country
        {
            INDIA, US
        }

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
