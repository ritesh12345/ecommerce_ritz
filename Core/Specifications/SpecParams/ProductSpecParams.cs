using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications.SpecParams
{
    public class ProductSpecParams
    {
		private List<string> _brands = [];

        public const int MaxPageSize = 50;

        private int _pageSize = 6;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = _pageSize > MaxPageSize ? MaxPageSize : value; 
        }

        private int _pageIndex = 1;

        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value;
        }



        public List<string> Brands
		{
			get => _brands;
			set { _brands  = value.SelectMany(x=>x.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList(); }
		}

        private List<string> _types = [];

        public List<string> Types
        {
            get => _types;
            set { _types = value.SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList(); }
        }

        public string? Sort { get; set; }

        private string? _search;

        public string Search
        {
            get => _search ?? "";
            set => _search = value.ToLower();
        }

    }
}
