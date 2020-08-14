using System;
using System.Collections.Generic;

namespace WebAppLogin.Services
{
    public class Pagination<T>
    {
        public IEnumerable<T> data;
        public int pageSize;
        public int totalCount;
        public int currentPage;
        public int totalPages;
    }
}