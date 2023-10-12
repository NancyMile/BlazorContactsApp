using BlazorApp2.Shared;

namespace BlazorApp2.Client.Services
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAll();
        Task<Contact> GetDetails(int id);
        Task Save(Contact contact);
        Task Delete(int id);
    }
}
