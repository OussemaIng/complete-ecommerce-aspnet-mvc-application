using eTickets.Models;
using System.Collections.Generic;

namespace eTickets.Data.Services
{
    public interface IActorsService
    {
        IEnumerable<Actor> GetAll();
        Actor GetById(int id);
        void Add(Actor actor);
        Actor Updat(int id ,Actor newActor);
        void Delete(int id);
    }
}
