namespace Results;

public class ClassResult<T> : ClassResult
{
	public ClassResult(
		Outcome outcome,
		T? value)
		: base(outcome)
	{
		Value = value;
	}

	public ClassResult(
		Outcome outcome,
		ImmutableArray<String> errors)
		: base(outcome, errors)
	{
		Value = default;
	}

	public ClassResult(
		Exception exception,
		Outcome outcome)
		: base(outcome, exception)
	{
		Value = default;
	}

	public ClassResult(
		Exception exception)
		: base(exception)
	{
		Value = default;
	}

	public T? Value { get; }

	public static implicit operator ClassResult(ClassResult<T> result) =>
		new(result.Outcome, result.Errors);

	public static implicit operator ClassResult<T>(T returnValue) =>
		new(Outcome.Ok, returnValue);

	public static implicit operator ClassResult<T>(ClassResult result)
	{
		if (result.Outcome == Outcome.Ok)
			throw new InvalidCastException("Failed result cannot be cast to generic result.");

		return new(result.Outcome, result.Errors);
	}

	public static implicit operator ClassResult<T>(Exception exception) =>
		new(exception);

	public static Boolean operator !=(ClassResult<T> result, Outcome outcome) =>
		result.Outcome != outcome;

	public static Boolean operator ==(ClassResult<T> result, Outcome outcome) =>
		result.Outcome == outcome;
}