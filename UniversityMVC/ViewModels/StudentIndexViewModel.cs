using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using UniversityMVC.Models;

namespace UniversityMVC.ViewModels
{
    public class StudentIndexViewModel
    {
        public IPagedList<Student> Students { get; set; }
        public string Search { get; set; }
        public IEnumerable<CampusWithCount> CampusesWithCount { get; set; }
        public string Campus { get; set; }
        public string SortBy { get; set; }
        public Dictionary<string, string> Sorts { get; set; }

        public IEnumerable<SelectListItem> CampusFilterItems
        {
            get
            {
                var allCampuses = CampusesWithCount.Select(cc => new SelectListItem
                {
                    Value = cc.CampusName,
                    Text = cc.CampusNameWithCount
                });
                return allCampuses;
            }
        }
    }

    public class CampusWithCount
    {
        public string CampusName { get; set; }
        public int StudentCount { get; set; }
        public string CampusNameWithCount
        {
            get
            {
                return CampusName + " (" + StudentCount.ToString() + ")";
            }
        }
    }
}
