﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Util.Applications.Trees;

namespace UniversalSystemCenter.Service.Dtos {
    /// <summary>
    /// 消息类型数据传输对象
    /// </summary>
    public class MessageCategoryDto : TreeDtoBase {
        /// <summary>
        /// 分类名称
        /// </summary>
        [Required(ErrorMessage = "分类名称不能为空")]
        [StringLength( 500, ErrorMessage = "分类名称输入过长，不能超过500位" )]
        [Display( Name = "分类名称" )]
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// 消息类型(通知、代办)
        /// </summary>
        [Required(ErrorMessage = "消息类型(通知、代办)不能为空")]
        [Display( Name = "消息类型(通知、代办)" )]
        [DataMember]
        public int Type { get; set; }
        /// <summary>
        /// 所属平台
        /// </summary>
        [Required(ErrorMessage = "所属平台不能为空")]
        [Display( Name = "所属平台" )]
        [DataMember]
        public int AppId { get; set; }
        /// <summary>
        /// 模块
        /// </summary>
        [StringLength( 500, ErrorMessage = "模块输入过长，不能超过500位" )]
        [Display( Name = "模块" )]
        [DataMember]
        public string Module { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        [Required(ErrorMessage = "路径不能为空")]
        [StringLength( 800, ErrorMessage = "路径输入过长，不能超过800位" )]
        [Display( Name = "路径" )]
        [DataMember]
        public string Path { get; set; }
        /// <summary>
        /// 是否末级
        /// </summary>
        [Display( Name = "是否末级" )]
        [DataMember]
        public bool? IsLeave { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Display( Name = "创建时间" )]
        [DataMember]
        public DateTime? CreationTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Display( Name = "创建人" )]
        [DataMember]
        public Guid? CreatorId { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [Display( Name = "最后修改时间" )]
        [DataMember]
        public DateTime? LastModificationTime { get; set; }
        /// <summary>
        /// 最后修改人
        /// </summary>
        [Display( Name = "最后修改人" )]
        [DataMember]
        public Guid? LastModifierId { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [Display( Name = "是否删除" )]
        [DataMember]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        [Display( Name = "版本号" )]
        [DataMember]
        public Byte[] Version { get; set; }
    }
}