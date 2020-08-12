using System;
using System.Collections.Generic;
using System.Text;

namespace CensusAnalyserProblem.POCO
{
    public class USCensus
    {
        public string stateCode;
        public string state;
        public int population;
        public int housingUnits;
        public double totalArea;
        public double waterArea;
        public double landArea;
        public double density;
        public double populationDensity;

        public USCensus(string stateCode, string state, int population, int housingUnits, double totalArea, double waterArea, double landArea, double density, double populationDensity)
        {
            this.stateCode = stateCode;
            this.state = state;
            this.population = population;
            this.housingUnits = housingUnits;
            this.totalArea = totalArea;
            this.waterArea = waterArea;
            this.landArea = landArea;
            this.density = density;
            this.populationDensity = populationDensity;
         }
    }
}
