﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.TaskScheduling.Model;

/// <summary>
/// 
/// </summary>
public class sys_jobtask : BaseEntity
{
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string group { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string jobtype { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string datamap { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool disallowconcurrentexecution { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DateTime createtime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string description { get; set; }
}