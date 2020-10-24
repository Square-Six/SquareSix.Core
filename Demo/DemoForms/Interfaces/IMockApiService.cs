using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoForms.Models;

namespace DemoForms.Interfaces
{
    public interface IMockApiService
    {
        Task<List<CommentModel>> GetCommentModelsAsync();
    }
}
