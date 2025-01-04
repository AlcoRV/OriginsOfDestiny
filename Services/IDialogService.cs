using OriginsOfDestiny.Models.Dialogs;

namespace OriginsOfDestiny.Services
{
    public interface IDialogService
    {
        public Task<Dialog> Get(string id);
    }
}
