using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HelpDeskApp.Shared
{
    public interface IFileStorage
    {
        Task Save(Stream fileStream, string name);
        Task Delete(string name);
        Task<IEnumerable<string>> GetNames();
        Task<Stream> Load(string name);
    }
}
