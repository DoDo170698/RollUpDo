using System;

namespace Entities.Models
{
    public interface Entity
    {
        //[NotMapped]
       // public virtual System.Guid Guid { get; set; }
        string Describe();
    }
    //public class BaseEntity 
    //{
    //    public BaseEntity()
    //    {
    //        CreatedDate = DateTime.Now;
    //        IsDeleted = false;
    //        Active = true;
    //    } 
    //    public DateTime? CreatedDate { get; set; } 
    //    public DateTime? UpdatedDate { get; set; } 
    //    public bool Active { get; set; }
    //    public bool IsDeleted { get; set; }
    //}
  
}