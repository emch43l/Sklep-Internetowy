using Sklep_Internetowy.Contexts;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Repositories.Interfaces;

namespace Sklep_Internetowy.Repositories
{
    public class ImageRepository : RepositoryBase<Image>, IImageRepository
    {
        public ImageRepository(DataContext context) : base(context)
        {
        }
    }
}
