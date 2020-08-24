using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Choi.MyProj.Domain
{
    public interface IResult<T>
    {
        bool Success { get; }

        T Value { get; }

        string ErrorMessage { get; }

        IResult<T> Failed(string error);
    }


    public static class Result<T>
    {
        public static IResult<T> Create(T value) => new Impl(value);

        public class Impl : IResult<T>
        {
            public bool Success { get; private set; }

            public T Value { get; private set; }

            public string ErrorMessage { get; private set; }

            public IResult<T> Failed(string error =  "") => new Impl(error);

            public Impl(T value)
            {
                Success = true;
                Value = value;
                ErrorMessage = string.Empty;
            }

            private Impl(string error)
            {
                Success = false;
                Value = default(T);
                ErrorMessage = error;
            }
        }

    }
}