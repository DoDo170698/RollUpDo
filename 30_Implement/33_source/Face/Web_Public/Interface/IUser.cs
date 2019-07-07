using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Public.Interface
{
    interface IUser 
    {
        Task<ExecuteResult> CheckLogIn(string UserName, string Password);
    }
}
