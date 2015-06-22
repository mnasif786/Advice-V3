using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advice.Domain.Common
{
    public class BaseEntity<T> where T : struct
    {
        public virtual T Id { get; set; }
        
        public virtual DateTime? CreatedOn { get; set; }
        public virtual string LastModifiedBy { get; set; } //It is a UserForAuditing. Might need to create a UserForAuditing object..
        public virtual DateTime? LastModifiedOn { get; set; }
        public virtual bool Deleted { get; set; }
        private int? _hashCode;

        public override bool Equals(object obj)
        {
            var other = obj as BaseEntity<T>;
            if (other == null) return false;

            if (other.Id.Equals(default(T?)) && Id.Equals(default(T?)))
                return other == this;

            if (other.Id.Equals(default(T?)) || Id.Equals(default(T?)))
                return false;

            return other.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            if (_hashCode.HasValue)
                return _hashCode.Value;

            if (Id.Equals(default(T)))
            {
                _hashCode = base.GetHashCode();
                return _hashCode.Value;
            }

            return Id.GetHashCode();
        }

        protected internal virtual void SetLastModifiedDetails(string currentUser)
        {
            LastModifiedBy = currentUser;
            LastModifiedOn = DateTime.Now;
        }

        public virtual void MarkForDelete(string user)
        {
            if (!Deleted)
            {
                Deleted = true;
                SetLastModifiedDetails(user);
            }
        }

        public virtual void ReinstateFromDelete(string user)
        {
            if (Deleted)
            {
                Deleted = false;
                SetLastModifiedDetails(user);
            }
        }
    }
}

