#region Namespaces

using Evolent.DataModel.GenericRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;

#endregion

namespace Evolent.DataModel.UnitOfWork
{
    #region Class

    /// <summary>
    /// <see cref="UnitOfWork"/> Class which implements IDisposable
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Private Variables

        private bool disposed = false;
        private EvolentEntities _context = null;
        private IGenericRepository<Contact> _contactRepository;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="UnitOfWork"/> class.
        /// </summary>
        public UnitOfWork()
        {
            _context = new EvolentEntities();
        }

        #endregion

        #region Public Repository Properties

        /// <summary>
        /// Get/Set Property for Contact repository.
        /// </summary>
        public IGenericRepository<Contact> ContactRepository
        {
            get
            {
                if (this._contactRepository == null)
                    this._contactRepository = new GenericRepository<Contact>(_context);
                return _contactRepository;
            }
        }

        #endregion

        #region Public member methods...
        /// <summary>
        /// Save method.
        /// </summary>
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var outputLines = new List<string>();
                foreach (var eve in ex.EntityValidationErrors)
                {
                    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"D:\errors.txt", outputLines);
                throw ex;
            }
        }

        #endregion

        #region IDiosposable Implementation

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    #endregion
}
