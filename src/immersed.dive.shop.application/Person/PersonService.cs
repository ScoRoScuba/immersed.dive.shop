using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using immersed.dive.shop.domain.interfaces;
using immersed.dive.shop.domain.interfaces.Data;
using Serilog;

namespace immersed.dive.shop.application.Person;

public class PersonService : IPersonService
{
    private IDataStore<model.Person> _personDataStore;
    private readonly ILogger _logger;

    public PersonService(IDataStore<model.Person> personDataStore, ILogger logger)
    {
        _personDataStore = personDataStore;
        _logger = logger;
    }

    public async Task<model.Person> Get(Guid id)
    {
        return await _personDataStore.FindAsync(c => c.Id == id);
    }

    public async Task Add(model.Person person)
    {
        await _personDataStore.AddAsync(person);
    }

    public async Task<IList<model.Person>> GetAll()
    {
        return await _personDataStore.GetAllAsync();
    }
}