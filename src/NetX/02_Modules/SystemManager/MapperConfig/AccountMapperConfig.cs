using AutoMapper;
using NetX.SystemManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.Profiles
{
    /// <summary>
    /// account 映射清单
    /// </summary>
    public class AccountProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public AccountProfile()
        {
            ToEntity();
            ToModel();
        }

        private void ToEntity()
        {
            CreateMap<UserModel, sys_user>();
        }

        private void ToModel()
        {
            CreateMap<sys_user, UserModel>();
        }
    }
}
