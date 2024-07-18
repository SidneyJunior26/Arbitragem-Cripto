using System.ComponentModel.DataAnnotations;

namespace Solution.Core.Entities;

public class Entity
{
    [Key]
    public Guid Id
	{
		get;
		protected set;
	}
	public DateTime RegisterDate
	{
		get;
        protected set;
	}
	public DateTime UpdateDate
	{
		get;
        protected set;
	}
}

