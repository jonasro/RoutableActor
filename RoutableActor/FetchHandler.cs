namespace RoutableActor
{
    public static class FetchHandler
    {
        public static void Handle(Fetch fetch)
        {
            var controller = ControllerFactory.GetController(fetch.Id);
            controller.Handle(fetch);
        }
    }
}
