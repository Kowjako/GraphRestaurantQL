using System.Reflection;
using System.Text;
using GraphQL;
using GraphQL.MicrosoftDI;
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

builder.Services.AddSingleton<ISchema, RootSchema>(sp => new(new SelfActivatingServiceProvider(sp)));

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
