namespace CoreLayer.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();

        void SaveChanges();
    }
}
