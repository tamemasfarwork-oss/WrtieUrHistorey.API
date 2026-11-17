using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepo
{
    public interface IFileStorage
    {
        Task<string> SaveAsync(Stream content, string extension, string folder, CancellationToken ct);
        Task DeleteAsync(string relativePath, CancellationToken ct);
    }
}
