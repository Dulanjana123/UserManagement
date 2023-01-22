using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagemnt.Models.MasterData;

namespace UserManagemnt.Repositories
{
    public interface IEmailRepository
    {
        Task<IEnumerable<Email>> GetAllAsync();
    }
}
