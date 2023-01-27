using System;

namespace MockBank.Application.Configurations.Common.Interfaces
{
    public interface IDateTime
    {
        DateTime Now { get; }
    }
}