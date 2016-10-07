using EF_Relations.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EF_Relations.ViewModels.MovieVM
{
    public class CreateMovieVM
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [DisplayName("Release Date: ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        public int RunningTime { get; set; }

        public List<CheckBoxListItem> Genres { get; set; } = new List<CheckBoxListItem>();
    }
}