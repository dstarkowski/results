namespace Results;

public readonly struct Result
{
	public Result(
		Outcome outcome)
	{
		Outcome = outcome;
		Errors = ImmutableArray<String>.Empty;
	}

	public Result(
		Outcome outcome,
		String error)
	{
		Outcome = outcome;
		Errors = ImmutableArray.Create(error);
	}

	public Result(
		Outcome outcome,
		ImmutableArray<String> errors)
	{
		Outcome = outcome;
		Errors = errors;
	}

	public Result(
		Outcome outcome,
		Exception exception)
	{
		Outcome = outcome;
		Errors = ImmutableArray.Create(exception.Message);
	}

	public Result(
		Exception exception)
	{
		Outcome = Outcome.Error;
		Errors = ImmutableArray.Create(exception.Message);
	}

	public static Result Conflict =>
		new(Outcome.Conflict);

	public static Result Forbidden =>
		new(Outcome.Forbidden);

	public static Result Invalid =>
		new(Outcome.Invalid);

	public static Result NotFound =>
		new(Outcome.NotFound);

	public static Result Ok =>
		new(Outcome.Ok);

	public ImmutableArray<String> Errors { get; init; }

	public Boolean IsValid => Outcome == Outcome.Ok;

	public Outcome Outcome { get; }

	public static implicit operator Result(Exception exception) =>
		new(exception);

	public static Boolean operator !=(Result result, Outcome outcome) =>
		result.Outcome != outcome;

	public static Boolean operator ==(Result result, Outcome outcome) =>
		result.Outcome == outcome;
}