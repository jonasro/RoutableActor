namespace RoutableActor
{
    public static class FetchHandler
    {
        public static void Handle(Fetch fetch)
        {
            var controller = ControllerFactory.GetController(fetch.ControllerId);
            controller.Handle(fetch);

            controller.ResetEvent.WaitOne();
        }
    }
}
