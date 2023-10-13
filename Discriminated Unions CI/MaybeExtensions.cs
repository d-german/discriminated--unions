namespace Discriminated_Unions_CI;

public static class MaybeExtensions
{
    public static Maybe<TOut> Bind<TIn, TOut>(this Maybe<TIn> @this, Func<TIn, TOut> f)
    {
        try
        {
            Maybe<TOut> updatedValue = @this switch
            {
                Something<TIn> s when !EqualityComparer<TIn>.Default.Equals(s.Value, default) => new Something<TOut>(f(s.Value)),
                Something<TIn> _ => new Nothing<TOut>(),
                Nothing<TIn> _ => new Nothing<TOut>(),
                Error<TIn> e => new Error<TOut>(e.CapturedError),
                _ => new Error<TOut>(new Exception(
                    "New Maybe state that isn't coded for!: " +
                    @this.GetType()))
            };
            return updatedValue;
        }
        catch (Exception e)
        {
            return new Error<TOut>(e);
        }
    }
}