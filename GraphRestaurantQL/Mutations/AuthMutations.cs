using GraphQL;
using GraphQL.Server.Transports.AspNetCore;
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

            Field<StringGraphType>("uploadProfileImage").Argument<FormFileGraphType>("file").Resolve(ctx =>
            {
                var blobFile = ctx.GetArgument<IFormFile>("file");
                return "Ok";
            });
        }
    }
}
