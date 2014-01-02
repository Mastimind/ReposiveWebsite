using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TaskFollowUp.Data;

namespace TaskFollowUp.Models
{
    public class SprintModel
    {
       
        public SprintModel(Sprint sprint)
        {
            if (sprint != null)
            {

                Id = sprint.Id;
                Title = sprint.SprintName;
                StartDate = sprint.StartDate.ToString("yyyy-MM-dd");
                EndDate = sprint.EndDate.ToString("yyyy-MM-dd");
                SprintTasks = sprint.SprintTasks;
            }
        }



        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string StartDate { get; set; }
        [Required]
        public string EndDate { get; set; }

        public ICollection<Task> SprintTasks { get;internal set; }
    }
}