﻿using ENB.SchoolTimetables.EF;
using ENB.Church.Members.Entities.Repositories;
using ENB.Church.Members.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Church.Members.EF.Repositories
{

    /// <summary>
    /// A concrete repository to work with case in the system.
    /// </summary>
    public class AsyncActivityRepository : AsyncRepository<Activity>, IAsyncActivityRepository
    {
        /// <summary>
        /// Gets a list of all guests whose last name exactly matches the search string.
        /// </summary>
        /// <param name="name">The last name that the system should search for.</param>
        /// <returns>An IEnumerable of Person with the matching people.</returns>
        /// 

        private readonly ChurchMembersContext  _churchMembersContext;
        public AsyncActivityRepository(ChurchMembersContext  churchMembersContext) : base(churchMembersContext)
        {
            _churchMembersContext = churchMembersContext;
        }
        public IEnumerable<Activity> FindByName(string description)
        {
            return _churchMembersContext.Set<Activity>().Where(x => x.Activity_Description == description);
        }

        
    }
}
