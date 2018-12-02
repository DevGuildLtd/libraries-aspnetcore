using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Data
{
    /// <summary>
    /// Defines interface for repository factory.
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Creates the repository.
        /// </summary>
        /// <returns>Created repository</returns>
        IRepository CreateRepository();
    }
}
