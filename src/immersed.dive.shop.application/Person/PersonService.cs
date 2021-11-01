using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using immersed.dive.shop.domain.interfaces;
using immersed.dive.shop.domain.interfaces.Data;
using immersed.dive.shop.model;

namespace immersed.dive.shop.application
{
    public class PersonService : IPersonService
    {
        private IDataStore<Person> _personDataStore;

        public PersonService(IDataStore<Person> personDataStore)
        {
            _personDataStore = personDataStore;
        }

        public async Task<Person> Get(Guid id)
        {
            return await _personDataStore.FindAsync(c => c.Id == id);
        }

        public async Task Add(Person person)
        {
            await _personDataStore.AddAsync(person);
        }

        public async Task<IList<Person>> GetAll()
        {
            return await _personDataStore.GetAllAsync();
        }
    }
}
