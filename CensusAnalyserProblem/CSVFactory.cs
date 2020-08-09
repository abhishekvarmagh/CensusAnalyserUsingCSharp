using System;
using System.Collections.Generic;
using System.Text;

namespace CensusAnalyserProblem
{
    public class CSVFactory
    {
        public ICSVBuilder getCensusAnalyser()
        {
            return new CensusAnalyser();
        }
    }
}
