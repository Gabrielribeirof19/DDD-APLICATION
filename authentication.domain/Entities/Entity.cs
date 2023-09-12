namespace authentication.domain.entities
{
    public class Entity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Modified { get; set; }
        public Guid PersonIdCreated { get; set; } = Guid.Empty;
        public string Comments { get; set; } = string.Empty;

        public authentication.Domain.Enums.EStatus Status { get; set; } = authentication.Domain.Enums.EStatus.Active;
        public void SetModified() => Modified = DateTime.UtcNow;
        public Entity()
        {
            Created = DateTime.UtcNow;
            Modified = Created;
        }

        public void SetId(Guid id)
        {
            if (id == Guid.Empty)
                return;
            Id = id;
        }

        public void SetCreated(DateTime created)
        {
            Created = created;
        }

        public void CreatedNow()
        {
            SetCreated(DateTime.UtcNow);
            SetModified();
        }

        public void SetModified(DateTime modified)
        {
            Modified = modified;
        }

        public void SetStatus(authentication.Domain.Enums.EStatus status) => Status = status;

    }
}
