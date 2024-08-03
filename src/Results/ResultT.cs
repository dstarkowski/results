namespace Results;

/*
 result extensions -> create ProbelmDetails using Results.Problem()
 convert task with ContinueWith()
*/

public readonly struct Result<T>
{
	private Result(
		Outcome outcome,
		T? value,
		ImmutableArray<String> errors)
	{
		Outcome = outcome;
		Value = value;
		Errors = errors;
	}

	public ImmutableArray<String> Errors { get; }

	public Boolean IsValid => Outcome == Outcome.Ok;

	public Outcome Outcome { get; }

	public T? Value { get; }

	public static implicit operator Result(Result<T> result) =>
		new(result.Outcome, result.Errors);

	public static implicit operator Result<T>(T value) =>
		new(Outcome.Ok, value, []);

	public static implicit operator Result<T>(Result result)
	{
		if (result.Outcome == Outcome.Ok)
			throw new InvalidCastException("Failed result cannot be cast to generic result.");

		return new(result.Outcome, default, result.Errors);
	}

	public static implicit operator Result<T>(Exception exception) =>
		new(Outcome.Error, default, [exception.Message]);

	public static implicit operator Result<T>((Result Result, ImmutableArray<String> Errors) tuple) =>
		new(tuple.Result.Outcome, default, tuple.Errors);

	public static implicit operator Result<T>((Result Result, String Error) tuple) =>
		new(tuple.Result.Outcome, default, [tuple.Error]);

	public static implicit operator Result<T>((Result Result, Exception Exception) tuple) =>
		new(tuple.Result.Outcome, default, [tuple.Exception.Message]);

	public static Boolean operator !=(Result<T> result, Outcome outcome) =>
		result.Outcome != outcome;

	public static Boolean operator ==(Result<T> result, Outcome outcome) =>
		result.Outcome == outcome;
}