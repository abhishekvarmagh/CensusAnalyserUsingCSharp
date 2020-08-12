using System;
using System.Collections.Generic;
using System.Text;

namespace CensusAnalyserProblem
{
    public class CSVFactory
    {
        public static ICSVBuilder CreateBuilder()
        {
            return new CSVDataReader();
        }
    }
}
