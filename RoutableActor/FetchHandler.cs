using System.Collections.Concurrent;

namespace RoutableActor
{
    public static class FetchHandler
    {
        private static ConcurrentDictionary<int, Controller> _controllers;

        static FetchHandler()
        {
            _controllers = _controllers ?? new ConcurrentDictionary<int, Controller>();
        }

        public static void Handle(Fetch fetch)
        {
            var controller = GetController(fetch);
            controller.Handle(fetch);
        }

        private static Controller GetController(Fetch fetch)
        {
            Controller controller;
            if (!_controllers.TryGetValue(fetch.Id, out controller))
            {
                controller = new Controller(fetch.Id);
                _controllers.TryAdd(fetch.Id, controller);
            }

            return controller;
        }
    }
}
