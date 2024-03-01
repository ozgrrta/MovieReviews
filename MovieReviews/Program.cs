using Autofac;
using Autofac.Extensions.DependencyInjection;
using GraphQL;
using Microsoft.EntityFrameworkCore;
using MovieReviews.Database;
using MovieReviews.GraphQL;
using MovieReviews.Repository;

var builder = WebApplication.CreateBuilder(args);

// Use Autofac for dependency injection
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
		   .ConfigureContainer<ContainerBuilder>(containerBuilder =>
		   {
			   containerBuilder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
			   containerBuilder.RegisterType<MovieRepository>().As<IMovieRepository>().InstancePerLifetimeScope();
			   containerBuilder.RegisterType<QueryObject>().AsSelf().SingleInstance();
			   containerBuilder.RegisterType<MovieReviewSchema>().AsSelf().SingleInstance();
		   });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
	   .AddEntityFrameworkInMemoryDatabase()
	   .AddDbContext<MovieContext>(context => { context.UseInMemoryDatabase("MovieDb"); });

builder.Services
		.AddGraphQL(o =>
		{
			// Adds all graph types in the current assembly with a singleton lifetime.
			o.AddGraphTypes();
			// Add GraphQL data loader to reduce the number of calls to our repository. https://graphql-dotnet.github.io/docs/guides/dataloader/
			o.AddDataLoader();
			o.AddSystemTextJson();
		});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseGraphQL<MovieReviewSchema>("/graphql");

// Enables Altair UI at path /
app.UseGraphQLAltair(path: "/");

app.MapControllers();

app.Run();
