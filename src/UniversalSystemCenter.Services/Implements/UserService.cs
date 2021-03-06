﻿using Util;
using Util.Maps;
using Util.Domains.Repositories;
using Util.Datas.Queries;
using Util.Applications;
using UniversalSystemCenter.Data;
using UniversalSystemCenter.Domain.Models;
using UniversalSystemCenter.Domain.Repositories;
using UniversalSystemCenter.Service.Dtos;
using UniversalSystemCenter.Service.Queries;
using UniversalSystemCenter.Service.Abstractions;
using System.Threading.Tasks;
using UniversalSystemCenter.Core.Auth.Param;
using UniversalSystemCenter.Core.Result;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using UniversalSystemCenter.Core.Auth;

namespace UniversalSystemCenter.Service.Implements
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public class UserService : CrudServiceBase<User, UserDto, UserQuery>, IUserService
    {
        /// <summary>
        /// 初始化用户服务
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="userRepository">用户仓储</param>
        public UserService(IUniversalSysCenterUnitOfWork unitOfWork, IUserRepository userRepository, IAuthRequest authRequest)
            : base(unitOfWork, userRepository)
        {
            UserRepository = userRepository;
            _authRequest = authRequest;
        }

        /// <summary>
        /// 用户仓储
        /// </summary>
        public IUserRepository UserRepository { get; set; }

        private IAuthRequest _authRequest { get; set; }

        /// <summary>
        /// 创建查询对象
        /// </summary>
        /// <param name="param">查询参数</param>
        protected override IQueryBase<User> CreateQuery(UserQuery param)
        {
            return new Query<User>(param);
        }


        /// <summary>
        /// 系统中心用户登录
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        public async Task<Result> UserLoginAsync(LoginDto loginDto)
        {
            var client = new Client() { UserName = loginDto.UserName, ClientId = loginDto.ClientId, Password = loginDto.Password };
            var result = await _authRequest.SendPasswordAuth(client);
            return result;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        public async Task<Result> LoginAsync(LoginDto loginDto)
        {
            Result result = new Result();
            try
            {
                var data = await UserRepository.FindAsNoTracking().Include(a => a.Account).Where(a => a.EId == loginDto.UserName || a.Account.Mobile == loginDto.UserName).Select(a => new LoginUser()
                {
                    Id = a.Id,
                    Account = a.EId,
                    OrganizationsId = a.OrganizationsId.ToString(),
                    Password = a.Account.Password,
                    RoleIdList = a.UserRoles.Select(b => b.RoleId).ToList(),
                    State = a.Account.State,
                    Saltd = a.Account.Saltd,
                    Head = a.Account.Head,
                    RealName = a.Account.RealName,
                    Mobile = a.Account.Mobile,
                    Sex = a.Account.Sex,
                    IsLocked = a.IsLocked,
                    LockDuration = a.LockDuration,
                    RegisterTime = a.RegisterTime,
                    LockMessage = a.LockMessage,
                    MerchantId = a.MerchantId,
                    MerchantName = a.Merchant.Name
                }).FirstOrDefaultAsync();
                LoginUserVerify(loginDto, result, data, true);
            }
            catch (Exception ex)
            {
                result.Code = 50;
                result.ResultEnum = ResultEnum.Warning;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 校验登录用户
        /// </summary>
        /// <param name="loginDto"></param>
        /// <param name="result"></param>
        /// <param name="data"></param>
        /// <param name="isCheckPwd">是否校验密码</param>
        private static void LoginUserVerify(LoginDto loginDto, Result result, LoginUser data, bool isCheckPwd)
        {
            if (null == data)
            {
                result.Code = 100;
                result.ResultEnum = ResultEnum.Warning;
                result.Message = "用户不存在";
            }
            else if (data.State != AccountStateEnums.InUse.GetHashCode())
            {
                result.Code = 101;
                result.ResultEnum = ResultEnum.Warning;
                result.Message = "用户已失效";
            }
            else if (data.IsLocked)
            {
                result.Code = 102;
                result.ResultEnum = ResultEnum.Warning;
                result.Message = "用户名已被锁定，无法登录，请联系管理员";
            }
            else if (isCheckPwd && data.Password != Util.Helpers.Encrypt.Md5By32(loginDto.Password + data.Saltd))
            {
                result.Code = 103;
                result.ResultEnum = ResultEnum.Warning;
                result.Message = "用户名或密码不正确";
            }
            else
            {
                data.Password = string.Empty;
                result.Code = 200;
                result.ResultEnum = ResultEnum.Sucess;
                result.Data = data;
                result.Message = "登录成功";
            }
        }
    }
}