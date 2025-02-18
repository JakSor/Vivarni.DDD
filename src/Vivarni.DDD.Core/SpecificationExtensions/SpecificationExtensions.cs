﻿using System;
using System.Runtime.CompilerServices;
using Ardalis.Specification;

namespace Vivarni.DDD.Core.SpecificationExtensions
{
    /// <summary>
    /// Extension methods for <see cref="Ardalis.Specification"/> which allows to configure the
    /// expiration time of cached specifications using <see cref="TimeSpan"/>'s.
    /// </summary>
    public static class SpecificationExtensions
    {
        /// <summary>
        /// Sets the caching expiration timespan of the specification,
        /// </summary>
        public static void SetCacheTTL<T>(this ISpecification<T> spec, TimeSpan ttl)
        {
            spec.Items["CacheTTL"] = ttl;
        }

        /// <summary>
        /// Returns the caching expiration timespan of the specification,
        /// or <see cref="TimeSpan.MaxValue" /> if not configured.
        /// </summary>
        public static TimeSpan GetCacheTTL<T>(this ISpecification<T> spec)
        {
            spec.Items.TryGetValue("CacheTTL", out var ttl);
            return (ttl as TimeSpan?) ?? TimeSpan.MaxValue;
        }
    }
}
