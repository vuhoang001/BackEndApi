﻿namespace BackEndApi.Models.Common;

public class Pagination
{
    public Pagination()
    {
    }

    public object Result { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
}