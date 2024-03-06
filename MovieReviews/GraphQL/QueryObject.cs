using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Types;
using MovieReviews.GraphQL.Types;
using MovieReviews.Repository;

namespace MovieReviews.GraphQL
{
	public class QueryObject : ObjectGraphType<object>
	{
		public QueryObject(IMovieRepository movieRepository)
		{
			AddField(new FieldType
			{
				Name = "movies",
				Type = typeof(ListGraphType<MovieObject>),
				Resolver = new FuncFieldResolver<object>(async context =>
				{
					return await movieRepository.GetMoviesAsync();
				})
			});

			AddField(new FieldType
			{
				Name = "movie",
				Type = typeof(MovieObject),
				Arguments = new QueryArguments(
					new QueryArgument<NonNullGraphType<GuidGraphType>> { Name = "id" }
				),
				Resolver = new FuncFieldResolver<object>(async context =>
				{
					return await movieRepository.GetMovieByIdAsync(context.GetArgument("id", Guid.Empty));
				})
			});
		}
	}
}
