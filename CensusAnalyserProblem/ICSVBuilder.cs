using System;
using System.Collections.Generic;
using System.Text;

namespace CensusAnalyserProblem
{
    public interface ICSVBuilder
    {
        object loadIndiaCensusData(string csvFilePath, string header);
    }
}
