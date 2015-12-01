using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
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
                var fetch = new Fetch { ControllerId = random.Next(1, 20), OperationId = random.Next(1000, 100000) };
                FetchHandler.Handle(fetch);    
            });

            await Task.Delay(100000);
        }

        [Test]
        public void GetControllerFromfactory()
        {
            var controller = ControllerFactory.GetController(2);

            controller.Should().NotBeNull();
            ControllerFactory.Controllers.Count.Should().Be(1);

            ControllerFactory.GetController(1);
            ControllerFactory.GetController(6);
            ControllerFactory.GetController(4);

            ControllerFactory.Controllers.Count.Should().Be(4);
        }

        
        private void Repeat(int numberOfRepeats, Action action)
        {
            var actions = Enumerable.Repeat(action, numberOfRepeats).ToArray();
            Parallel.Invoke(actions);
        }
    }
}
