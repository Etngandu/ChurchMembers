using ENB.Church.Members.Infrastructure;
using ENB.SchoolTimetables.EF;
using Microsoft.EntityFrameworkCore;

namespace ENB.Church.Members.EF
{
  public  class AsyncEFUnitOfWorkFactory :IAsyncUnitOfWorkFactory
    {
        private readonly ChurchMembersContext _churchMembersContext;

      

        public AsyncEFUnitOfWorkFactory(ChurchMembersContext  churchMembersContext)
        {
            _churchMembersContext = churchMembersContext;

        }
        public AsyncEFUnitOfWorkFactory(bool forcenew, ChurchMembersContext  churchMembersContext)
        {
                _churchMembersContext = churchMembersContext;

        }
        /// <summary>
        /// Creates a new instance of an EFUnitOfWork.
        /// </summary>
        public async Task<IAsyncUnitOfWork> Create()
        {
            return await Create(false);
        }

        /// <summary>
        /// Creates a new instance of an EFUnitOfWork.
        /// </summary>
        /// <param name="forceNew">When true, clears out any existing data context from the storage container.</param>
        public async Task<IAsyncUnitOfWork> Create(bool forceNew)
        {
            var asyncEFUnitOfWork = await Task.FromResult(new AsyncEFUnitOfWork(forceNew,_churchMembersContext));


            return asyncEFUnitOfWork!;

        }


    }
}
