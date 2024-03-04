using GraphQL.Types;

namespace GraphRestaurantQL.Subscriptions
{
    public class OrderSubscription : ObjectGraphType
    {
        public OrderSubscription(IEventService eventService)
        {
            Field<EventModelType>("orderCreated")
                .ResolveStream(ctx =>
                {
                    return eventService.Observable;
                });
        }
    }
}
