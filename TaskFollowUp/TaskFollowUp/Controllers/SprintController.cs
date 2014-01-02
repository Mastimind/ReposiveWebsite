using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskFollowUp.Models;
using TaskFollowUp.Data;
using TaskFollowUp.Internal.Signaling;

namespace TaskFollowUp.Controllers
{
    public class SprintController : ApiController
    {
        SprintRepository _repository;
        private readonly SprintNotification _notifications;
        public SprintController(IRepository repository, SprintNotification notifications)
        {
            _repository = (SprintRepository)repository;
            _notifications = notifications;

        }
        // GET api/values
        public IEnumerable<GenericModel<SprintModel>> Get(int ? id)
        {
            var result = _repository.Get(null, new[] { "SprintTasks", "SprintTasks.AssignedDeveloper", "SprintTasks.Bugs" });
            var sprint=new GenericModel<SprintModel>{Title="All Sprints",Items= (id.HasValue ? result.Where(s => s.Id == id.Value) : result)
                                    .Select(s =>new SprintModel(s)).ToArray()};    
            var list = new List<GenericModel<SprintModel>>();
            list.Add(sprint);
            return list;
        }

        //// GET api/values/5
        //public SprintModel GetById(int ? id)
        //{
        //    return id.HasValue? new SprintModel(_repository.GetById(null, new[] { "Task", "Task.Developer", "Task.Bug" },id.Value)) : new SprintModel();    
        
        //}

        [HttpPost]
        public void AddSprint(SprintModel model)
        {
            Sprint entity = new Sprint();
            UpdateEntityFields(model, entity);
            _repository.Save(entity);

            _notifications.NotifyRefresh(model);
            
        }

        [HttpPut]
        public void UpdateSprint(SprintModel model)
        {
            Sprint entity = _repository.GetById(null,null,model.Id);
            UpdateEntityFields(model, entity);
            _repository.Save(entity);

            _notifications.NotifyRefresh(model);
        }

        [HttpDelete]
        public void DeleteSprint(SprintModel model)
        {
            _repository.DeleteById(model.Id);
            _notifications.NotifyDelete(model);
        }

        private static void UpdateEntityFields(SprintModel model, Sprint entity)
        {
            entity.SprintName = model.Title;
            entity.StartDate = DateTime.Parse(model.StartDate);
            entity.EndDate = DateTime.Parse(model.EndDate);
           
        }
    }
}