using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class KursFileManager : KursFileService
    {
        KursFileDataAccess _kursFile;
        public KursFileManager(KursFileDataAccess KursFile)
        {
            _kursFile = KursFile;
        }
        public async Task Add(KursFile entity)
        {
            await _kursFile.Insert(entity);
        }

        public async Task Delete(KursFile entity)
        {
            await _kursFile.Delete(entity);
        }

        public async Task<KursFile> GetByID(int ID)
        {
            return await _kursFile.GetByID(ID);
        }

        public async Task<KursFile> GetImageByName(string name)
        {
            return await _kursFile.GetImageByName(name);
        }

        public async Task<KursFile> GetVideoByName(string name)
        {
            return await _kursFile.GetVideoByName(name);
        }
        public async Task<KursFile> GetImageByFileName(string name)
        {
            return await _kursFile.GetImageByFileName(name);
        }

        public async Task<KursFile> GetVideoByFileName(string name)
        {
            return await _kursFile.GetVideoByFileName(name);
        }

        public async Task<List<KursFile>> GetList()
        {
            return await _kursFile.GetList();
        }

        public async Task Update(KursFile entity)
        {
            await _kursFile.Update(entity);
        }

        public async Task<List<KursFile>> GetListWithIncludesAsync()
        {
            return await _kursFile.GetListWithIncludesAsync();
        }
    }
}
