using System;
using System.Threading.Tasks;
using Orchestrator.Commands;
using Rebus.Bus;
using Rebus.Sagas;
using Shared.Messages.IntegrationEvents;

namespace Orchestrator
{
    public class DemoSaga : Saga<DemoSagaData>, IDemoSaga
    {
        private readonly IBus bus;

        public DemoSaga(IBus bus)
        {
            this.bus = bus;
        }

        public async Task Handle(ServiceOneFinishedEvent message)
        {
            Console.WriteLine("Service one has been finished");
            await bus.Publish(new ServiceOneCompletedEvent { AggregateId = message.AggregateId });
            
            Data.ServiceOneFinished = true;

            ProcessSaga();
        }

        public async Task Handle(ServiceTwoFinishedEvent message)
        {
            Console.WriteLine("Service two has been finished");
           
            Data.ServiceTwoFinished = true;

            ProcessSaga();
        }

        public async Task Handle(StartSagaCommand message)
        {
            Console.WriteLine("Saga started");
            await bus.Publish(new SagaStartedEvent() { AggregateId = message.AggregateId });

            Data.SagaStarted = true;

            ProcessSaga();
        }

        protected override void CorrelateMessages(ICorrelationConfig<DemoSagaData> config)
        {
            config.Correlate<StartSagaCommand>(m => m.AggregateId, d => d.Id);
            config.Correlate<ServiceOneFinishedEvent>(m => m.AggregateId, d => d.Id);
            config.Correlate<ServiceTwoFinishedEvent>(m => m.AggregateId, d => d.Id);
        }

        private void ProcessSaga()
        {
            if (Data.IsSagaComplete)
            {
                Console.WriteLine("Saga complete!");
                MarkAsComplete();
            }
        }
    }
}

