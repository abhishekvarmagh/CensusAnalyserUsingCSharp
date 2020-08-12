using CensusAnalyserProblem.DAO;
using System;
using System.Collections.Generic;
using System.Text;
using static CensusAnalyserProblem.CensusAnalyser;

namespace CensusAnalyserProblem
{
    public class CensusAdapterFactory
    {
        public static Dictionary<string, CensusDataDAO> GetCensusAdapter(Country country, params string[] csvFilePath)
        {
            switch (country)
            {
                case CensusAnalyser.Country.INDIA:
                    return new IndiaCensusAdapter().LoadCensusData(csvFilePath);
                case CensusAnalyser.Country.US:
                    return new USCensusAdapter().LoadCensusData(csvFilePath);
                default:
                    throw new CensusAnalyserException("No Such Country", CensusAnalyserException.ExceptionType.NO_SUCH_COUNTRY);
            }
        }
    }
}
