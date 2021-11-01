using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using immersed.dive.shop.model;

namespace immersed.dive.shop.domain.interfaces
{
    public interface IPersonService
    {
        Task<Person> Get(Guid id);
        Task Add(Person person);
        Task<IList<Person>> GetAll();
    }
}