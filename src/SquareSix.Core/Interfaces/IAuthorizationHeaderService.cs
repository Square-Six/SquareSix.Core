using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SquareSix.Core
{
    public interface IAuthorizationHeaderService
    {
        Task<List<KeyValuePair<string, string>>> GetAuthorizationHeaders();
    }
}
