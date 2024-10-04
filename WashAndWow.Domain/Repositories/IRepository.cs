namespace Wash_Wow.Domain.Repositories
{
    public interface IRepository<in TDomain>
    {
        void Add(TDomain entity);
        void Update(TDomain entity);
        void Remove(TDomain entity);
    }
}
