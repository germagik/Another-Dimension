using System;

namespace Utils {
    [Serializable]
    public class Probability
    {
        public ProbabilityLevel grade;
    }

    public enum ProbabilityLevel
    {
        VeryHigh, High, Medium, Low, VeryLow
    }
}
