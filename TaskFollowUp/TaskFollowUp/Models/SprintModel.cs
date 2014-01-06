using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DotNet.Highcharts;
using DotNet.Highcharts.Options;
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

                int days = (int)(sprint.EndDate.Date-sprint.StartDate.Date).TotalDays;
               // Histories = new List<SprintHistroy>();

                categories=new string [days];
                data=new object[days];
                for (int i =0; i < days; i++)
                {
                    var dy=sprint.StartDate.AddDays(i + 1).Date.ToString("dd");
                    categories[i] = dy + (!dy[0].Equals('1')?(dy[1].Equals('1') ? "st" : (dy[1].Equals('2') ? "nd" : (dy[1].Equals('3') ? "rd" : "th"))):"th");
                    data[i]=(days - i) * 8;
                   
                }                           
            }
        }

        public string[] categories { get; set; }
        public object[] data {get;set;}

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string StartDate { get; set; }
        [Required]
        public string EndDate { get; set; }

        public ICollection<Task> SprintTasks { get;internal set; }
    }

    //public class SprintHistroy
    //{
    //    public int Id { get; set; }

    //    public int SprintId { get;set;}

    //    public double RemainingWork { get; set; }

    //    public DateTime ChangeDate {get ; set;}
    //}
}