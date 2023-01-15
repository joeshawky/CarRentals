using Business.Concrete;
using Core.Entities.Concrete;
using Core.Utilities.Comparer;
using Core.Utilities.Results;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

namespace ReCapProject.ConsoleLayer
{
    class program
    {
        static void Main(string[] args)
        {
            var user = new User
            {
                Email = "123",
                Id = 1
            };

            var user1 = new User
            {
                Id = 1,
                Email = "1234"
                
            };

        }

    }
}