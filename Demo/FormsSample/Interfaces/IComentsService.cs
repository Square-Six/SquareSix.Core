using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FormsSample.Models;

namespace FormsSample.Interfaces
{
    public interface IComentsService
    {
        Task<List<Comment>> GetCommentAsync();
    }
}
