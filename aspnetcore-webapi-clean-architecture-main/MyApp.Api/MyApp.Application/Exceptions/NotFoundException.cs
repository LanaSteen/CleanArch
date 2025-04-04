﻿using System;

namespace MyApp.Application.Exceptions
{
    public class NotFoundException : Exception
    { 
            public NotFoundException(string message) : base(message)
            {
            }

            public NotFoundException(string name, object key)
                : base($"Entity \"{name}\" ({key}) was not found.")
            {
            }
        }

        public class BusinessRuleException : Exception
        {
            public BusinessRuleException(string message) : base(message)
            {
            }
        }
    
  
}