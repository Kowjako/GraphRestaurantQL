using GraphQL.Types;

namespace GraphRestaurantQL.Subscriptions
{
    public class EventModelType : ObjectGraphType<EventModel>
    {
        public EventModelType()
        {
            Field(_ => _.Id);
            Field(_ => _.Description);
        }
    }

    public class EventModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public interface IEventService
    {
        IObservable<EventModel> Observable { get; }

        void Push(EventModel data);
    }

    public class EventService : IEventService
    {
        private readonly EventObservable _subject;

        public EventService()
        {
            _subject = new EventObservable();
        }

        public void Push(EventModel data) => _subject.Publish(data);

        public IObservable<EventModel> Observable => _subject;
    }
}
