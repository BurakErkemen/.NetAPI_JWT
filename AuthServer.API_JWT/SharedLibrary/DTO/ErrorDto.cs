﻿using System.Runtime.InteropServices;

namespace SharedLibrary.DTO
{
    public class ErrorDto
    {
        public List<string>? Errors { get; private set; }
        public bool IsShow { get; private set; }

        public ErrorDto()
        {
            Errors = new List<string>();
        }


        public ErrorDto(string error, bool isShow)
        {
            Errors?.Add(error);
            IsShow = isShow;
        }

        public ErrorDto(List<string> errors, bool isShow)
        {
            Errors = Errors; 
            IsShow = isShow;
        }
    }
}