﻿using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.Models
{
    public class PageResult
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public List<Flight> Items { get; set; }

        public PageResult(List<Flight> item)
        {
            Page = 0;
            TotalItems = item.Count();
            Items = item;
        }
    }
}
