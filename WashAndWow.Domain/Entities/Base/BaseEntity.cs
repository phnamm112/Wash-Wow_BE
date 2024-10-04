using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wash_Wow.Domain.Entities.Base
{
    public abstract class BaseEntity : IDisposable
    {
        protected BaseEntity()
        {
            ID = Guid.NewGuid().ToString("N");
            CreatedAt = LastestUpdateAt = DateTime.Now;
        }

        [Key]
        public string ID { get; set; }


        public string? CreatorID { get; set; }
        public DateTime? CreatedAt { get; set; }

        public string? UpdaterID { get; set; }
        public DateTime? LastestUpdateAt { get; set; }

        public string? DeleterID { get; set; }
        public DateTime? DeletedAt { get; set; }


        [NotMapped]
        private bool IsDisposed { get; set; }

        #region Dispose
        public void Dispose()
        {
            Dispose(isDisposing: true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool isDisposing)
        {
            if (!IsDisposed)
            {
                if (isDisposing)
                {
                    DisposeUnmanagedResources();
                }

                IsDisposed = true;
            }
        }

        protected virtual void DisposeUnmanagedResources()
        {
        }
        ~BaseEntity()
        {
            Dispose(isDisposing: false);
        }
        #endregion Dispose
    }
}
