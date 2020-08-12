using CensusAnalyserProblem.DAO;
using CensusAnalyserProblem.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CensusAnalyserProblem
{
    public class IndiaCensusAdapter : CensusAdapter
    {
        public Dictionary<string, CensusDataDAO> censusMap;
        public override Dictionary<string, CensusDataDAO> LoadCensusData(params string[] csvFilePath)
        {
            censusMap = base.LoadCountryCensusData(csvFilePath[0], "State,Population,AreaInSqKm,DensityPerSqKm");
            return this.LoadIndianStateCode(censusMap, csvFilePath[1]);
        }

        public Dictionary<string, CensusDataDAO> LoadIndianStateCode(Dictionary<string, CensusDataDAO> censusMap, string csvFilePath)
        {
            ICSVBuilder iCSVBuilder = CSVFactory.CreateBuilder();
            string[] censusData = iCSVBuilder.LoadCensusData(csvFilePath, "SrNo,State Name,TIN,StateCode");
            Dictionary<string, IndianStateCode> stateCodeMap = new Dictionary<string, IndianStateCode>();
            foreach(string records in censusData.Skip(1))
            {
                string[] column = records.Split(",");
                stateCodeMap.Add(column[1], new IndianStateCode(Convert.ToInt32(column[0]), column[1], Convert.ToInt32(column[2]), column[3]));
            }
            foreach(var indianStateKey in censusMap.Keys)
            {
                foreach(var indianStateCodeKey in stateCodeMap.Keys)
                {
                    if(indianStateKey == indianStateCodeKey)
                    {
                        censusMap[indianStateKey].stateCode = stateCodeMap[indianStateCodeKey].stateCode;
                        break;
                    }
                }
            }
            return censusMap;
        }
        
    }
}
