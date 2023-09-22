using AFS.Data;
using AFS.Models;
using System;
using System.Collections.Generic;

namespace AFS.Repositories
{
    public interface IAfsRepository : IDisposable
    {
        IEnumerable<spSelectAllLeetSpeekTexts> GetAllTranslatedData();
        void AddInputText(ContentModel entity);
        void Save();
    }
}
