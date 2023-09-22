using AFS.Data;
using AFS.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AFS.Repositories
{
    public class AfsRepository : IAfsRepository, IDisposable
    {
        private readonly AFS_DBEntities _context;

        public AfsRepository(AFS_DBEntities context)
        {
            _context = context;
        }

        public void AddInputText(ContentModel entity)
        {
            tblLeetSpeek model = new tblLeetSpeek();

            model.Id = Guid.NewGuid();
            model.InputText = entity.text;
            model.TranslationType = entity.translation;
            model.TranslatedText = entity.translated;
            model.CreateDate = DateTime.Now;
            model.CreateBy = "BBB441C5-BE75-42DB-AA73-40C84A35D8E7".ToString();

            _context.spInsertLeetSpeak(model.InputText, model.TranslationType,
                model.TranslatedText, model.CreateBy);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<spSelectAllLeetSpeekTexts> GetAllTranslatedData()
        {
            return _context.spSelectAllLeetSpeekTexts().ToList();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}