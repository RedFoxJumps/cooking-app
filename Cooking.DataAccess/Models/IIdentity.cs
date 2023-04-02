namespace Cooking.DataAccess.Models
{
    public interface IIdentity<TId>
    {
        public TId Id { get; set; }
    }
}
