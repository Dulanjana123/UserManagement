using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagemnt.Data;
using UserManagemnt.Models.MasterData;

namespace UserManagemnt.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly PMSDbContext _pMSDbContext;

        public EmailRepository(PMSDbContext pMSDbContext)
        {
            _pMSDbContext = pMSDbContext;
        }
        public async Task<IEnumerable<Email>> GetAllAsync()
        {
            return await _pMSDbContext.Email.
                ToListAsync();
        }
    }
}
