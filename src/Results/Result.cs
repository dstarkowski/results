namespace Results;

public readonly struct Result
{
	internal Result(
		Outcome outcome,
		ImmutableArray<String> errors)
	{
		Outcome = outcome;
		Errors = errors;
	}

	public static Result Conflict =>
		new(Outcome.Conflict, []);

	public static Result Forbidden =>
		new(Outcome.Forbidden, []);

	public static Result Invalid =>
		new(Outcome.Invalid, []);

	public static Result NotFound =>
		new(Outcome.NotFound, []);

	public static Result Ok =>
		new(Outcome.Ok, []);

	public ImmutableArray<String> Errors { get; init; }

	public Boolean IsValid => Outcome == Outcome.Ok;

	public Outcome Outcome { get; }

	public static implicit operator Result(Exception exception) =>
		new(Outcome.Error, [exception.Message]);

	public static implicit operator Result((Result Result, ImmutableArray<String> Errors) tuple) =>
		new(tuple.Result.Outcome, tuple.Errors);

	public static implicit operator Result((Result Result, String Error) tuple) =>
		new(tuple.Result.Outcome, [tuple.Error]);

	public static implicit operator Result((Result Result, Exception Exception) tuple) =>
		new(tuple.Result.Outcome, [tuple.Exception.Message]);

	public static Boolean operator !=(Result result, Outcome outcome) =>
		result.Outcome != outcome;

	public static Boolean operator ==(Result result, Outcome outcome) =>
		result.Outcome == outcome;
}