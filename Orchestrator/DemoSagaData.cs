using System;
using Rebus.Sagas;

namespace Orchestrator
{
    public class DemoSagaData : SagaData
    {
        public bool SagaStarted { get; set; }
        public bool ServiceOneFinished { get; set; }
        public bool ServiceTwoFinished { get; set; }
        public bool IsSagaComplete => SagaStarted && ServiceOneFinished && ServiceTwoFinished;
    }
}

