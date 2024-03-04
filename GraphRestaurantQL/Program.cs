using System.Reflection;
using System.Text;
using GraphQL;
using GraphQL.Types;
using GraphRestaurantQL.Data;
using GraphRestaurantQL.Interfaces;
using GraphRestaurantQL.Mutations;
using GraphRestaurantQL.Query;
using GraphRestaurantQL.Schema;
using GraphRestaurantQL.Services;
using GraphRestaurantQL.Subscriptions;
using GraphRestaurantQL.Type;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GraphQLDbContext>(dbc =>
{
    dbc.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddSingleton<IEventService, EventService>();

// Register all Graph Types
builder.Services.AddSingleton<MenuType>();
builder.Services.AddSingleton<CategoryType>();
builder.Services.AddSingleton<ReservationType>();
builder.Services.AddSingleton<TokenType>();
builder.Services.AddSingleton<EventModelType>();

builder.Services.AddSingleton<MenuInputType>();
builder.Services.AddSingleton<CategoryInputType>();
builder.Services.AddSingleton<ReservationInputType>();

// Register all Queries - also graph types
builder.Services.AddSingleton<MenuQuery>();
builder.Services.AddSingleton<CategoryQuery>();
builder.Services.AddSingleton<ReservationQuery>();
builder.Services.AddSingleton<RootQuery>();

// Register all Mutations - also graph types
builder.Services.AddSingleton<CategoryMutations>();
builder.Services.AddSingleton<MenuMutations>();
builder.Services.AddSingleton<ReservationMutations>();
builder.Services.AddSingleton<AuthMutations>();
builder.Services.AddSingleton<RootMutation>();

builder.Services.AddSingleton<OrderSubscription>();

builder.Services.AddSingleton<ISchema, RootSchema>();

builder.Services.AddGraphQL(b =>
{
    b.AddAutoSchema<ISchema>();
    b.AddSystemTextJson();
    b.AddAuthorizationRule(); // enable authroization on operations level, but introspect query (schema discovery)
    // can go without it, so Graphiql will produce documentation without problems
    b.AddFormFileGraphType(); // allow to upload files, FormFileGraphType automapped to IFormFile
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var secKey = Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"]!);
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secKey),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseGraphQLGraphiQL("/ui/graphiql");
}

app.UseAuthentication();
app.UseAuthorization();

app.UseWebSockets(); 
app.UseGraphQL("/graphql");

app.Run();
