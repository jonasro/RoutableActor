using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            
            Repeat(() =>
            {
                var fetch = new Fetch { Id = random.Next(1, 19), OperationId = random.Next(1000, 100000) };
                FetchHandler.Handle(fetch);    
            });

            await Task.Delay(100000);
        }

        private void Repeat(Action action)
        {
            var actions = Enumerable.Repeat(action, 10000).ToArray();
            Parallel.Invoke(actions);
        }
    }
}
