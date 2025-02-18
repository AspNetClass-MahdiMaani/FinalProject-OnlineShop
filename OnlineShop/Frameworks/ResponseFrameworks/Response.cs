﻿using OnlineShop.Frameworks.ResponseFrameworks.Contracts;
using System.Net;

namespace OnlineShop.Frameworks.ResponseFrameworks
{
    public class Response<T> : IResponse<T>
    {
        #region [- ctors -]
        public Response()
        {

        }

        public Response(bool isSuccessful, HttpStatusCode status, string? message, T? value)
        {
            IsSuccessful = isSuccessful;
            Status = status;
            Message = message;
            Value = value;
        }
        #endregion

        public bool IsSuccessful { get; set; }
        public HttpStatusCode Status { get; set; }
        public string? Message { get; set; }
        public T? Value { get; set; }
    }
}
