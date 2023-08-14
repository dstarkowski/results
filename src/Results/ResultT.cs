namespace Results;

public readonly struct Result<T>
{
	public Result(
		Outcome outcome,
		T? value)
	{
		Outcome = outcome;
		Value = value;
		Errors = ImmutableArray<String>.Empty;
	}

	public Result(
		Outcome outcome,
		ImmutableArray<String> errors)
	{
		Outcome = outcome;
		Value = default;
		Errors = errors;
	}

	public Result(
		Exception exception,
		Outcome outcome)
	{
		Outcome = outcome;
		Value = default;
		Errors = ImmutableArray.Create(exception.Message);
	}

	public Result(
		Exception exception)
	{
		Outcome = Outcome.Error;
		Value = default;
		Errors = ImmutableArray.Create(exception.Message);
	}

	public ImmutableArray<String> Errors { get; }

	public Boolean IsValid => Outcome == Outcome.Ok;

	public Outcome Outcome { get; }

	public T? Value { get; }

	public static implicit operator Result(Result<T> result) =>
		new(result.Outcome, result.Errors);

	public static implicit operator Result<T>(T returnValue) =>
		new(Outcome.Ok, returnValue);

	public static implicit operator Result<T>(Result result)
	{
		if (result.Outcome == Outcome.Ok)
			throw new InvalidCastException("Failed result cannot be cast to generic result.");

		return new(result.Outcome, result.Errors);
	}

	public static implicit operator Result<T>(Exception exception) =>
		new(exception);

	public static Boolean operator !=(Result<T> result, Outcome outcome) =>
		result.Outcome != outcome;

	public static Boolean operator ==(Result<T> result, Outcome outcome) =>
		result.Outcome == outcome;
}