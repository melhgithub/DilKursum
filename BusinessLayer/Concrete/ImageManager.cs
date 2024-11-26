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
    public class ImageManager : ImageService
    {
        ImageDataAccess _image;
        public ImageManager(ImageDataAccess Image)
        {
            _image = Image;
        }
        public async Task Add(Image entity)
        {
            await _image.Insert(entity);
        }

        public async Task Delete(Image entity)
        {
            await _image.Delete(entity);
        }

        public async Task<Image> GetByID(int ID)
        {
            return await _image.GetByID(ID);
        }

        public async Task<Image> GetImageByName(string name)
        {
            return await _image.GetImageByName(name);
        }

        public async Task<List<Image>> GetList()
        {
            return await _image.GetList();
        }

        public async Task Update(Image entity)
        {
            await _image.Update(entity);
        }
    }
}
