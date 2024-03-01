using GraphQL.Types;
using GraphRestaurantQL.Services;
using GraphRestaurantQL.Type;

namespace GraphRestaurantQL.Mutations
{
    public class AuthMutations : ObjectGraphType
    {
        public AuthMutations(ITokenService tokenSvc)
        {
            Field<TokenType>("login").Resolve(ctx =>
            {
                return tokenSvc.GetToken();
            });
        }
    }
}
