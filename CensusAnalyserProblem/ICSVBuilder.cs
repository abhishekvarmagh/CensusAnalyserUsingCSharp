using CensusAnalyserProblem.DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CensusAnalyserProblem
{
    public interface ICSVBuilder
    {
        string[] LoadCensusData(string csvFilePath, string header);
    }
}
