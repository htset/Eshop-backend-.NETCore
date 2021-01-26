﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop_API.Controllers
{
    public class QueryStringParameters
    {
        const int maxPageSize = 3;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 3;
        public int PageSize
        {
            get { return _pageSize;  }
            set { _pageSize = (value > PageSize) ? maxPageSize : value; }
        }

        public string Name { get; set; }
        public string Category { get; set; }
    }
}