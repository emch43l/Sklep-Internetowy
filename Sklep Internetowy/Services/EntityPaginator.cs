namespace Sklep_Internetowy.Services
{
    public class EntityPaginator<T>
    {
        private int _pageEntityNum = 10;

        private int _maxPageButtons = 3;

        private IEnumerable<T> _entities;
        public EntityPaginator(IEnumerable<T> data) 
        {
            _entities = data;
        }
        public IEnumerable<T> GetPaginatedData(int pageNum)
        {
            if (pageNum < 1)
                throw new ArgumentException("Page number cannot be less than 1 !");
            return _entities
                .Skip(_pageEntityNum * (pageNum - 1))
                .Take(_pageEntityNum);
        }

        public int GetPagesNumber()
        {
            return (int)Math.Ceiling(((double)_entities.Count()) / _pageEntityNum);
        }

        public List<int> GetPagesNumber(int pageNum = 1)
        {

            int min = pageNum - _maxPageButtons / 2;
            int max = pageNum + _maxPageButtons / 2;
            int pagesNumber = GetPagesNumber();


            if (min < 1)
            {
                int shift = Math.Abs(min) + 1;
                max += shift;
                min += shift;
            }

            if (max > pagesNumber)
            {
                min -= max - pagesNumber;
                if (min < 1)
                    min += Math.Abs(min) + 1;
                max -= max - pagesNumber;
            }

            return GeneratePageValues(min, max - min + 1);
        }

        public void SetMaxPageButtons(int number)
        {
            if(number < 3)
                throw new ArgumentException("Number of buttons on page must be at least 3 !");
            if (number % 3 != 0)
                throw new ArgumentException("Number must be multiple of the number 3 !");
            _maxPageButtons = number;
        }
        
        private List<int> GeneratePageValues(int a, int b)
        {
            return Enumerable.Range(a, b).Select(i => i).ToList();
        }

        public void SetPageEntityNumber(int pageEntityNum)
        {
            if (pageEntityNum <= 0)
                throw new ArgumentException("Value must be positive !");
            _pageEntityNum = pageEntityNum;
        }

        public int GetPageEntityNumber()
        {
            return _pageEntityNum;
        }
    }
}
