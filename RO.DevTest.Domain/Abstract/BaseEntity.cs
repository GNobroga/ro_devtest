namespace RO.DevTest.Domain.Abstract;
public abstract class BaseEntity {
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }

    protected BaseEntity() { }

    protected BaseEntity(Guid id) => Id = id;
}
