using ContactsManager.Shared;
using System.Net.Http.Json;

namespace ContactsManager.Client.Services
{
    public class ContactService : IContactService
    {
        private readonly HttpClient _httpClient;

        public ContactService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteContact(int id)
        {
            await _httpClient.DeleteAsync($"api/contacts/{id}");
        }

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Contact>>("api/contacts");
        }

        public async Task<Contact> GetContactById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Contact>($"api/contacts/{id}");
        }

        public async Task SaveContact(Contact contact)
        {
            if (contact.Id == 0)
            {
                //Insert
                await _httpClient.PostAsJsonAsync<Contact>($"api/contacts", contact);
            }
            else
            {
                //Update
                await _httpClient.PutAsJsonAsync<Contact>($"api/contacts/{contact.Id}", contact);
            }
        }
    }
}
