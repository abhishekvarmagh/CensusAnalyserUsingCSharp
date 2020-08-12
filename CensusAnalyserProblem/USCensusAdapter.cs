using CensusAnalyserProblem.DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CensusAnalyserProblem
{
    public class USCensusAdapter : CensusAdapter
    {
        public override Dictionary<string, CensusDataDAO> LoadCensusData(params string[] csvFilePath)
        {
            return base.LoadCountryCensusData(csvFilePath[0], "State Id,State,Population,Housing units,Total area,Water area,Land area,Population Density,Housing Density");
        }
    }
}
