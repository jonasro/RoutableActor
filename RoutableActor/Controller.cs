using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace RoutableActor
{
    public class Controller
    {
        private int _controllerId;
        private ConcurrentQueue<Fetch> _fetchQueue;
        private Random _random;
        private int _messagesProcessed
            ;

        public Controller(int controllerId)
        {
            _fetchQueue = _fetchQueue ?? new ConcurrentQueue<Fetch>();
            _controllerId = controllerId;
            _random = _random ?? new Random();

            ProcessQueue();
        }

        private void ProcessQueue()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    Fetch fetch;
                    if (_fetchQueue.TryDequeue(out fetch))
                    {
                        Console.WriteLine("Processing fetch for controller {0}, operation id {1}", fetch.Id,
                              fetch.OperationId);
                        _messagesProcessed++;

                        Console.WriteLine("Completed processing on operation {0}, messages processed on controller {1} = {2}", fetch.OperationId,
                            fetch.Id, _messagesProcessed);
                    }

                    Thread.Sleep(100);
                }
            });
        }

        public void Handle(Fetch fetch)
        {
            _fetchQueue.Enqueue(fetch);
            Console.WriteLine("Handle fetch for controller {0}", _controllerId);
        }
    }
}
