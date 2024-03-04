using System.Reactive.Linq;
using System.Reactive.Subjects;
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
        private readonly ISubject<EventModel> _subject;
        private Guid Id { get; set; }

        public EventService()
        {
            Id = Guid.NewGuid();
            _subject = new ReplaySubject<EventModel>(1);
        }

        public void Push(EventModel data) => _subject.OnNext(data);

        public IObservable<EventModel> Observable => _subject.AsObservable();
    }
}
