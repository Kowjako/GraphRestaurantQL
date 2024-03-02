using System.Text;
using GraphQL;
using GraphQL.Server.Ui.GraphiQL;
using GraphQL.Types;
using GraphRestaurantQL.Data;
using GraphRestaurantQL.Interfaces;
using GraphRestaurantQL.Mutations;
using GraphRestaurantQL.Query;
using GraphRestaurantQL.Schema;
using GraphRestaurantQL.Services;
using GraphRestaurantQL.Type;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GraphQLDbContext>(dbc =>
{
    dbc.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IMenuRepository, MenuRepository>();
builder.Services.AddTransient<IReservationRepository, ReservationRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

// Register all Graph Types
builder.Services.AddTransient<MenuType>();
builder.Services.AddTransient<CategoryType>();
builder.Services.AddTransient<ReservationType>();
builder.Services.AddTransient<TokenType>();

builder.Services.AddTransient<MenuInputType>();
builder.Services.AddTransient<CategoryInputType>();
builder.Services.AddTransient<ReservationInputType>();

// Register all Queries - also graph types
builder.Services.AddTransient<MenuQuery>();
builder.Services.AddTransient<CategoryQuery>();
builder.Services.AddTransient<ReservationQuery>();
builder.Services.AddTransient<RootQuery>();

// Register all Mutations - also graph types
builder.Services.AddTransient<CategoryMutations>();
builder.Services.AddTransient<MenuMutations>();
builder.Services.AddTransient<ReservationMutations>();
builder.Services.AddTransient<AuthMutations>();
builder.Services.AddTransient<RootMutation>();

builder.Services.AddTransient<ISchema, RootSchema>();

builder.Services.AddGraphQL(b =>
{
    b.AddAutoSchema<ISchema>();
    b.AddSystemTextJson();
    b.AddFormFileGraphType(); // allow to upload files, FormFileGraphType automapped to IFormFile
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var secKey = Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"]!);
                    options.TokenValidationParameters = new TokenValidationParameters()
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
    app.UseGraphQLGraphiQL("/ui/graphiql", new GraphiQLOptions()
    {
        Headers = new Dictionary<string, string>()
        {
            ["Authorization"] = "pass here jwt token, so GraphiQl has access to /graphql"
        }
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.UseGraphQL("/graphql", x =>
{
    x.AuthorizationRequired = true;
    x.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
});

app.Run();
