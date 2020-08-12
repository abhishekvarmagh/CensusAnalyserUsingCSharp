using CensusAnalyserProblem.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CensusAnalyserProblem
{
    public class SortFeild
    {
        public enum SortBy
        {
            STATE, STATE_CODE, POPULATION, POPULATION_DENSITY, TOTAL_AREA
        }
        public static List<CensusDataDAO> SortCensusData(List<CensusDataDAO> data, SortBy sortBy)
        { 
            switch(sortBy)
            {
                case SortBy.STATE: return data.OrderBy(x => x.state).ToList();
                case SortBy.STATE_CODE: return data.OrderBy(x => x.stateCode).ToList();
                case SortBy.POPULATION: return data.OrderByDescending(x => x.population).ToList();
                case SortBy.POPULATION_DENSITY: return data.OrderByDescending(x => x.density).ToList();
                case SortBy.TOTAL_AREA: return data.OrderByDescending(x => x.area).ToList();
                default: return data;
            }
        }
    }
}
