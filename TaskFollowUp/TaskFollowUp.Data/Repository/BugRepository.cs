//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
namespace TaskFollowUp.Data
{
    public partial class BugRepository:IRepository<Bug>,IRepository
    {
    		DbContext _context;
            public BugRepository(DbContext context)
            {
                _context = context ?? new ProjectConnectionString();
            }
    
            public IEnumerable<Bug> Get(Expression<Func<Bug, bool>> predicate, string [] relations)
            {
                    var query = _context.Set<Bug>().Select(s=>s);
                    foreach (var rel in relations)
                    {
                      query=  query.Include(rel);
                    }
    
                    return (predicate!=null ? query.Where(predicate) : query).ToList();        
                }
    
    		public Bug GetById(Expression<Func<Bug, bool>> predicate, string [] relations, int Id)
            {
    				var query = _context.Set<Bug>().Select(s=>s);;
                    foreach (var rel in relations)
                    {
                      query=  query.Include(rel);
                    }
                return (predicate!=null? query.Where(predicate) : query).FirstOrDefault(s => s.Id == Id);
            }
    
            public void Save(List<Bug> entities)
            {
                foreach(var entity in entities)
    			{
    				SetState(entity);
    			}
    			_context.SaveChanges();
            }
    
            public void Save(Bug entity)
            {
                SetState(entity);
    			
    			_context.SaveChanges();
            }
    
            public void Delete(Bug entity)
            {
                _context.Set<Bug>().Remove(entity);
    			
    			_context.SaveChanges();
            }
    
            public void DeleteById(int Id)
            {
               var entity= _context.Set<Bug>().FirstOrDefault(s => s.Id == Id);
    
    		   Delete(entity);
            }
    
    		 private void SetState(Bug entity)
             {
                    _context.Entry(entity).State = entity.Id <= 0 ? EntityState.Added : EntityState.Modified;
             }
    }
}
