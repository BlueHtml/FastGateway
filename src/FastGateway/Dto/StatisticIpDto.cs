﻿namespace FastGateway.Dto;

public sealed class StatisticIpDto
{
    public string Ip { get; set; }

    public DateTime CreatedTime { get; set; }
    
    public int Count { get; set; }

    public string ServiceId { get; set; }
    
    /// <summary>
    /// 归属地
    /// </summary>
    public string? Location { get; set; }
}