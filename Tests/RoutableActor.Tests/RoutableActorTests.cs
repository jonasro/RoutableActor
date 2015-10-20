using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RoutableActor.Tests
{
    [TestFixture]
    public class RoutableActorTests
    {
        [Test]
        public async void HandleMessage()
        {
            var random = new Random();
            
            Repeat(1000, () =>
            {
                var fetch = new Fetch { Id = random.Next(1, 20), OperationId = random.Next(1000, 100000) };
                FetchHandler.Handle(fetch);    
            });

            await Task.Delay(100000);
        }

        private void Repeat(int numberOfRepeats, Action action)
        {
            var actions = Enumerable.Repeat(action, numberOfRepeats).ToArray();
            Parallel.Invoke(actions);
        }
    }
}
