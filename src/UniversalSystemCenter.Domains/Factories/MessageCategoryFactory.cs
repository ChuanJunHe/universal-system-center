﻿using System;
using UniversalSystemCenter.Domain.Models;

namespace UniversalSystemCenter.Domains.Factories {
    /// <summary>
    /// 消息类型工厂
    /// </summary>
    public static class MessageCategoryFactory {
        /// <summary>
        /// 创建消息类型
        /// </summary>
        /// <param name="categoryId">消息分类编号（MessageCategoryId）</param>
        /// <param name="name">分类名称</param>
        /// <param name="type">消息类型(通知、代办)</param>
        /// <param name="appId">所属平台</param>
        /// <param name="module">模块</param>
        /// <param name="parentId">父编号</param>
        /// <param name="path">路径</param>
        /// <param name="level">级数</param>
        /// <param name="isLeave">是否末级</param>
        /// <param name="enabled">启用</param>
        /// <param name="sortId">排序号</param>
        /// <param name="creationTime">创建时间</param>
        /// <param name="creatorId">创建人</param>
        /// <param name="lastModificationTime">最后修改时间</param>
        /// <param name="lastModifierId">最后修改人</param>
        /// <param name="isDeleted">是否删除</param>
        /// <param name="version">版本号</param>
        public static MessageCategory Create( 
            Guid categoryId,
            string name,
            int type,
            int appId,
            string module,
            Guid? parentId,
            string path,
            int level,
            bool isLeave,
            bool enabled,
            int sortId,
            DateTime? creationTime,
            Guid? creatorId,
            DateTime? lastModificationTime,
            Guid? lastModifierId,
            bool isDeleted,
            Byte[] version
        ) {
            MessageCategory result;
            result = new MessageCategory( categoryId,"",1 );
            result.Name = name;
            result.Type = type;
            result.AppId = appId;
            result.Module = module;
            result.ParentId = parentId;
            result.IsLeave = isLeave;
            result.Enabled = enabled;
            result.SortId = sortId;
            result.CreationTime = creationTime;
            result.CreatorId = creatorId;
            result.LastModificationTime = lastModificationTime;
            result.LastModifierId = lastModifierId;
            result.IsDeleted = isDeleted;
            result.Version = version;
            return result;
        }
    }
}