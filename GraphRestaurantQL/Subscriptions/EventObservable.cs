namespace GraphRestaurantQL.Subscriptions
{
    public class EventObservable : IObservable<EventModel>
    {
        private List<IObserver<EventModel>> observers = new List<IObserver<EventModel>>();

        public IDisposable Subscribe(IObserver<EventModel> observer)
        {
            observers.Add(observer);
            return new EventObserver(observers, observer);
        }

        public void Publish(EventModel data)
        {
            observers.ForEach(p => p.OnNext(data));
        }
    }
}
