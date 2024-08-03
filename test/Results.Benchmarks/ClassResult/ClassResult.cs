namespace Results;

public class ClassResult
{
	public ClassResult(
		Outcome outcome)
	{
		Outcome = outcome;
		Errors = ImmutableArray<String>.Empty;
	}

	public ClassResult(
		Outcome outcome,
		String error)
	{
		Outcome = outcome;
		Errors = ImmutableArray.Create(error);
	}

	public ClassResult(
		Outcome outcome,
		ImmutableArray<String> errors)
	{
		Outcome = outcome;
		Errors = errors;
	}

	public ClassResult(
		Outcome outcome,
		Exception exception)
	{
		Outcome = outcome;
		Errors = ImmutableArray.Create(exception.Message);
	}

	public ClassResult(
		Exception exception)
	{
		Outcome = Outcome.Error;
		Errors = ImmutableArray.Create(exception.Message);
	}

	public static ClassResult Conflict =>
		new(Outcome.Conflict);

	public static ClassResult Forbidden =>
		new(Outcome.Forbidden);

	public static ClassResult Invalid =>
		new(Outcome.Invalid);

	public static ClassResult NotFound =>
		new(Outcome.NotFound);

	public static ClassResult Ok =>
		new(Outcome.Ok);

	public ImmutableArray<String> Errors { get; init; }

	public Boolean IsValid => Outcome == Outcome.Ok;

	public Outcome Outcome { get; }

	public static implicit operator ClassResult(Exception exception) =>
		new(exception);

	public static Boolean operator !=(ClassResult result, Outcome outcome) =>
		result.Outcome != outcome;

	public static Boolean operator ==(ClassResult result, Outcome outcome) =>
		result.Outcome == outcome;


}