using OnlinePizzaWebApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePizzaWebApplication.ViewModels
{
    public class SearchBooksViewModel
    {
        [Required]
        [DisplayName("Search")]
        public string SearchText { get; set; }

        //public IEnumerable<string> SearchListExamples { get; set; }

        public IEnumerable<Books> BookList { get; set; }

    }
}
