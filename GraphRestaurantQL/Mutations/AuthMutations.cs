using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.Server.Transports.AspNetCore;
using GraphQL.Types;
using GraphRestaurantQL.Services;
using GraphRestaurantQL.Type;

namespace GraphRestaurantQL.Mutations
{
    public class AuthMutations : ObjectGraphType
    {
        public AuthMutations()
        {
            Field<TokenType, string>("login")
                .ResolveScoped(ctx =>
            {
                var tokenSvc = ctx.RequestServices!.GetRequiredService<ITokenService>();
                return tokenSvc.GetToken();
            });

            Field<StringGraphType>("uploadProfileImage")
                .Argument<FormFileGraphType>("file")
                .Resolve(ctx =>
            {
                var blobFile = ctx.GetArgument<IFormFile>("file");
                return "Ok";
            });
        }
    }
}
