﻿using JetBrains.Annotations;

namespace Application.UnitTests.Dto
{
    public struct LoadFundRequestDto
    {
        public string external_tag { get; set; }
        public int account_id { get; set; }
        public int amount { get; set; }
        public string? message { get; set; }
        public string? idempotency_key { get; set; }
    }
}