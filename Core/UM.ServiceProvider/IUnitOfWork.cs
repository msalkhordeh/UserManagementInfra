namespace UM.ServiceProvider
{
    public interface IUnitOfWork
    {
        int Complete();

        Task<int> CompleteAsync(
            CancellationToken cancellationToken = default);
    }
}
