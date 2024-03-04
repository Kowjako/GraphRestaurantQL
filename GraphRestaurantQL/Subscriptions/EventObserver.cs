namespace GraphRestaurantQL.Subscriptions
{
    public class EventObserver : IDisposable
    {
        private List<IObserver<EventModel>> _observers;
        private IObserver<EventModel> _observer;

        public EventObserver(List<IObserver<EventModel>> observers, IObserver<EventModel> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}
