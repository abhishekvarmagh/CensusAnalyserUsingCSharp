using CensusAnalyserProblem.DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CensusAnalyserProblem
{
    public interface ICSVBuilder
    {
        Dictionary<string, CensusDataDAO> loadIndiaCensusData(string csvFilePath, string header);
    }
}
