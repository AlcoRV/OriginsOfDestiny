using Microsoft.EntityFrameworkCore;
using OriginsOfDestiny.Data;
using OriginsOfDestiny.Models.Dialogs;

namespace OriginsOfDestiny.Services
{
    public class DialogService : IDialogService
    {
        private readonly ApplicationDbContext _context;

        public DialogService(ApplicationDbContext context) {  _context = context; }

        public async Task<Dialog> Get(string id) => await _context.Dialogs.FirstOrDefaultAsync(d => d.Id == id);
    }
}
